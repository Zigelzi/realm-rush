using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform target;

    private Transform weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = transform.Find("BallistaTopMesh").transform;
    }

    // Update is called once per frame
    void Update()
    {
        AimWeapon();
    }

    private void AimWeapon()
    {
        GameObject closestEnemy = GetTarget();
        weapon.LookAt(closestEnemy.transform);
    }

    private GameObject GetTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = enemies[0];
        
        Vector3 towerPosition = transform.position;
        float closestEnemyDistance = Vector3.Distance(towerPosition, closestEnemy.transform.position);

        foreach (GameObject enemy in enemies)
        {
            float distanceFromTower = Vector3.Distance(towerPosition, enemy.transform.position);
            if (distanceFromTower < closestEnemyDistance)
            {
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
}
