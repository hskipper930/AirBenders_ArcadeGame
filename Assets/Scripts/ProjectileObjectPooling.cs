using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObjectPooling : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private int initialNumber;
    private static List<GameObject> projectilePool = new List<GameObject>();

    private void Start()
    {
        for(int i = 0; i < initialNumber; i++)
        {
            projectilePool.Add(Instantiate(projectile));
            projectilePool[i].SetActive(false);
        }
    }

    public static void ActivateProjectile(Vector3 startPosition, Vector3 targetPosition)
    {
        for(int i = 0; i < projectilePool.Count; i++)
        {
            if (!projectilePool[i].activeInHierarchy)
            {
                projectilePool[i].SetActive(true);
                projectilePool[i].GetComponent<ProjectileMovement>().SetTarget(targetPosition);
                return;
            }
        }
        //instantiate more projectiles
    }
}
