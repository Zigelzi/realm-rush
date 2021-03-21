using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] [Range(0, 60f)] float towerRange = 15f; // One tile is 10f

    Transform weapon;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        weapon = transform.Find("BallistaTopMesh").transform;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GameState != GameManager.State.Defeated)
        {
            AimWeapon();
        } else
        {
            Attack(false);
        }
        
    }

    void AimWeapon()
    {
        if (target == null)
        {
            target = GetTarget();
        } else if (target.activeSelf == false)
        {
            target = GetTarget();
        } else
        {
            float closestEnemyDistance = Vector3.Distance(transform.position, target.transform.position);
            weapon.LookAt(target.transform);
            if (closestEnemyDistance < towerRange)
            {
                Attack(true);
            } else
            {
                Attack(false);
                target = GetTarget();
            }
        }
    }

    GameObject GetTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        
        if (enemies.Length > 0)
        {
            GameObject closestEnemy = enemies[0].gameObject;
            Vector3 towerPosition = transform.position;
            float closestEnemyDistance = Vector3.Distance(towerPosition, closestEnemy.transform.position);

            foreach (Enemy enemy in enemies)
            {
                float distanceFromTower = Vector3.Distance(towerPosition, enemy.transform.position);
                
                if (distanceFromTower < closestEnemyDistance)
                {
                    closestEnemy = enemy.gameObject;
                }
            }
            return closestEnemy;
        } else
        {
            return null;
        }
    }

    void Attack(bool isActive)
    {
        ParticleSystem.EmissionModule emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}
