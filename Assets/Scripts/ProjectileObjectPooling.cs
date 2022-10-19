using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObjectPooling : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private int initialNumber;
    [SerializeField] private int expansionNumber;
    private static List<GameObject> projectilePool = new List<GameObject>();
    private static ProjectileObjectPooling instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        CreateProjectiles(initialNumber);
    }

    public static void ActivateProjectile(Vector3 startPosition, Vector3 targetPosition)
    {
        if (projectilePool.Count > 0)
        {
            projectilePool[0].transform.position = startPosition;
            projectilePool[0].SetActive(true);
            projectilePool[0].GetComponent<ProjectileMovement>().SetTarget(targetPosition);
            projectilePool.RemoveAt(0);
        }
        else
        {
            instance.CreateProjectiles(instance.expansionNumber);
        }
    }

    public static void DeactivateProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
        projectilePool.Add(projectile);
    }

    private void CreateProjectiles(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            projectilePool.Add(Instantiate(projectile));
            projectilePool[i].SetActive(false);
        }
    }
}
