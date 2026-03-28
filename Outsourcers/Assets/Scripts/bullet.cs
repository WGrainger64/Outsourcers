using System;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public int bulletDamage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            print("hit" + collision.gameObject.name + " !");
            CreatBulletImpactEffect(collision);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            print("hit a wall");

            CreatBulletImpactEffect(collision);

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<Bug>().isAlive == true)
            {

                collision.gameObject.GetComponent<Bug>().TakeDamage(bulletDamage);
                CreateBloodSprayEffect(collision);
            }

            
            Destroy(gameObject);
        }
    }

    private void CreateBloodSprayEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject bloodSprayPrefab = Instantiate(
            GlobalReference.Instance.bloodSprayEffect,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );
        bloodSprayPrefab.transform.position = new Vector3(bloodSprayPrefab.transform.position.x, objectWeHit.gameObject.transform.position.y-1f, bloodSprayPrefab.transform.position.z);

        bloodSprayPrefab.transform.SetParent(objectWeHit.gameObject.transform);
        //Replace the y value lower on the bug
        //bloodSprayPrefab.transform.localPosition = new Vector3(bloodSprayPrefab.transform.position.x, 2f, bloodSprayPrefab.transform.position.z);
        print(bloodSprayPrefab.transform.position);
    }

    void CreatBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(
            GlobalReference.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );

        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}
