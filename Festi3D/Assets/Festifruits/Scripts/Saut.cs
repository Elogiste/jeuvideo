using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saut : MonoBehaviour
{
    private Rigidbody2D rb; // d�claration d'un rigidbody
    public float jumpForce; // d�claration de la variable de saut 
    bool testSaut = false;
 
    // Start is called before the first frame update
    void Start()
    {
        //print("la valeur de saut est " + testSaut);
        rb = GetComponent<Rigidbody2D>(); // stokage de la composante rigidbody
    }

    // Update is called once per frame
    void Update()
    {

        //print("time btw frame" + Time.deltaTime);

        
      

    }
    private void FixedUpdate()
    {
        //print("time btw frame" + Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.UpArrow) && testSaut==false) // si la touche espace est enfonc�e
        {
            rb.linearVelocity = Vector2.up * jumpForce; // on ajoute une force � l'objet
            testSaut = true; 

        }
        if (Mathf.Abs(rb.linearVelocity.y)<0.01) // une fois que la v�locit� s'approche bcp de zero, on va mettre la vaiable testSaut � false 
        {
            testSaut = false;
        }
    }

}
