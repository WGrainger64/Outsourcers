using UnityEngine;
using UnityEngine.AI;

public class Bug : MonoBehaviour
{
    [SerializeField] public int HP = 20;
    internal Animator animator;
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
            GetComponent<Outline>().enabled = false; //It wont be outlined if its being held
        }
    }

    public void TakeDamage(int damageAmount)
    {
        
        HP -= damageAmount;

        if (HP <= 0)
        {
            animator.SetTrigger("DIE");
            animator.SetBool("isDead",true);
            isAlive = false;
            print("dead");
            GetComponent<NavMeshAgent>().enabled = false;
            animator.enabled = false;
        }
        else
        {
            animator.SetTrigger("DAMAGE");
        }
   }

    private void OnDrawGizmos() //20:16 Enemies: State Machine
    {
        
    }
}
