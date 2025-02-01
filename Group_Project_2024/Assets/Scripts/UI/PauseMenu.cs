using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button pauseButton;
    public GameObject pausePanel;
    public Button resumeButton;
    public Button exitButton;

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    public void OnPauseButtonClicked()
    {
        // PauseGame();
        // Debug.Log("Game Paused!");
        // isPaused = true;
   
        // Debug.Log("Button Cliked!");
        if (GameController.Instance != null)
        {
            GameController.Instance.PauseGame();
        }
    }
    public void ExitPauseMenu(){
        if(isPaused == true){
            ResumeGame();
            Debug.Log("Exit Pause Menu!");
        }
    }

    // void PauseGame()
    // {
    //     Time.timeScale = 0f; 
    //     if (pausePanel != null)
    //     {
    //         pausePanel.SetActive(true); 
    //     }
    // }

    void ResumeGame()
    {
        Time.timeScale = 1f; 
        if (pausePanel != null)
        {
            pausePanel.SetActive(false); 
        }
    }

    public void OnResumeButtonClicked()
    {
        // Debug.Log("Resume Button Clicked");
        // ResumeGame(); 
         if (GameController.Instance != null){
            GameController.Instance.ResumeGame();
        }
    }

    public void OnExitButtonClicked()
    {
        // Debug.Log("Exit Button Clicked");
        if (GameController.Instance != null){
            GameController.Instance.ExitGame();
        }
    }
}