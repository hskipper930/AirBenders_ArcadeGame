using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : EnemyAI
{
    [SerializeField] private float shootCooldown; //number of seconds between firing.

    public void SetStats(int enemyhealth, int enemyDamage, float enemySpeed, float fireRate, int score)
    {
        health = enemyhealth;
        damage = enemyDamage;
        speed = enemySpeed;
        shootCooldown = fireRate;
        base.score = score;
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Shoot());
        gameManager = GameObject.FindGameObjectWithTag("GameController");
    }

    protected override void Move(){}

    protected IEnumerator Shoot()
    {
        for(; ; )
        {
            ProjectileObjectPooling.ActivateProjectile(transform.position, targetTransform.position, "hostile", damage);
            yield return new WaitForSeconds(shootCooldown);
        }
    }
}
