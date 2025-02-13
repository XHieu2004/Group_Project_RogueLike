using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneController : MonoBehaviour
{
    
    public GameObject player;
    public GameObject UI;

    void Awake()
    {   

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        SceneManager.sceneLoaded += PlayerOnSceneLoaded;
    }


    void PlayerOnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        
        Animator playerAnimator = player.GetComponent<Animator>();
        playerAnimator.Play(playerAnimator.GetLayerName(0) + ".Idle");
        WeaponController weaponController = player.GetComponentInChildren<WeaponController>();
        DontDestroyOnLoad(weaponController.weaponSlot1);
        DontDestroyOnLoad(weaponController.weaponSlot2);


        if(scene.name == "Level 2"){
            player.transform.position = new Vector3(-3.83f, -92.43f, -1);
        }
        else if(scene.name == "Boss"){
            player.transform.position = new Vector3(-1.2f, -99.4f, -3.366f);
        }
        // else if (scene.name == "GameScene") 
        // {
        // EventSystem existingEventSystem = FindObjectOfType<EventSystem>();
        // if (existingEventSystem != null)
        // {
        //     Destroy(existingEventSystem.gameObject);
        // }
        // }
    }
}
