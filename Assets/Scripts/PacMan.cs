using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public PacManStateMachine stateMachine;
    public State runState;
    public State huntState;
    public Material pacmanMaterial;
    public Renderer bodyRenderer;

    void Start()
    {
        stateMachine.ChangeState(runState, this);
    }

    void Awake()
{
    if (agent == null)
        agent = GetComponent<NavMeshAgent>();
}

    public void MoveToNearestDot()
{
    if (agent == null)
    {
        Debug.LogError("Enemy NavMeshAgent is missing!");
        return;
    }

    var smallDots = FindObjectsByType<Dot>(FindObjectsSortMode.None);
    var bigDots = FindObjectsByType<BigDot>(FindObjectsSortMode.None);

    Transform closest = null;
    float minDist = Mathf.Infinity;

    foreach (var dot in smallDots)
    {
        float dist = Vector3.Distance(transform.position, dot.transform.position);
        if (dist < minDist)
        {
            minDist = dist;
            closest = dot.transform;
        }
    }

    foreach (var dot in bigDots)
    {
        float dist = Vector3.Distance(transform.position, dot.transform.position);
        if (dist < minDist)
        {
            minDist = dist;
            closest = dot.transform;
        }
    }

    if (closest != null)
    {
        agent.SetDestination(closest.position);
    }
}





    [System.Obsolete]
    public void MoveToPlayer()
    {
        var player = FindObjectOfType<GhostController>();
        if (player != null)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    public void EnterHuntMode()
    {
        stateMachine.ChangeState(huntState, this);
    }

    public System.Collections.IEnumerator HuntCountdown()
    {
        yield return new WaitForSeconds(GameManager.Instance.huntDuration);
        stateMachine.ChangeState(runState, this);
    }

    public void SetAppearance(Color color)
    {
        if (bodyRenderer != null)
        {
            bodyRenderer.material.color = color;
        }
    }

    void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        if (stateMachine.currentState == huntState)
        {
            // PacMan hunts the player
            GameManager.Instance.GhostHit();
        }
        else
        {
            // Player catches PacMan
            GameManager.Instance.PacmanHit();
        }
    }
}

}