using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    
    public GameObject player;
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
            player.transform.position = new Vector3(-1.73f, -89.73f, -1);
        }
    }
}
