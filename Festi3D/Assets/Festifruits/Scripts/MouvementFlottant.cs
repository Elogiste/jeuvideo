using UnityEngine;

public class MouvementFlottant : MonoBehaviour
{
    public Transform[] Points;
    public int currentPointIndex;
    public float speed;

    

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(Points[currentPointIndex].position, transform.position) < 0.1f){
            currentPointIndex++;
            if(currentPointIndex >= Points.Length){
                currentPointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, Points[currentPointIndex].position,Time.deltaTime * speed);
    }
}
