using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseButton;  // Référence au bouton de retour au menu

    private bool isPaused = false;

    void Update()
    {
        // Vérifie si le joueur appuie sur la touche ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Fonction pour activer/désactiver la pause
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause le jeu
            pauseButton.SetActive(true);  // Affiche le bouton de retour au menu
        }
        else
        {
            Time.timeScale = 1f; // Reprend le jeu
            pauseButton.SetActive(false);  // Cache le bouton de retour au menu
        }
    }

    // Fonction pour revenir au menu
    public void RetourMenu()
    {
        Time.timeScale = 1f; // Reprend le jeu (si jamais le jeu a été mis en pause en retournant au menu)
        SceneManager.LoadScene("Menu"); // Charge la scène du menu principal
    }

    public void Jouer3D()
    {
        SceneManager.LoadScene("Scene");
    }
}
