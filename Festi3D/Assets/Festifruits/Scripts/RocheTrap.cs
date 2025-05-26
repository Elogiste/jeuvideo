using UnityEngine;

public class RocheTrap : MonoBehaviour
{
    public GameObject connectedRoche;
    public Collider2D colliderToActivate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject.GetComponent<SpriteRenderer>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            connectedRoche.AddComponent<Rigidbody2D>();
            colliderToActivate.enabled = true;
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
