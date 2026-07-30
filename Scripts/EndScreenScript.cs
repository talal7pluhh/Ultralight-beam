using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using System.Collections;
using TMPro;

public class EndScreenScript : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup group;
    public CanvasGroup textCanvasGroup;
    public CanvasGroup buttonCanvasGroup;
    public Button QuitButton;
    public TextMeshProUGUI storyText;
    public CanvasGroup logoGame;


    [Header("Animation")]
    public float sceneFadeDuration = 1f;
    public float textFadeDuration = 1f;
    public float holdDuration = 3.8f;
    public float ButtonFadeDuration = 1.5f;
    public float LogoFadeDuration = 1.5f;
    // variables are easy to read through and understand if you're familiar with unity 

    [TextArea(3, 10)]
    public string[] storyTexts;
    private bool epilogueCoroutine = false;
    private void Start()
    {
        group.alpha = 0f;
        textCanvasGroup.alpha = 0f;
        buttonCanvasGroup.alpha = 0f;
        logoGame.alpha = 0f;

        // again easy
        QuitButton.gameObject.SetActive(true);
        QuitButton.interactable = false;
        QuitButton.onClick.AddListener(QuitGame);

        if (!epilogueCoroutine)
        {        
            epilogueCoroutine = true;
            StartCoroutine(RunEpilogue());
        }
        // this line fixed my game, because fading texts didn't work well these 3 lines helped me;

    }

    private IEnumerator RunEpilogue()
    {
        yield return Fade(group, 0f, 1f, sceneFadeDuration);

        foreach (string segment in storyTexts)
        {
            storyText.text = segment;
            yield return Fade(textCanvasGroup, 0f, 1f, textFadeDuration);
            yield return new WaitForSeconds(holdDuration);
            yield return Fade(textCanvasGroup, 1f, 0f, textFadeDuration);
            yield return new WaitForSeconds(1f);    
            // animates through the end of the story 
            // these 1f and 0f are the ones responsible for the fade in-out
            // it's pretty much understandable code for most people;
        }
        yield return Fade(logoGame, 0f, 1f, LogoFadeDuration);
        yield return Fade(buttonCanvasGroup, 0f, 1f, ButtonFadeDuration);
        QuitButton.interactable = true;
        QuitButton.gameObject.SetActive(true); 
        // this one was optional which removes the button from sight so the player can focus on reading;;
        // it just sets the button invisible through the readout? is this a real word? (readout);

    }

    private IEnumerator Fade(CanvasGroup group, float from, float to, float duration)
    {
        float t = 0f;
        group.alpha = from;
        while (t < duration)
        {
            t += Time.deltaTime;
            group.alpha = Mathf.Lerp(from, to, t / duration);
            yield return null;

        }
        group.alpha = to;
    }
    // i needed some help in these lines of code i'm not really good with loops and arguments they're
    // too much
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }
}
