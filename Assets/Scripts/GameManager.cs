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
        livesUI?.UpdateLives(pacmanLives, ghostLives);
    }


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

    public void PowerDotCollected()
    {
        Debug.Log("Power Dot collected! PacMan is now hunting.");

        Enemy pacman = FindFirstObjectByType<Enemy>()
;
        if (pacman != null)
        {
            pacman.EnterHuntMode();
            StartCoroutine(pacman.HuntCountdown());
        }
    }

    public void PacmanHit()
{
    pacmanLives--;
    UpdateUI();

    if (pacmanLives <= 0)
        WinGame();
    else
        RespawnPacman();
}

public void GhostHit()
{
    ghostLives--;
    UpdateUI();

    if (ghostLives <= 0)
        LoseGame();
    else
        RespawnGhost();
}


    private void RespawnPacman()
{
    Enemy pacman = FindFirstObjectByType<Enemy>()
;
    if (pacman != null && pacmanSpawn != null)
    {
        pacman.transform.position = pacmanSpawn.position;
        pacman.agent.Warp(pacmanSpawn.position); // Reset NavMeshAgent position
    }
}

private void RespawnGhost()
{
    GhostController ghost = FindFirstObjectByType<GhostController>();
    if (ghost != null && ghostSpawn != null)
    {
        ghost.transform.position = ghostSpawn.position;
    }
}



    private void WinGame()
    {
        Debug.Log("You win!");
        // TODO: Show UI / restart
        RestartScene();
    }

    private void LoseGame()
    {
        Debug.Log("You lose!");
        // TODO: Show UI / restart
        RestartScene();
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
