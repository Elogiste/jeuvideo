using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deplacement : MonoBehaviour
{
    Animator animMomo; // déclaration du contrôleur
    Rigidbody2D rb;

    public Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        animMomo = GetComponent<Animator>(); // mentionne quel contrôleur
        rb = GetComponent<Rigidbody2D>();
        animator = animMomo;
    }

    void Update()
    {
        // Test de Time.deltaTime
        // print("time/frame" + Time.deltaTime);

        float direction = Input.GetAxis("Horizontal");

        // Déplacement du personnage à gauche et à droite
        if (direction != 0)
        {
            // On déplace le personnage en fonction de l'entrée utilisateur
            transform.Translate(Vector3.right * direction * Time.deltaTime * 5);

            // Utilisation de flipX pour inverser l'orientation sans toucher à la taille du personnage
            GetComponent<SpriteRenderer>().flipX = direction < 0;

            // Animation (si applicable)
            animMomo.SetFloat("Speed", Mathf.Abs(direction));

            animator.SetBool("running", true);
        }else{
            animator.SetBool("running", false);
        }
    }

    void FixedUpdate()
    {
        float direction = Input.GetAxis("Horizontal");

        // Mise à jour de la vélocité du Rigidbody
        rb.linearVelocity = new Vector2(direction * 10, rb.linearVelocity.y);
    }

    public bool canAttack()
    {
        float direction = Input.GetAxis("Horizontal");
        return direction == 0;
    }
}
