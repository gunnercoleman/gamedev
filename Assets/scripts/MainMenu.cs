using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Tooltip("Name of the scene to load when Play is pressed. It must be listed in Build Settings.")]
    public string gameSceneName = "level_1";

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();


        // Application.Quit does nothing in the editor, so stop Play mode instead 
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
} 
