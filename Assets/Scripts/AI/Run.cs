using UnityEngine;

[CreateAssetMenu(menuName = "State/RUN")]
public class RUN : State
{
    public float dangerDistance = 6f; // How close the player needs to be before PacMan flees

    public override void Enter(Enemy enemy)
    {
        enemy.SetAppearance(Color.yellow);
    }

    public override void Tick(Enemy enemy)
    {
        GhostController ghost = GameObject.FindFirstObjectByType<GhostController>();

        if (ghost == null || enemy.agent == null) return;

        float distanceToGhost = Vector3.Distance(enemy.transform.position, ghost.transform.position);

        if (distanceToGhost < dangerDistance)
        {
            // Flee in the opposite direction of the ghost
            Vector3 fleeDirection = (enemy.transform.position - ghost.transform.position).normalized;
            Vector3 fleeTarget = enemy.transform.position + fleeDirection * 5f; // Flee 5 units away

            enemy.agent.SetDestination(fleeTarget);
        }
        else
        {
            enemy.MoveToNearestDot();
        }
    }

    public override void Exit(Enemy enemy) { }
}
