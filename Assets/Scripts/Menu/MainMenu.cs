using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneName;

    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
