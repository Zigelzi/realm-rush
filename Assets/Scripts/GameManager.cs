using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreDisplay;
    int restartDelay = 2;
    State gameState;
    Bank bank;

    public enum State { Alive, Transcending, Defeated }
    public State GameState { get { return gameState; }}
    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
        if (bank != null)
        {
            UpdateScoreDisplay();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bank != null)
        {
            CheckBankBalance();
            UpdateScoreDisplay();
        }
    }

    void CheckBankBalance()
    {
        if (bank.CurrentBalance <= 0)
        {
            LoseGame();
        }
    }

    void LoseGame()
    {
        gameState = State.Defeated;
        Invoke("RestartGame", restartDelay);
    }

    void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);

    }

    void UpdateScoreDisplay()
    {
        scoreDisplay.text = $"Gold: {bank.CurrentBalance}";
    }
}
