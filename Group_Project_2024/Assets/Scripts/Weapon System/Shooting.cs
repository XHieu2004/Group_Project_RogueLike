using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform bulletPoint;
    public GameObject[] bullets;
    public float shootingCooldown;
    private float cooldownTimer = 0;
    public AudioSource audioSource; 

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && cooldownTimer > shootingCooldown){
            Shoot();
        }
        cooldownTimer += Time.deltaTime;
    }
    private void Shoot(){
        int i = FindBullet();
        bullets[i].transform.position = bulletPoint.position;
        Vector2 direction = bulletPoint.right;
        
        bullets[i].GetComponent<ProjectTile>().SetDirection(direction); 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bulletPoint.rotation  = Quaternion.Euler(0, 0,angle);
        if (angle > 90 || angle < -90)
        {
            bullets[i].transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            bullets[i].transform.localScale = new Vector3(1, 1, 1);
        }
        cooldownTimer = 0f;
        if (audioSource != null){
            audioSource.Play();
        }

    }
    private int FindBullet()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}