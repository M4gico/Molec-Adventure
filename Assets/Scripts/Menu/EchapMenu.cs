using UnityEngine;
using UnityEngine.SceneManagement;

public class EchapMenu : MonoBehaviour
{
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
