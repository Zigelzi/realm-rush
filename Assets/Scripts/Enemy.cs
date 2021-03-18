using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] [Range(0, 50)] int goldReward = 25;
    [SerializeField] [Range(0, 50)] int goldCost = 25;
    Bank bank;
    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RewardGold()
    {
        if (bank != null)
        {
            bank.Deposit(goldReward);
        }
    }

    public void StealGold()
    {
        if (bank != null)
        {
            bank.Withdraw(goldCost);
        }
    }
}
