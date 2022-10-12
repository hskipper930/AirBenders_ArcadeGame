using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonRangedEnemyAI : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform targetTransform;

    private void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("player").transform;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float distance = Vector3.Distance(transform.position, targetTransform.position);
        float interpolant = (speed * Time.deltaTime) / distance;
        transform.position = Vector3.Lerp(transform.position, targetTransform.position, interpolant);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("player"))
        {
            //damage player
            Destroy(gameObject);
        }
    }

}
