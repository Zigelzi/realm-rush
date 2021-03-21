using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int healthIncrement = 20;
    int currentHealth;
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            ProcessHit(20);
        }     
    }

    void ProcessHit(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            enemy.RewardGold();
            maxHealth += healthIncrement;
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

}
