using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] protected float speed;
    protected Transform targetTransform;
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private int powerUpDropChance;
    protected int score;
    protected GameObject gameManager;

    public void SetStats(int enemyhealth, int enemyDamage, float enemySpeed, int scoreVal)
    {
        health = enemyhealth;
        damage = enemyDamage;
        speed = enemySpeed;
        score = scoreVal;
    }

    protected virtual void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("player").transform;
        gameManager = GameObject.FindGameObjectWithTag("GameController");
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

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            OnDeath();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if(health <= 0)
        {
            OnDeath();
            gameManager.GetComponent<GameManager>().AddScore(score);
            Destroy(gameObject);
        }
    }

    private void OnDeath()
    {
        int randomNum = Random.Range(0, 100);
        if(randomNum <= powerUpDropChance)
        {
            randomNum = Random.Range(0, powerUps.Length);
            Instantiate(powerUps[randomNum], transform.position, transform.rotation);

        }
    }

}
