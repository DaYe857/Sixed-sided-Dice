using System;
using UnityEngine;

public class EventHandler
{
    public static event Action RollDiceEvent;

    public static void RollDice() => RollDiceEvent?.Invoke();
    
    public static event Action WinDiceEvent;
    
    public static void WinDice() => WinDiceEvent?.Invoke();
    
    public static event Action LoseDiceEvent;
    
    public static void LoseDice() => LoseDiceEvent?.Invoke();
}
