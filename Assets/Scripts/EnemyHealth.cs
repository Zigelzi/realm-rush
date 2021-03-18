using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    private int currentHealth;
    private Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        enemy = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            ProcessHit(20);
        }     
    }

    private void ProcessHit(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            enemy.RewardGold();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

}
