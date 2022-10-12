using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 targetPosition;

    //note: the projectile's target MUST be set immediately after instantiating a projectile
    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        float interpolant = (speed * Time.deltaTime) / distance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, interpolant);
        if(transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }


}
