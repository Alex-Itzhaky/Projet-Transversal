using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int reputationPoints = 0;
    public TMP_Text repPointsText;

    public void Start()
    {
        repPointsText.text = reputationPoints.ToString();
    }
    public void AddPoints(int amount)
    {
        reputationPoints += amount;
        repPointsText.text = reputationPoints.ToString();
    }

    public void RemovePoints(int amount)
    {
        reputationPoints -= amount;
        repPointsText.text = reputationPoints.ToString();
    }
}
