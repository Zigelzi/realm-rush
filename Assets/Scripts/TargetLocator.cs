using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] GameObject target;

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
        if (target == null)
        {
            target = GetTarget();
            
        } else
        {
            weapon.LookAt(target.transform);
        }
        
    }

    private GameObject GetTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
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
        } else
        {
            return null;
        }
        
    }
}
