using UnityEngine;

public class BoxThrow : MonoBehaviour
{
    public float throwForce = 10f;
    private bool isHeld = false;
    private bool playerTouching = false;
    private Transform player;
    private Rigidbody2D rb;
    private float Gravity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Gravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(player==null)
        {
            return;
        }

        if(playerTouching && Input.GetKeyDown(KeyCode.H))
        {
            isHeld = true;
            rb.gravityScale = 0f;
            rb.linearVelocity = Vector2.zero;
        }

        if(isHeld)
        {
            transform.position = player.position +  Vector3.up * 0.5f;
        }

        if(isHeld&&Input.GetKeyUp(KeyCode.H))
        {
            isHeld = false;
            rb.gravityScale = Gravity;
            rb.AddForce(Vector2.up * throwForce, ForceMode2D.Impulse);
        }   
    }

    void OnCollisionEnter2D(Collision2D collision) //if the player is close enough to the box, it will pick up. 
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player = collision.collider.transform;
            playerTouching = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)//when the player moves away from the box, it will drop the box, bool become false. 
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerTouching = false;
            if(isHeld)
            {
                isHeld = false;        
            }
        }
    }
}