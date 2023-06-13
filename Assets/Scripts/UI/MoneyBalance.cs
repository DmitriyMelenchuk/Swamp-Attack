using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyBalance : MonoBehaviour
{
    [SerializeField] private TMP_Text _count;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.MoneyChanged += OnMoneyChange;
        _count.text = _player.Money.ToString();
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= OnMoneyChange;
    }

    private void OnMoneyChange(int money)
    {
        _count.text = money.ToString();
    }
}
