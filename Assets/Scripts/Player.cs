using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;

    private Weapon _currentWeapon;
    private Animator _animator;
    private int _currentHealth;
    private int _currentWeaponNumber = 0;
    private float _lastTimeAttack;
    private int _previousWeaponNumber; 

    public int Money { get; private set; } = 500;
    public int CurrentWeaponNumber => _currentWeaponNumber; 
    public int PreviousWeaponNumber => _previousWeaponNumber; 

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;
    public event UnityAction<Weapon> ChangedWeapon;
    public event UnityAction<Weapon> BuyedWeapon;

    private void Start()
    {
        SetWeapon(_weapons[_currentWeaponNumber]);
        _currentHealth = _health;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if(_lastTimeAttack <= 0)
            {
                _currentWeapon.Shoot(_shootPoint);
                _lastTimeAttack = _currentWeapon.Delay;
            }

        _lastTimeAttack -= Time.deltaTime;
    }

    public void AddMoney(int reward)
    {
        Money += reward;
        MoneyChanged?.Invoke(Money);
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
            Destroy(gameObject);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanged?.Invoke(Money);
        _weapons.Add(weapon);
        BuyedWeapon?.Invoke(weapon);
    }

    public void NextWeapon()
    {
        _previousWeaponNumber = _currentWeaponNumber;

        if (_currentWeaponNumber >= _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        SetWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        _previousWeaponNumber = _currentWeaponNumber;

        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        SetWeapon(_weapons[_currentWeaponNumber]);
    }

    public void SetWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        ChangedWeapon?.Invoke(_currentWeapon);
    }
}
