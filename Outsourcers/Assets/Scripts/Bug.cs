using UnityEngine;
using UnityEngine.AI;

public class Bug : MonoBehaviour
{
    [SerializeField] public int HP = 20;
    private Animator animator;
    private bool isAlive;

    private NavMeshAgent navAgent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navAgent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking",false);
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
