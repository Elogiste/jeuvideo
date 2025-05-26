using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag=="tourner")
        {
            transform.Rotate(0, 1, 0);
            
            transform.localScale = Vector3.one/2;

            transform.localScale = Vector3.one*2;

        }
    }
}
