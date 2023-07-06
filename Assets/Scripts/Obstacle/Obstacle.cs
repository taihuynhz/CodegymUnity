using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.parent.CompareTag("Player"))
        {
            collision.collider.transform.parent.GetComponent<PlayerController>().Deduct(damage);
        }
    }      
}
