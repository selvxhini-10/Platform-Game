using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spike")) 
        {
            Die();
        }
    }
//dies
    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }



}

