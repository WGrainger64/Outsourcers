using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public int HP = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {

        HP -= damageAmount;

        if (HP <= 0)
        {
            print("Player Dead");
        }
        else
        {
            print("Player Hit");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("hi");
        if (other.CompareTag("BugAttack"))
        {
            TakeDamage(other.gameObject.GetComponent<BugAttack>().damage);
        }
    }
}
