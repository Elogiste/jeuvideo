using UnityEngine;
using UnityEngine.UI;

public class fruitCompte : MonoBehaviour
{
    public int fruitsCompte;
    public Text fruitText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fruitText.text = fruitsCompte.ToString();
    }
}
