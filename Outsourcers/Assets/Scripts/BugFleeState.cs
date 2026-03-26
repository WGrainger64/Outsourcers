using UnityEngine;

public class BugFleeState : StateMachineBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    Transform player;

    public float fleeSpeed = 6f;

    public float stopFleeingDistance = 31;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Initiation

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.speed = fleeSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 playerPos = new Vector3(player.position.x+50, player.position.y, player.position.z+50);
        agent.SetDestination(playerPos);
        animator.transform.LookAt(player);

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        //Checking if the agent should stop chasing

        if (distanceFromPlayer > stopFleeingDistance)
        {
            animator.SetBool("isFleeing", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
