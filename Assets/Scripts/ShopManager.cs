using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public MoneyManager moneyManager;
    public GameObject basicTowerPrefab;

    public int basicTowerCost;

    public int GetBasicTowerCost(GameObject towerPrefab)
    {
        int cost = 0;
        if (towerPrefab == basicTowerPrefab)
        {
            cost = basicTowerCost;
        }
        return cost;
    }

    public bool CanBuyBasicTower(GameObject towerPrefab)
    {
        int cost = GetBasicTowerCost(towerPrefab);
        bool canBuy = moneyManager.GetCurrPlayerMoney() >= cost;
        return canBuy;
    }
    
    public void BuyTower(GameObject towerPrefab)
    {
        moneyManager.SubtractMoney(GetBasicTowerCost(towerPrefab));
    }
}