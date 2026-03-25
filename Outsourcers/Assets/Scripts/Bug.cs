using UnityEngine;
using UnityEngine.AI;

public class Bug : MonoBehaviour
{
    [SerializeField] public int HP = 20;
    private Animator animator;
    public bool isAlive;
    public GameObject player;

    [Header("Bug Holding Position")]
    public Vector3 spawnPos;
    public Vector3 spawnRot;
    public bool currHold = false;

    private NavMeshAgent navAgent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Awake()
    {
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (navAgent.velocity.magnitude > 0.1f)
        {
            //animator.SetBool("isWalking", true);
        }
        else
        {
            //animator.SetBool("isWalking",false);
        }
        if (isAlive)
        {
            //If bug is alive
        }
        else
        {
            
            //If bug is dead allow it to be picked up
           
        }

        if (currHold)
        {
            //If the bug is currently being held
        }
    }

    public void TakeDamage(int damageAmount)
    {
        
        HP -= damageAmount;

        if (HP <= 0)
        {
            animator.SetTrigger("DIE");
            isAlive = false;
            print("dead");
            
        }
        else
        {
            animator.SetTrigger("DAMAGE");
        }
   } 
}
