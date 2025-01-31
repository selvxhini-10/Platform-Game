using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 1f;
    private Transform target;
    private bool movingToPointB = true;

    void Start()
    {
        target = pointB; 
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            if (movingToPointB)
            {
                target = pointA;
                movingToPointB = false;
            }
            else
            {
                target = pointB;
                movingToPointB = true;
            }
            Flip();
        }
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; 
        transform.localScale = localScale;
    }
}
