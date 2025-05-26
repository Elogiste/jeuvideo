using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class MouvementJoueur : MonoBehaviour
{

    private float horizontal;
    private float speed = 16f;
    private float sautP = 24f;
    private bool faceR = true;
    SpriteRenderer spriteRenderer;
    public fruitCompte fc;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        FlipSprite();

        if(Input.GetButtonDown("Jump") && surSol())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, sautP);
        }

        if (Input.GetButtonDown("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    private bool surSol()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private void Flip()
    {
        if(faceR && horizontal < 0f || !faceR && horizontal > 0f)
        {
            faceR = !faceR;
            Vector3 localScale = transform.localScale;
            localScale.x = -1f;;
            transform.localScale = localScale;
        }
    }

    void FlipSprite()
    {
        float direction = Mathf.Sign(Input.GetAxis("Horizontal"));
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit")){
            Destroy(collision.gameObject);
            fc.fruitsCompte++;
        }
        if (collision.gameObject.CompareTag("But"))
        {
            
        }
    }
}
