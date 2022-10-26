using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private int damage;
    private Vector3 targetPosition;

    public void SetDamage(int amount)
    {
        damage = amount;
    }
    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
    }

    public void SetTag(string tag)
    {
        gameObject.tag = tag;
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
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(gameObject.CompareTag("hostile"))
        {
            if (collision.gameObject.CompareTag("player"))
            {
                //damage player
                ProjectileObjectPooling.DeactivateProjectile(gameObject);
            }
        }
        if(gameObject.CompareTag("friendly"))
        {
            if(collision.gameObject.CompareTag("enemy"))
            {
                collision.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
                ProjectileObjectPooling.DeactivateProjectile(gameObject);
            }
        }
    }
}
