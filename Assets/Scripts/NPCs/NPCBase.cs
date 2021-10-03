using UnityEngine;
using UnityEngine.AI;

public class NPCBase : MonoBehaviour
{
    public bool activeAI;

    public Vector3 targetPosition;
    public bool moving;
    public float speed;

    public NavMeshSurface navMesh;
    public NavMeshAgent agent;
    public Animator animator;

    public virtual void CheckMovement()
    {
        if (moving)
        {
            if (transform.position == agent.destination)
            {
                moving = false;
                animator.SetBool("Move", false);
            }
        }
    }
    public virtual void SetMovement(Vector3 pos)
    {
        navMesh.BuildNavMesh();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = pos;
        moving = true;
        animator.SetBool("Move", true);
    }
}
