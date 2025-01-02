using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform bulletPoint;
    public Shooting shooting;
    public GameObject weaponSlot1;
    public GameObject weaponSlot2;
    public SpriteRenderer slot1Renderer;
    public SpriteRenderer slot2Renderer;
    public float bulletOffset = 1f;
    private Camera mainCamera;
    private List<ProjectTile> bullets = new();
    private List<GameObject> pickable = new();
    private CircleCollider2D pickrange;
    void Start()
    {
        mainCamera = Camera.main;
        pickrange = GetComponent<CircleCollider2D>();
        shooting = GetComponent<Shooting>();
        if(weaponSlot1 != null && weaponSlot2 != null){
            slot1Renderer = weaponSlot1.GetComponent<SpriteRenderer>();
            slot2Renderer = weaponSlot2.GetComponent<SpriteRenderer>();
            slot2Renderer.enabled = false;
            foreach (var bullet in shooting.bullets){
                bullets.Add(bullet.GetComponent<ProjectTile>());
            } 
            RuntimeAnimatorController controller = weaponSlot1.GetComponent<Weapon>().animatorController;
            foreach (var bullet in bullets){
                bullet.anim.runtimeAnimatorController = controller;
        }
        }
    }
    void Update()
    {
        SwapWeapon();
        PickWeapon();
        CarryWeapon();
        RotateWeaponToMouse();
        
    }

    void PickWeapon(){
        if(Input.GetKeyDown(KeyCode.F)){
            
            if(weaponSlot1 == null){
                (weaponSlot1, pickable[0]) = (pickable[0], weaponSlot1);
            }else if(weaponSlot2 == null){
                (weaponSlot2, pickable[0]) = (pickable[0], weaponSlot2);
            }else{
                (weaponSlot1, pickable[0]) = (pickable[0], weaponSlot1);
            }
            pickable.RemoveAll(item=>item == null);
            Debug.Log(pickable.Count);

            

            if(weaponSlot1 != null){
                slot1Renderer = weaponSlot1.GetComponent<SpriteRenderer>(); 
                slot1Renderer.enabled = true;
            }
            if(weaponSlot2 != null){
                slot2Renderer = weaponSlot2.GetComponent<SpriteRenderer>();
                slot2Renderer.enabled = false;
            }

            RuntimeAnimatorController controller = weaponSlot1.GetComponent<Weapon>().animatorController;
            foreach (var bullet in bullets){
                bullet.anim.runtimeAnimatorController = controller;
            }
            
        }
    }

    void SwapWeapon(){
        if(Input.GetKeyDown(KeyCode.R)){
            (weaponSlot2, weaponSlot1) = (weaponSlot1, weaponSlot2); // Swap weapon
            (slot1Renderer, slot2Renderer) = (slot2Renderer, slot1Renderer);
            slot1Renderer.enabled = true;
            slot2Renderer.enabled = false;
            RuntimeAnimatorController controller = weaponSlot1.GetComponent<Weapon>().animatorController;
            foreach (var bullet in bullets){
                bullet.anim.runtimeAnimatorController = controller;
            } 
        }

    }
    void CarryWeapon(){

        if(weaponSlot1 != null){
            weaponSlot1.transform.position = transform.position;
            
            bulletPoint.position = weaponSlot1.transform.position + bulletPoint.right * bulletOffset;
        }
        if(weaponSlot2 != null){
            
            weaponSlot2.transform.position = transform.position;
        
        }
    }
    void RotateWeaponToMouse(){
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; 

        
        Vector3 direction = mousePosition - weaponSlot1.transform.position;

        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        
        weaponSlot1.transform.rotation = Quaternion.Euler(0, 0, angle);
        bulletPoint.rotation  = Quaternion.Euler(0, 0,angle);
        if (angle > 90 || angle < -90)
            {
                weaponSlot1.transform.localScale = new Vector3(1, -1, 1); 
                
            }
            else
            {
                weaponSlot1.transform.localScale = new Vector3(1, 1, 1);
            }
        
    }
    void OnTriggerStay2D(Collider2D collider){
        
       
        Debug.Log("pickable: "+"  count:"+pickable.Count);
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Weapon") && other.gameObject != weaponSlot1 && other.gameObject != weaponSlot2){
            if(other.gameObject != weaponSlot1 && other.gameObject!= weaponSlot2){
                Debug.Log("get a weapon!");
                pickable.Add(other.gameObject);
            }
            
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.CompareTag("Weapon") && other.gameObject != weaponSlot1 && other.gameObject != weaponSlot2){
            if(other.gameObject != weaponSlot1 && other.gameObject!= weaponSlot2){
                Debug.Log("pickable: "+pickable.Count);
                pickable.Remove(other.gameObject);
            }
        }
    }
}
