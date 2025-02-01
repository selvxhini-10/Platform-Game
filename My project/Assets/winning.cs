using UnityEngine;
using UnityEngine.SceneManagement; 

public class WinFlag : MonoBehaviour{
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")) {
            Debug.Log("Player Wins!");
            WinGame();
        }}
void WinGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);



    }
}
