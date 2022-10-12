using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed;
    protected Transform targetTransform;
    [SerializeField] private int health;

    protected virtual void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("player").transform;
    }

    private void Update()
    {
        Move();
    }

    protected virtual void Move()
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

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
