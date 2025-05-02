using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private LivesUI livesUI;

    public int pacmanLives = 3;
    public int ghostLives = 3;

    public float huntDuration = 7f;

    private int remainingDots;

    public Transform pacmanSpawn;
    public Transform ghostSpawn;

    void Start()
    {
        livesUI = FindFirstObjectByType<LivesUI>();
        UpdateUI();
        
        // Count small dots only — big dots handled separately
        remainingDots = FindObjectsByType<Dot>(FindObjectsSortMode.None).Length;

        Debug.Log($"Dots in level: {remainingDots}");
    }

    private void UpdateUI()
    {
        // Ensure UI is updated with the current lives
        livesUI?.UpdateLives(pacmanLives, ghostLives);
    }

    // Called when any dot is collected by PacMan
    public void DotCollected()
    {
        remainingDots--;

        Debug.Log($"Dot collected! Dots remaining: {remainingDots}");

        if (remainingDots <= 0)
        {
            Debug.Log("PacMan collected all dots. Player (Ghost) loses.");
            LoseGame();
        }
    }

    // Called when PacMan collects a Power Dot
    public void PowerDotCollected()
    {
        Debug.Log("Power Dot collected! PacMan is now hunting.");

        Enemy pacman = FindFirstObjectByType<Enemy>();
        if (pacman != null)
        {
            pacman.EnterHuntMode();
            StartCoroutine(pacman.HuntCountdown());
        }
    }

    // Called when PacMan is hit by the Ghost
    public void PacmanHit()
    {
        pacmanLives--;
        UpdateUI();

        if (pacmanLives <= 0)
            WinGame(); // Ghost wins
        else
            RespawnPacman();
    }

    // Called when Ghost is hit by PacMan
    public void GhostHit()
    {
        ghostLives--;
        UpdateUI();

        if (ghostLives <= 0)
            LoseGame(); // PacMan wins
        else
            RespawnGhost();
    }

    // Respawn PacMan to its spawn point
    private void RespawnPacman()
    {
        Enemy pacman = FindFirstObjectByType<Enemy>();
        if (pacman != null && pacmanSpawn != null)
        {
            pacman.transform.position = pacmanSpawn.position;
            pacman.agent.Warp(pacmanSpawn.position); // Reset NavMeshAgent position
            pacman.SetAppearance(Color.yellow); // Optional: Reset appearance to normal
        }
    }

    // Respawn Ghost to its spawn point
    private void RespawnGhost()
    {
        GhostController ghost = FindFirstObjectByType<GhostController>();
        if (ghost != null && ghostSpawn != null)
        {
            ghost.transform.position = ghostSpawn.position;
            // Reset Ghost appearance if needed
        }
    }

    // End the game with PacMan winning
    private void WinGame()
    {
        Debug.Log("You win!");
        // TODO: Show UI / restart the scene after winning
        RestartScene();
    }

    // End the game with Ghost winning
    private void LoseGame()
    {
        Debug.Log("You lose!");
        // TODO: Show UI / restart the scene after losing
        RestartScene();
    }

    // Restart the scene (for both win and lose)
    private void RestartScene()
    {
        // Optionally, you could show a Game Over screen before restarting.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
