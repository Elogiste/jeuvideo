using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpingPower;
    public LayerMask groundLayer;
    public Transform groundcheck;
    float horizontal;
    float vertical;
    SpriteRenderer sr;
    Animator animator;
    bool canClimb = false;

    public void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, canClimb ? vertical * speed : rb.linearVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        animator.SetBool("IsGrounded", IsGrounded());

        if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    public void Move(InputAction.CallbackContext context){
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
        if(horizontal< 0){
            sr.flipX = true;
        }else{
             sr.flipX = false;
        }
    }

    public void Jump(InputAction.CallbackContext context){
        if(context.performed && IsGrounded()){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }
    }

    bool IsGrounded(){
        return Physics2D.OverlapCapsule(groundcheck.position, new Vector2(0.25f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Climb")){
            canClimb = true;
        }
        
    }

     private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Climb")){
            canClimb = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
