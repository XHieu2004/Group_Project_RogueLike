using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryMenu : MonoBehaviour
{
    public GameObject retryMenu;
    public AudioSource gameTrack;
    public AudioSource gameOver;
    void Start(){
        retryMenu.SetActive(false);
    }

    public void ShowRetryMenu(){
        retryMenu.SetActive(true);
        gameTrack.Stop();
        gameOver.Play();
    }
     public void RetryGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame(){
        Application.Quit();
    }
}
