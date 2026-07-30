using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    public static WinCondition instance;
    public CanvasGroup WinPanelCanvasGroup;
    public GameObject WinPanel;
    public GameObject touchLook;
    public JoyStickMove stickMove;
    private bool hasWon = false;
    public float fadeDuration = 0.7f;
    public static class PauseState
    {
        public static bool isPaused = false;
    }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        PauseState.isPaused = false;
        Time.timeScale = 1f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (hasWon) return;
        if (other.CompareTag("Player"))
        {
            hasWon = true;
            TriggerWin();
        }
    }

    private void TriggerWin()
    {
        if (touchLook != null)
        {

            touchLook.SetActive(false);
            var touchLookScript = touchLook.GetComponent<TouchLook>();
            if (touchLookScript != null)
            {
                touchLookScript.enabled = false;
            }
        }

        if (stickMove!= null)
        {
            stickMove.gameObject.SetActive(false);
        }

        if (WinPanel != null)
        {
            WinPanel.SetActive(true);
            StartCoroutine(FadeIn());
        }
    }
    private IEnumerator FadeIn()
    {
        WinPanelCanvasGroup.alpha = 0f;
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            WinPanelCanvasGroup.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }
        WinPanelCanvasGroup.alpha = 1f;
        Time.timeScale = 0f;
    }

    public void FinishStory()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("End Scene");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        if (PauseState.isPaused) return;
        
        PauseState.isPaused = true;
        Time.timeScale = 0f;
        if (touchLook != null) touchLook.SetActive(false);
        if (stickMove != null) stickMove.gameObject.SetActive(false);
        SceneManager.LoadScene("Menu Scene", LoadSceneMode.Additive);
    }

    public void ResumeGame()
    {
        PauseState.isPaused = false;
        Time.timeScale = 1f;

        if (touchLook != null) touchLook.SetActive(true);
        if (stickMove != null) stickMove.gameObject.SetActive(true);

        SceneManager.UnloadSceneAsync("Menu Scene");    
    }

}
