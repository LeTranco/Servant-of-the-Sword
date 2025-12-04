using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    public Slider slider;
    
    void Start()
    {
        LoadAudio();
    }
    
    public void SetAudio(float value)
    {
        AudioListener.volume = value;
        SaveAudio();
    }

    private void SaveAudio()
    {
        PlayerPrefs.SetFloat("Audio", AudioListener.volume);
    }

    private void LoadAudio()
    {
        if (PlayerPrefs.HasKey("Audio"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("Audio");
            slider.value = PlayerPrefs.GetFloat("Audio");
        }
        else
        {
            PlayerPrefs.SetFloat("Audio", 0.5f);
            AudioListener.volume = PlayerPrefs.GetFloat("Audio");
            slider.value = PlayerPrefs.GetFloat("Audio");
        }
    }
    
    void Update()
    {
        
    }
}
