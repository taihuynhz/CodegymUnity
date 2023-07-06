using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected float hP = 30f;
    [SerializeField] protected float fuel = 25f;
    [SerializeField] protected float capacity = 10f;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.CompareTag("Player"))
        {
            other.transform.parent.GetComponent<PlayerController>().Add(hP, capacity, fuel);
            Destroy(gameObject);
        }
    }
}
