using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; 
    public int currentScore = 0;
    public TextMeshProUGUI scoreText; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        UpdateScoreUI(); 
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        // if (scoreText != null)
        // {
        //     scoreText.text = "Score: " + currentScore.ToString();
        // }
        // else
        // {
        //     Debug.LogError("Something went wrong with Score");
        // }
        if (GameController.Instance != null){
            GameController.Instance.UpdateScore(currentScore); // Update score through GameManager
        }
    }
}