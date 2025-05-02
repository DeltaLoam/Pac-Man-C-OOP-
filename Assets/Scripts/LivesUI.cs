using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI pacmanLivesText;
    public TextMeshProUGUI ghostLivesText;

    public void UpdateLives(int pacmanLives, int ghostLives)
    {
        pacmanLivesText.text = $"PacMan Lives: {pacmanLives}";
        ghostLivesText.text = $"Ghost Lives: {ghostLives}";
    }
}
