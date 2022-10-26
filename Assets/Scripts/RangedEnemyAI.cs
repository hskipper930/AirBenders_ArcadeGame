using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : EnemyAI
{
    [SerializeField] private float shootCooldown; //number of seconds between firing.

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Shoot());
    }

    protected override void Move(){}

    private IEnumerator Shoot()
    {
        for(; ; )
        {
            ProjectileObjectPooling.ActivateProjectile(transform.position, targetTransform.position, "hostile", damage);
            yield return new WaitForSeconds(shootCooldown);
        }
    }
}
