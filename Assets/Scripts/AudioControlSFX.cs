using UnityEngine;
using UnityEngine.UI;

public class AudioControlSFX : MonoBehaviour
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
        PlayerPrefs.SetFloat("SFX", AudioListener.volume);
    }
    
    private void LoadAudio()
    {
        if (PlayerPrefs.HasKey("SFX"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("SFX");
            slider.value = PlayerPrefs.GetFloat("SFX");
        }
        else
        {
            PlayerPrefs.SetFloat("SFX", 0.5f);
            AudioListener.volume = PlayerPrefs.GetFloat("SFX");
            slider.value = PlayerPrefs.GetFloat("SFX");
        }
    }
    
    void Update()
    {
        
    }
}
