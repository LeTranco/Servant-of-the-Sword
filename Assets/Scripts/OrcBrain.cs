using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class OrcBrain : MonoBehaviour {
    public Transform enemy;
    
    [Header("Q-Learning Ayarları")]
    public float learningRate = 0.1f;    
    public float discountFactor = 0.95f; 
    public float explorationRate = 0.2f; 
    
    private Dictionary<string, float[]> qTable = new Dictionary<string, float[]>();
    private string lastState;
    private int lastAction;
    private float lastDistance;
    private bool firstActionTaken = false;
    private string SavePath => Path.Combine(Application.persistentDataPath, "orc_intelligence.json");

    void Start() {
        LoadBrain();
    }

    public void SaveBrain() {
        try {
            QTableData data = new QTableData();
            foreach (var kvp in qTable) {
                data.keys.Add(kvp.Key);
                data.values.Add(new FloatArray { array = kvp.Value });
            }
            File.WriteAllText(SavePath, JsonUtility.ToJson(data));
        } catch (System.Exception e) { Debug.LogError("Kaydetme Hatası: " + e.Message); }
    }

    public void LoadBrain() {
        if (!File.Exists(SavePath)) return;
        try {
            QTableData data = JsonUtility.FromJson<QTableData>(File.ReadAllText(SavePath));
            qTable.Clear();
            for (int i = 0; i < data.keys.Count; i++) qTable.Add(data.keys[i], data.values[i].array);
        } catch (System.Exception e) { Debug.LogError("Yükleme Hatası: " + e.Message); }
    }

    string GetState() {
        if (enemy == null) return "Enemy_Dead";
        float dist = Vector2.Distance(transform.position, enemy.position);
        string distRange = dist < 1.2f ? "Close" : (dist < 3f ? "Mid" : "Far");
        string relativePos = (enemy.position.x > transform.position.x) ? "Right" : "Left";
        return $"{distRange}_{relativePos}";
    }

    public int DecideAction() {
        if (enemy == null) return -1;
        string currentState = GetState();
        float currentDist = Vector2.Distance(transform.position, enemy.position);
        if (!qTable.ContainsKey(currentState))
            qTable[currentState] = new float[4];

        if (firstActionTaken) {
            float distDiff = lastDistance - currentDist;
            float moveReward = 0;
            if (distDiff > 0.01f) moveReward = 0.8f;  
            else if (distDiff < -0.01f) moveReward = -1.0f; 
            else moveReward = -0.1f; // 
            UpdateQTable(lastState, currentState, lastAction, moveReward);
        }

        lastDistance = currentDist;
        lastState = currentState;
        
        int action;
        if (Random.value < explorationRate) {
            action = Random.Range(0, 4);
        } else {
            action = System.Array.IndexOf(qTable[currentState], qTable[currentState].Max());
        }
        
        if(explorationRate > 0.05f)
            explorationRate -= 0.0001f; 

        lastAction = action;
        firstActionTaken = true;
        return action;
    }

    public void UpdateQTable(string state, string nextState, int action, float reward) {
        if (!qTable.ContainsKey(state) || !qTable.ContainsKey(nextState)) return;
        float oldQ = qTable[state][action];
        qTable[state][action] += learningRate * (reward + (discountFactor * qTable[nextState].Max()) - oldQ);
        qTable[state][action] = Mathf.Clamp(qTable[state][action], -50f, 50f);
    }

    public void ApplyReward(float reward) {
        if (!string.IsNullOrEmpty(lastState)) UpdateQTable(lastState, GetState(), lastAction, reward);
    }
    private void OnApplicationQuit() => SaveBrain();
}

[System.Serializable]
public class QTableData
{
    public List<string> keys = new List<string>(); public List<FloatArray> values = new List<FloatArray>();
}

[System.Serializable]
public class FloatArray
{
    public float[] array;

}
