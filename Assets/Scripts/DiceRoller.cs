using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceRoller : MonoBehaviour
{
    private int currentNum = 0;

    public void RollDice()
    {
        int newNum;
        do
        {
            newNum = Random.Range(1, 7);
        }while(newNum == currentNum);
        currentNum = newNum;
        EventHandler.RollDice();
    }

    public int GetCurrentNum() => currentNum;
}
