using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class RetryMenu : MonoBehaviour
{
    public GameObject retryMenu;
    public AudioSource gameTrack;
    public AudioSource gameOver;
    private GameObject player; 

    void Start()
    {
        retryMenu = GameObject.FindGameObjectWithTag("RetryMenu");
        retryMenu.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player"); // Find the Player on start
        if (player == null)
        {
            Debug.LogError("Player GameObject with 'Player' tag not found!");
        }
    }

    public void ShowRetryMenu()
    {
        // retryMenu.SetActive(true);
        // gameTrack.Stop();
        // gameOver.Play();
        if (GameController.Instance != null)
        {
            GameController.Instance.ShowRetryMenu(); // Use GameManager's method
        }
    }

    public void RetryGame()
    {
        // Time.timeScale = 1;
        // if (player != null)
        // {
        //     PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        //     if (playerHealth != null)
        //     {
        //         playerHealth.ResetHP();
        //     }
        //     else
        //     {
        //         Debug.LogError("PlayerHealth component not found on the Player GameObject!");
        //     }
        // }
        // else
        // {
        //     Debug.LogError("Player GameObject is null!");
        // }

        // retryMenu.SetActive(false);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (GameController.Instance != null){
            GameController.Instance.RetryLevel(); // Use GameController's method
        }
    }

    public void ExitGame()
    {
        // Application.Quit();
        // retryMenu.SetActive(false);
        // Debug.Log("Exit successed?");
        if (GameController.Instance != null){
            GameController.Instance.ExitGame(); // Use GameController's method
        }
    }
}