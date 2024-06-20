using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private int _money = 1000;

    private void Awake()
    {
        MoneyManager.OnMoneyUpdated += UpdateMoneyText;
    }

    private void Start()
    {
        UpdateMoneyText(_money);
    }

    private void UpdateMoneyText(int money)
    {
        _moneyText.text = $"Cash: {money.ToString()}";
    }
}