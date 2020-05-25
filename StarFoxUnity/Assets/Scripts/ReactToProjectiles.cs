using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactToProjectiles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet") || other.CompareTag("PlayerBullet"))
        {
            if (other.gameObject.GetComponent<ProjectileMovement>() != null)
                other.gameObject.GetComponent<ProjectileMovement>().HitnDestroy();
            else if (other.gameObject.GetComponent<SmartProjectileMovement>() != null)
                other.gameObject.GetComponent<SmartProjectileMovement>().HitnDestroy();
        }
    }
}
