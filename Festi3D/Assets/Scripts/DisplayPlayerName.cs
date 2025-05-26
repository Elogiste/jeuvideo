using UnityEngine;
using TMPro;

public class DisplayPlayerName : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;

    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Joueur");
        playerNameText.text = playerName;
    }
}
