using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class StartScreen : MonoBehaviour
{

    [Header("UI")]
    public CanvasGroup group;
    public CanvasGroup textCanvasGroup;
    public TextMeshProUGUI storyText;


    [Header("Animation")]
    public float sceneFadeDuration = 1f;
    public float textFadeDuration = 1f;
    public float holdDuration = 3.8f;

    [TextArea(3, 10)]
    public string[] storyTexts;
    private bool epilogueCoroutine = false;
    private void Start()
    {
        group.alpha = 0f;
        textCanvasGroup.alpha = 0f;
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
        yield return new WaitForSeconds(1.5f);

        yield return Fade(group, 1f, 0f, sceneFadeDuration);

        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            while (audio.volume > 0f)
            {
                audio.time -= Time.deltaTime * 2f;
                yield return null;
            }
        }
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
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
