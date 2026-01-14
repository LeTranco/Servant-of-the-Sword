using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    private Scene scene;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(scene.buildIndex + 1);
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
