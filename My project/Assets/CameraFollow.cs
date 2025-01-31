using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  
    public float followSpeed = 5f; 
    public float xOffset = 2f; 

    void Update()
    {
        if (player != null)
        {
            Vector3 targetPosition = new Vector3(player.position.x + xOffset, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}