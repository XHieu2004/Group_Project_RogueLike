using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScene : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other){
        // if(other.CompareTag("Player")){
        //     SceneManager.LoadSceneAsync(3);
        // }
        if (other.CompareTag("Player"))
        {
            if (GameController.Instance != null)
            {
                GameController.Instance.LoadNextLevel(3);
            }
        }
        
    }
}

