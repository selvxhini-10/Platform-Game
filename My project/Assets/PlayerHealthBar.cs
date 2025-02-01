using UnityEngine;
using UnityEngine.SceneManagement; 
public class PlayerHealth : MonoBehaviour
{
    public int maxHearts = 3;  
    private int currentHearts;
    public GameObject[] heartSprites;
    void Start(){
        currentHearts = maxHearts; 
        UpdateHearts();
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Enemy")) {
            TakeDamage();
        }
    }
    public void TakeDamage(){
        if (currentHearts > 0){
            currentHearts--;  
            UpdateHearts();
            if (currentHearts <= 0){
                Die();  
        }
    }}

    void UpdateHearts(){
        for (int i = 0; i < heartSprites.Length; i++){
            heartSprites[i].SetActive(i < currentHearts);  
        }
    }

    void Die(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
    }
}
