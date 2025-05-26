using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainPrincipal : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TextMeshProUGUI errorText; // champ pour afficher une erreur

    public void Play2D()
    {
        if (IsNameValid())
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void Play3D()
    {
        if (IsNameValid())
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // pour arrêter dans l'éditeur
        #endif
    }

    private bool IsNameValid()
    {
        string playerName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            if (errorText != null)
                errorText.text = "Veuillez entrer votre nom.";
            return false;
        }

        PlayerPrefs.SetString("PlayerName", playerName); // stocke le nom du joueur
        return true;
    }
}
