using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 3f;
    private Vector3 target;

    void Start()
    {
        target = pointB.position; 
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            if (target == pointB.position)
            {
                target = pointA.position;
                Debug.Log("Switching to PointA");
            }
            else
            {
                target = pointB.position;
                Debug.Log("Switching to PointB");
            }
            Flip();
        }

        Debug.Log("Current Position: " + transform.position);
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; 
        transform.localScale = localScale;
    }
}
