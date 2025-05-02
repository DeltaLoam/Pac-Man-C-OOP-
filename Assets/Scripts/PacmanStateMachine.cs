using UnityEngine;

public class PacManStateMachine : MonoBehaviour
{
    public State currentState;

    private Enemy enemy; // ✅ Moved inside the class

    public void ChangeState(State newState, Enemy newEnemy)
    {
        currentState?.Exit(enemy);
        currentState = newState;
        enemy = newEnemy; // Store the reference
        currentState.Enter(enemy);
    }

    void Update()
    {
        if (currentState == null)
        {
            Debug.LogWarning("StateMachine has no current state.");
            return;
        }

        if (enemy == null)
        {
            Debug.LogError("StateMachine has no enemy assigned!");
            return;
        }

        currentState.Tick(enemy);
    }
}
