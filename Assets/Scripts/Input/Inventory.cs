using System;
using UnityEngine;
[Serializable]
public class Inventory
{
    public int reputationPoints = 0;

    public void AddPoints(int amount)
    {
        reputationPoints += amount;
    }

    public void RemovePoints(int amount)
    {
        reputationPoints -= amount;
    }
}
