using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int healthIncrement = 20;
    private int currentHealth;
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        currentHealth = maxHealth;
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
            maxHealth += healthIncrement;
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

}
