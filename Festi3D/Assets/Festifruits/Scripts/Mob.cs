using UnityEngine;

public class Mob : MonoBehaviour
{
    public float speed = 2f;
    public Transform pointA;
    public Transform pointB;

    private Rigidbody2D rb;
    private Vector3 targetPosition;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        targetPosition = pointB.position;

        // Empêche la rotation physique
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        float directionX = Mathf.Sign(targetPosition.x - transform.position.x);
        rb.velocity = new Vector2(directionX * speed, rb.velocity.y);

        // Change la direction si proche du point
        if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.1f)
        {
            targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
        }

        // Inverse le sprite selon la direction
        sr.flipX = directionX < 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contact = collision.GetContact(0);
            float contactY = contact.point.y;

            // Le joueur saute sur le mob
            if (contactY > transform.position.y + 0.5f)
            {
                PlayerCollision pc = collision.gameObject.GetComponent<PlayerCollision>();
                if (pc != null)
                {
                    pc.EnnemiDetruit(); // Décrémente le compteur
                }

                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    playerRb.velocity = new Vector2(playerRb.velocity.x, 10f); // rebond
                }

                Destroy(gameObject); // détruit le mob
            }
            else
            {
                // Le mob blesse le joueur
                PlayerCollision pc = collision.gameObject.GetComponent<PlayerCollision>();
                if (pc != null)
                {
                    pc.TakeDommages(1); // perd 1 vie
                }
            }
        }
    }
}
