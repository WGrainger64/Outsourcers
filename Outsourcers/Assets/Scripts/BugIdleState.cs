using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugIdleState : StateMachineBehaviour
{
    float timer;
    public float idleTime = 0f;

    Transform player;

    public float detectionAreaRadius = 18f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Transition to Patrol
        timer += Time.deltaTime;
        if (timer > idleTime)
        {
            animator.SetBool("isPatrolling", true);
        }

        //Transition to Chase State or Flee State
        //get a random value to decide whether to chase or flee 
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionAreaRadius)
        {
            int randomNum = Random.Range(0, 2);
            if (randomNum == 0)
            {
               
                animator.SetBool("isChasing", true);
                animator.SetBool("isFleeing", false);

            }
            else if (randomNum == 1)
            {
                
                animator.SetBool("isFleeing", true);
                animator.SetBool("isChasing", false);
            }
            animator.SetBool("isPatrolling", false);
        
        }

    }
}
