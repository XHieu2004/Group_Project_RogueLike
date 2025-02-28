using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public GameObject loadingScreen;
    public Image LoadingBarFill;
    public int currentScore;
    public GameObject victoryCanvas;

    [Header("Audio")]
    public AudioSource gameTrack;
    public AudioSource gameOverTrack;
    public AudioSource victoryTrack;

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
        if(victoryTrack != null){
            victoryTrack.Stop();
        }
        
        if (gameTrack != null){
            gameTrack.Play();
        }

        if (pauseMenuCanvas != null){
            pauseMenuCanvas.SetActive(false);
        }
        if (retryMenuCanvas != null){
            retryMenuCanvas.SetActive(false);
        }
        if(loadingScreen!= null){
            loadingScreen.SetActive(false);
        }
        if(victoryCanvas != null){
            victoryCanvas.SetActive(false);
        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        
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


        if(scene.name == "Level 2" || scene.name == "Boss" || scene.name == "GameScene"){
            player.transform.position = playerSpawnPoint.transform.position;
            victoryCanvas.SetActive(false);
        }
        // else if(scene.name == "Boss"){
        //     playerSpawnPoint.transform.position = new Vector3(-1.2f, -99.4f, -3.366f);
        // }
        if(scene.name == "Victory Scene"){
            gameTrack.Stop();
            victoryTrack.Play();
            victoryCanvas.SetActive(true);
        }
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

    public void UpdateScoreUI(){
        scoreText.text = "Score: " + currentScore;
    }

    public void RetryGame(){
        currentScore = 0;
        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ResetHP();
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on the Player GameObject!");
            }
        }
        else
        {
            Debug.LogError("Player GameObject is null!");
        }
        Time.timeScale = 1;
        EventSystem existingEventSystem = FindObjectOfType<EventSystem>();
        if (existingEventSystem != null)
        {
            Destroy(existingEventSystem.gameObject);
        }
        if (pauseMenuCanvas != null){
        pauseMenuCanvas.SetActive(false);
        }
        if (retryMenuCanvas != null){
            retryMenuCanvas.SetActive(false);
        }
        if(victoryCanvas != null){
            victoryCanvas.SetActive(false);
        }
        StartCoroutine(LoadSceneAsync(2));
    }

    public void RetryLevel()
    {
        currentScore = 0;
        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ResetHP();
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on the Player GameObject!");
            }
        }
        else
        {
            Debug.LogError("Player GameObject is null!");
        }
        Time.timeScale = 1;
        // EventSystem existingEventSystem = FindObjectOfType<EventSystem>();
        // if (existingEventSystem != null)
        // {
        //     Destroy(existingEventSystem.gameObject);
        // }
        if (pauseMenuCanvas != null){
        pauseMenuCanvas.SetActive(false);
        }
        if (retryMenuCanvas != null){
            retryMenuCanvas.SetActive(false);
        }
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadNextLevel(int levelName)
    {
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1));
    }
    private IEnumerator LoadSceneAsync(int sceneIndex){
         if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        // Update loading bar
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            if (LoadingBarFill != null)
            {
                LoadingBarFill.fillAmount = progressValue;
            }
            yield return null;
        }

        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
        }
    }
}
