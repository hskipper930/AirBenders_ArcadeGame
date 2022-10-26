using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : RangedEnemyAI
{
    private enum State { shooting, chasing, summoning}
    private State currentState;
    [SerializeField] private float stateDuration;
    [SerializeField] private GameObject enemyToSpawn;

    protected override void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("player").transform;
        StartCoroutine(ChangeState());
    }

    protected override void Move()
    {
        if (currentState == State.chasing)
        {
            float distance = Vector3.Distance(transform.position, targetTransform.position);
            float interpolant = (speed * Time.deltaTime) / distance;
            transform.position = Vector3.Lerp(transform.position, targetTransform.position, interpolant);
        }
    }

    private IEnumerator ChangeState()
    {
        WaitForSeconds waitTime = new WaitForSeconds(stateDuration);
        for(; ; )
        {
            currentState = State.shooting;
            StartCoroutine("Shoot");
            yield return waitTime;
            StopCoroutine("Shoot");
            currentState = State.chasing;
            yield return waitTime;
            currentState = State.summoning;
            StartCoroutine("Summon");
            yield return waitTime;
            StopCoroutine("Summon");
        }
    }

    private IEnumerator Summon()
    {
        Vector3 offset = new Vector3();
        Vector3 spawnPosition;
        for(; ; )
        {
            spawnPosition = transform.position;
            offset.x = Random.Range(1.5f, 5);
            offset.y = Random.Range(1.5f, 5);
            switch(Random.Range(0, 1))
            {
                case 0:
                    spawnPosition.x += offset.x;
                    break;
                case 1:
                    spawnPosition.x -= offset.x;
                    break;
            }
            switch(Random.Range(0, 1))
            {
                case 0:
                    spawnPosition.y += offset.y;
                    break;
                case 1:
                    spawnPosition.y -= offset.y;
                    break;
            }
            GameObject enemy = Instantiate(enemyToSpawn, spawnPosition, transform.rotation);
            enemy.GetComponent<EnemyAI>().SetStats(2, 1, 5);
            yield return new WaitForSeconds(Random.Range(.5f, 3));
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        //damage player
    }
}
