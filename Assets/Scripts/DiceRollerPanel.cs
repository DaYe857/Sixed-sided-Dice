using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollerPanel : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> diceNum;
    private int currentNum;

    private void OnEnable()
    {
        EventHandler.RollDiceEvent += RollDice;
    }

    private void OnDisable()
    {
        EventHandler.RollDiceEvent -= RollDice;
    }

    public void RollDice()
    {
        ResetDices();
        DiceRoller diceRoller = transform.parent.parent.GetComponent<DiceRoller>();
        currentNum = diceRoller.GetCurrentNum();
        switch (currentNum)
        {
            case 1:
                diceNum[0].SetActive(true);
                break;
            case 2:
                diceNum[1].SetActive(true);
                break;
            case 3:
                diceNum[2].SetActive(true);
                break;
            case 4:
                diceNum[3].SetActive(true);
                break;
            case 5:
                diceNum[4].SetActive(true);
                break;
            case 6:
                diceNum[5].SetActive(true);
                break;
        }
    }

    private void ResetDices()
    {
        foreach (var obj in diceNum)
        {
            obj.SetActive(false);
        }
    }
}
