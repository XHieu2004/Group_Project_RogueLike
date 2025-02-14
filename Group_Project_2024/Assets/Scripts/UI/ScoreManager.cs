using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{ 
    public TextMeshProUGUI scoreText; 

    void Start()
    {
        UpdateScoreUI(); 
    }

    public void AddScore(int points)
    {
        GameController.Instance.currentScore += points;
        UpdateScoreUI();
    }
    public void ResetScore(){
        GameController.Instance.currentScore = 0;
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
            if (scoreText != null)
            {
                scoreText.text = "Score: " + GameController.Instance.currentScore;
            }// Update score through GameManager
        }
    }

}