using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : EnemyAI
{
    [SerializeField] private float shootCooldown; //number of seconds between firing.
    [SerializeField] private GameObject projectile;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Shoot());
    }

    protected override void Move()
    {
        // enemy will randomly move side-to-side
    }

    private IEnumerator Shoot()
    {
        for(; ; )
        {
            GameObject projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
            projectileInstance.GetComponent<ProjectileMovement>().SetTarget(targetTransform.position);
            yield return new WaitForSeconds(shootCooldown);
        }
    }
}
