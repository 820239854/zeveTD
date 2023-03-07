using System;
using UnityEngine;

public class MoneyManager:MonoBehaviour
{
    private int currPlayerMoney ;
    public int starterMoney;

    private void Start()
    {
        currPlayerMoney = starterMoney;
    }

    public int GetCurrPlayerMoney()
    {
        return currPlayerMoney;
    }
    
    public void AddMoney(int amount)
    {
        currPlayerMoney += amount;
    }
    
    public void SubtractMoney(int amount)
    {
        currPlayerMoney -= amount;
        Debug.Log("Subtracted " + amount + " from player money.`");
    }
}
