using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float speed = 50f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * speed; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
