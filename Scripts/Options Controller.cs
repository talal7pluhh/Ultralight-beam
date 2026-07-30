using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;
public class OptionsController : MonoBehaviour
{

    [Header("volume")]
    public Slider volumeSlider;
    public AudioSource[] allAudioSources;
    public TextMeshProUGUI volumeText;  
    void Start()
    {

        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;

            volumeSlider.onValueChanged.AddListener(applyVolume);
        }
        applyVolume(savedVolume);
    }
    
    void applyVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
        if (volumeText != null)
        {
           volumeText.text = Mathf.RoundToInt(value * 100) + "%";
        }
    }
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }

        public void ContinueGame()
        {

        if (WinCondition.instance != null)
        {
            WinCondition.instance.ResumeGame();    
        }

    }
}
