using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    public int vie = 3;
    public int oranges = 0;
    bool isDead = false;
    bool hasWon = false;

    int totalOranges = 0;
    int totalEnnemis = 0;

    // UI
    public TextMeshProUGUI orangeText;
    public TextMeshProUGUI enemyText;
    public TextMeshProUGUI lifeText;

    // UI Canvas for victory/defeat
    public GameObject resultCanvas;
    public TextMeshProUGUI resultText;
    public Button backButton;

    void Start()
    {
        totalOranges = GameObject.FindGameObjectsWithTag("Orange").Length;
        totalEnnemis = FindObjectsOfType<Mob>().Length;
        UpdateUI();
        resultCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Piquet"))
        {
            TakeDommages(1); // Perdre 1 vie si collision avec un piquet
        }

        if (collision.CompareTag("Orange"))
        {
            oranges++;
            Destroy(collision.gameObject); // Ramasser l'orange
            UpdateUI();
        }

        if (collision.CompareTag("EndLevel"))
        {
            CheckFinDeNiveau(); // Vérifier la fin du niveau
        }
    }

    public void EnnemiDetruit()
    {
        totalEnnemis--;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (orangeText != null) orangeText.text = "🟠 Oranges : " + oranges + " / " + totalOranges;
        if (enemyText != null) enemyText.text = "💀 Ennemis : " + totalEnnemis;
        if (lifeText != null) lifeText.text = "❤️ Vies : " + vie + " / 3";
    }

    void CheckFinDeNiveau()
    {
        // Vérifier si toutes les oranges sont collectées et tous les ennemis sont détruits
        if (!hasWon && oranges >= totalOranges && totalEnnemis <= 0)
        {
            hasWon = true;
            ShowResult("Victoire");
        }
        else if (vie <= 0 && !isDead)
        {
            isDead = true;
            StartDeathAnimation(); // Lancer l'animation de mort si toutes les vies sont perdues
        }
    }

    void ShowResult(string result)
    {
        resultCanvas.SetActive(true);
        resultText.text = result;
        backButton.gameObject.SetActive(true);
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(RetourAuMenu);

        // Mettre le jeu en pause immédiatement après le résultat
        Time.timeScale = 0f; // ⏸ Pause totale
    }

    void RetourAuMenu()
    {
        Time.timeScale = 1f; // 🔁 On remet le jeu à la normale
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void TakeDommages(int dommage)
    {
        if (isDead) return; // Si le joueur est déjà mort, on ne fait rien

        vie -= dommage;  // Décrémenter les vies du joueur
        UpdateUI();  // Mettre à jour l'UI avec les nouvelles vies

        if (vie > 0)
        {
            // Le joueur a encore des vies, on continue le jeu sans animation de mort
            isDead = false;
        }
        else
        {
            // Le joueur a perdu toutes ses vies, on joue l'animation de mort
            isDead = true;
            StartDeathAnimation(); // Appeler l'animation de mort
        }
    }

    void StartDeathAnimation()
    {
        // Vérifie si l'Animator est bien attaché à l'objet
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            // Déclencher l'animation de mort
            animator.SetTrigger("Die");  // Le trigger "Die" doit être présent dans l'Animator
        }
        else
        {
            Debug.LogError("Animator non trouvé sur l'objet joueur!");
        }

        // Appeler ShowResult immédiatement après que l'animation de mort commence
        ShowResult("Défaite");
    }

    void RestartLevel()
    {
        // Redémarre le niveau après que l'animation de mort soit terminée
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (transform.position.y < -10 && !isDead)
        {
            TakeDommages(1);  // Perdre 1 vie si le joueur tombe
        }

        CheckFinDeNiveau();  // Vérifier la fin du niveau
    }
}
