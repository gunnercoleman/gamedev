using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [Tooltip("Name of the scene to load. It must be listed in Build Settings.")]
    [SerializeField] string nextSceneName = "level_2";

    public void Hello()
    {
        Debug.Log("Hello from LevelLoader");
    }

    public void LoadNextLevel()
    {
        // ScoreManager pauses the game when the level is complete, 

        // so unpause before loading 
        Debug.Log("LoadNextLevel clicked, loading " + nextSceneName);
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName);
    }
}