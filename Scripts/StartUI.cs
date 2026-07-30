using UnityEngine;
using UnityEngine.SceneManagement;
public class StartUI : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("BeginningOfStory");
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }
        
}
