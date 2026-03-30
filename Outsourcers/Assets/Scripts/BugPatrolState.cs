using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class BugPatrolState : StateMachineBehaviour
{
    float timer;
    public float patrolTime = 10f;

    Transform player;
    NavMeshAgent agent;

    public float detectionAreaRadius = 18f;
    public float patrolSpeed = 2f;

    List<Transform> waypointsList = new List<Transform>();
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Initiation

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        agent.speed = patrolSpeed;
        timer = 0;

        //Get all waypoints and move to the first one
        GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoint");
        foreach(Transform t in waypointCluster.transform)
        {
            waypointsList.Add(t);
        }

        Vector3 nextPosition = waypointsList[Random.Range(0, waypointsList.Count)].position;
        agent.SetDestination(nextPosition);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Check if enemy reached waypoint then it will move to the next
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);
        }

        //Transition to Idle
        timer += Time.deltaTime;
        if (timer > patrolTime)
        {
            animator.SetBool("isPatrolling", false);
        }

        //Transition to Chase State or Flee State
        //get a random value to decide whether to chase or flee 
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionAreaRadius)
        {
            int randomNum = Random.Range(0,2);
            //Debug.Log(randomNum);
            if (randomNum == 0)
            {
                
                animator.SetBool("isChasing",true);
                animator.SetBool("isFleeing", false);

            }
            else if (randomNum == 1)
            {
                
                animator.SetBool("isFleeing",true);
                animator.SetBool("isChasing", false);
            }
            animator.SetBool("isPatrolling", false);
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Stop the agent
        agent.SetDestination(agent.transform.position);
    }
}
