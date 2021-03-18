using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State { Alive, Transcending, Defeated }
    private State gameState;
    private Bank bank;
    public State GameState { get { return gameState; }}
    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bank != null)
        {
            CheckBankBalance();
        }
    }

    private void CheckBankBalance()
    {
        if (bank.CurrentBalance <= 0)
        {
            End();
        }
    }

    private void End()
    {
        gameState = State.Defeated;

    }
}
