using UnityEngine;

[CreateAssetMenu(menuName = "State/HUNT")]
public class HUNT : State
{
    public override void Enter(Enemy enemy)
    {
        enemy.SetAppearance(Color.red);
        enemy.StartCoroutine(enemy.HuntCountdown());
    }

    public override void Tick(Enemy enemy)
    {
        GhostController ghost = GameObject.FindFirstObjectByType<GhostController>();
if (ghost != null && enemy.agent != null)
{
    enemy.agent.SetDestination(ghost.transform.position);
}

    }

    public override void Exit(Enemy enemy) {}
}