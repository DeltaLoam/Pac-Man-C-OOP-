using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class GhostController : MonoBehaviour
{
    public float speed = 5f;
    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        moveDirection = new Vector3(h, 0, v).normalized * speed;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var pacman = other.GetComponent<Enemy>();
            if (pacman.stateMachine.currentState == pacman.huntState)
            {
                GameManager.Instance.GhostHit();
            }
            else
            {
                GameManager.Instance.PacmanHit();
            }
        }
    }
}