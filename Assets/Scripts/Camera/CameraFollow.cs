using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow: MonoBehaviour
{
    [SerializeField] protected Transform target;
    protected Vector3 cameraDistance;

    private void Awake()
    {
        GetDistance();
    }

    protected void LateUpdate()
    {
        Following();
    }

    protected void Following()
    {
        transform.position = target.transform.position + target.transform.forward * cameraDistance.z;
        transform.LookAt(target.transform.position);
        transform.position = new Vector3(transform.position.x, transform.position.y + cameraDistance.y, transform.position.z);
    }

    protected void GetDistance()
    {
        cameraDistance = transform.position - target.transform.position;
    }
}