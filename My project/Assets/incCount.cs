using UnityEngine;

public class PlasticCanCounter : MonoBehaviour{
    public Sprite[] numberSprites;
    private SpriteRenderer spriteRenderer;
    private int canCount = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateCounter();
    }

    public void AddCan()
    {
        if (canCount < numberSprites.Length - 1)
        {
            canCount++;
            UpdateCounter();
        }
    }

    void UpdateCounter()      {
        spriteRenderer.sprite = numberSprites[canCount]; 
    }
}

