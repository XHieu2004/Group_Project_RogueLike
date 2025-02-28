using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevel : MonoBehaviour
{
    public int nextScene;
    
    private void OnTriggerEnter2D(Collider2D other){
        // if(other.CompareTag("Player")){
        //     SceneManager.LoadSceneAsync(nextScene);
        // }
        if (GameController.Instance != null && other.CompareTag("Player")){
            GameController.Instance.LoadNextLevel(nextScene); 
        }
        
    }
}
