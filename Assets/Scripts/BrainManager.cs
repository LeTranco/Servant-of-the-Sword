using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class BrainManager : MonoBehaviour
{
    public static BrainManager Instance { get; private set; }
    private string fileName = "orc_intelligence.json";
    public string loadedJsonData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void OnBrainToggleChanged(bool useDefault)
    {
        if (useDefault) StartCoroutine(LoadStreamingAssets());
        else LoadOrCreateLocalLow();
    }

    private IEnumerator LoadStreamingAssets()
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);
        #if UNITY_WEBGL && !UNITY_EDITOR
            using (UnityWebRequest webRequest = UnityWebRequest.Get(path))
            {
                yield return webRequest.SendWebRequest();
                if (webRequest.result == UnityWebRequest.Result.Success)
                    loadedJsonData = webRequest.downloadHandler.text;
            }
        #else
        if (File.Exists(path)) loadedJsonData = File.ReadAllText(path);
        yield return null;
#endif
    }

    private void LoadOrCreateLocalLow()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "{}");
#if UNITY_WEBGL && !UNITY_EDITOR
                PlayerPrefs.Save(); 
#endif
        }
        loadedJsonData = File.ReadAllText(path);
    }
}