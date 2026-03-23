using System;
using UnityEngine;

public class bullet : MonoBehaviour
{
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
