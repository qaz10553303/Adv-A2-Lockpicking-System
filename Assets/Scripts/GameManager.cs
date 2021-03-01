using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float pinSpeed;
    public int skillLv;
    public int levelDifficulty;
    public int totalNumOfPin;
    public int unlockedNumOfPin;
    public int toolAmount;
    public float timeLeft;

    public bool canOperate;



    public GameObject gameplayInterface;
    public GameObject levelSelectInterface;
    public GameObject qteInterface;
    public GameObject resultInterface;
    public Text resultText;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        skillLv=Mathf.Clamp(skillLv, 0, 3);
        WinLoseCheck();
        if (!canOperate) return;
        TimerCheck();
    }

    void WinLoseCheck()
    {
        if (unlockedNumOfPin >= totalNumOfPin)//win
        {
            Win();
        }
        if (unlockedNumOfPin<0)//not lose but decrease tool amount by 1
        {
            toolAmount -= 1;
            unlockedNumOfPin = 0;
        }
        if (toolAmount <= 0)//lose
        {
            Lose();
        }
    }




    void TimerCheck()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            Lose();
        }
    }

    void Win()
    {
        canOperate = false;
        resultText.text = "You win!";
        resultInterface.SetActive(true);
    }

    void Lose()
    {
        canOperate = false;
        resultText.text = "You Lose!";
        resultInterface.SetActive(true);
    }

    public void SetLevelDifficulty(int difficulty)
    {
        levelDifficulty = difficulty;
        totalNumOfPin = difficulty+1;
        levelSelectInterface.SetActive(false);
        qteInterface.SetActive(true);
    }



}
