using UnityEngine;

public class DetectInput : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                isPaused = false;
            }
        }
    }
}
