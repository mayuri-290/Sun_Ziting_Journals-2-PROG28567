using UnityEngine;

public class player : MonoBehaviour
{
    public Collider2D groundCollider;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.IsTouching(groundCollider))
            Debug.Log("Player is on the ground");
        else
            Debug.Log("Player is in the air");
    
    }
}
