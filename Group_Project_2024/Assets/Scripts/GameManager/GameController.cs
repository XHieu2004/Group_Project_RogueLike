using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("Player Settings")]
    public GameObject player;
    public Transform playerSpawnPoint; 

    [Header("UI Elements")]
    public GameObject UI;
    public GameObject pauseMenuCanvas;
    public GameObject retryMenuCanvas;
    public TextMeshProUGUI scoreText;

    [Header("Audio")]
    public AudioSource gameTrack;
    public AudioSource gameOverTrack;

    [Header("Scene Management")]
    public string firstLevelName = "Level1";

    public GameObject PlayerInstance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(UI);
    }

    private void Start()
    {
        
        if (gameTrack != null)
        {
            gameTrack.Play();
        }

        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }
        if (retryMenuCanvas != null)
        {
            retryMenuCanvas.SetActive(false);
        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        playerSpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        // Find and assign the player if it exists in the scene.
        PlayerInstance = GameObject.FindGameObjectWithTag("Player");

        // If no player is found and we have a prefab, spawn the player.
        if (PlayerInstance == null && player != null)
        {
            if (playerSpawnPoint == null)
            {
                Debug.LogError("Player spawn point is not set in GameManager.");
            }
        }
        else if (PlayerInstance != null)
        {
            // Initialize the player if found.
            InitializePlayer();
        }
        Animator playerAnimator = player.GetComponent<Animator>();
        playerAnimator.Play(playerAnimator.GetLayerName(0) + ".Idle");
        WeaponController weaponController = player.GetComponentInChildren<WeaponController>();
        DontDestroyOnLoad(weaponController.weaponSlot1);
        DontDestroyOnLoad(weaponController.weaponSlot2);


        if(scene.name == "Level 2" || scene.name == "Boss"){
            player.transform.position = playerSpawnPoint.transform.position;
        }
        // else if(scene.name == "Boss"){
        //     playerSpawnPoint.transform.position = new Vector3(-1.2f, -99.4f, -3.366f);
        // }
    }

    private void InitializePlayer()
    {

    }

    public void UpdateScore(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + newScore;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(true);
        }
        // Pause audio or other game elements if needed
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }
        // Resume audio or other game elements if needed
    }

    public void ShowRetryMenu()
    {
        if (retryMenuCanvas != null)
        {
            retryMenuCanvas.SetActive(true);
        }
        if (gameTrack != null)
        {
            gameTrack.Stop();
        }
        if (gameOverTrack != null)
        {
            gameOverTrack.Play();
        }
    }

    public void RetryLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadNextLevel(int levelName)
    {
        SceneManager.LoadSceneAsync(levelName);
    }
}