using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = target.position + offset;
        
    }
}
