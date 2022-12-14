using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script Description
 * Programmer: Andrew Krieps and Hunter Skipper
 * 
 * This script will need to do the following:
 * > Run the waves
 * >> This script will randomly generate an ever lengthening # of enemies per wave
 * >> Waves will be spawned at intervals
 * >> After 5 waves a boss will spawn, and no waves will continue to spawn until it's defeated.
 * >> After player defeats boss, difficulty will increase
 * 
 */

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPoints = new GameObject[20];
    [SerializeField] private GameObject rangedEnemy;
    [SerializeField] private GameObject meleeEnemy;
    [SerializeField] private GameObject bossEnemy;
    private int enemiesToSpawn = 5;

    private float speed = 3f;
    private int health = 10;
    private int damage = 10;
    private float diffModifier = 1f;
    private int wave = 1;

    private int score;
    [SerializeField] private HighScores highscores;
    [SerializeField] private UIController ui;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaveTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeWave(int noe) //(noe stands for Number of Enemies)
    {
        GameObject[] enemies = new GameObject[noe];
        foreach(GameObject enemy in enemies)
        {
            //decide spawpoint
            int spawnLoc = Random.Range(0, 20);
            //Decide between ranged and melee
            GameObject typeToUse;
            int typeDecide = Random.Range(0, 2);
            switch (typeDecide)
            {
                case 0:
                    typeToUse = rangedEnemy;
                    break;
                case 1:
                    typeToUse = meleeEnemy;
                    break;
                default:
                    typeToUse = rangedEnemy;
                    break;
            }
            //spawn
            GameObject nEnemy = Instantiate(typeToUse, spawnPoints[spawnLoc].transform.position, transform.rotation);
            if(typeToUse == rangedEnemy)
            {
                float shootSpeed = Random.Range(4, 8);
                nEnemy.GetComponent<RangedEnemyAI>().SetStats(health, damage, speed, shootSpeed, 15);
            }
            else
            {
                nEnemy.GetComponent<EnemyAI>().SetStats(health, damage, speed, 10);
            }
            
        }
        if(wave % 3 == 0)
        {
            int spawnLoc = Random.Range(0, 20);
            GameObject bEnemy = Instantiate(bossEnemy, spawnPoints[spawnLoc].transform.position, transform.rotation);
            bEnemy.GetComponent<BossAI>().SetStats(health * 5, damage * 2, speed * 1.5f, 100);
            diffModifier += 0.1f;
        }
        enemiesToSpawn++;
        IncreaseDifficulty();
    }

    //Difficulty
    private void IncreaseDifficulty()
    {
        diffModifier += 0.1f;
        speed *= diffModifier;
        health = (int)(health * diffModifier);
        damage = (int)(damage * diffModifier);
    }

    private IEnumerator WaveTimer()
    {
        while (true)
        {
            MakeWave(enemiesToSpawn);
            wave++;
            yield return new WaitForSeconds(20f);
        }
    }

    public void AddScore(int scoreAmount)
    {
        score += scoreAmount;
        ui.UpdateScoreText(score);
    }

    public void EndGame()
    {
        ui.ShowDeathPanel(highscores.CheckForHighScore(score));
    }

    public void AddNewScore(string name)
    {
        highscores.AddHighScore(name, score);
    }
}
