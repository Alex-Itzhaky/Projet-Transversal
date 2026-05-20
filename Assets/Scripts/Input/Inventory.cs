using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private int _reputationPoints = 0;
    [SerializeField] private TMP_Text _repPointsText;
    [SerializeField] private TMP_Text _gameOverScreenText;

    public void Start()
    {
        _repPointsText.text = _reputationPoints.ToString();
    }
    public void AddPoints(int amount)
    {
        _reputationPoints += amount;
        _repPointsText.text = _reputationPoints.ToString();
        _gameOverScreenText.text = _reputationPoints.ToString();
    }

    public void RemovePoints(int amount)
    {
        _reputationPoints -= amount;
        _repPointsText.text = _reputationPoints.ToString();
        _gameOverScreenText.text = _reputationPoints.ToString();
    }
}
