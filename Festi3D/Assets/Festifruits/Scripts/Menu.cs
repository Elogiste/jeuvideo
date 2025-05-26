using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject infosText; // 🟡 Référence au texte à afficher/cacher

    public void Jouer()
    {
        SceneManager.LoadScene("Niveau");
    }

    public void Quitter()
    {
        Debug.Log("Quitter le jeu...");
        Application.Quit();
    }

    public void AfficherInfos()
    {
        if (infosText != null)
        {
            // Active ou désactive le texte selon son état actuel
            infosText.SetActive(!infosText.activeSelf);
        }
    }
    
    public void MenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal"); 
    }
}
