using UnityEngine;

public class Gestion : MonoBehaviour
{
    public static Gestion instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private int totalBears = 0;
    private int bearsKilled = 0;
    private bool gameEnded = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Trouver tous les ours dans la scène par Tag
        totalBears = GameObject.FindGameObjectsWithTag("AI").Length;
    }

    public void RegisterBearDeath()
    {
        bearsKilled++;
        if (bearsKilled >= totalBears && !gameEnded)
        {
            WinGame();
        }
    }

    public void PlayerDied()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            losePanel.SetActive(true);
            Time.timeScale = 0f; // Pause le jeu
        }
    }

    private void WinGame()
    {
        gameEnded = true;
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
