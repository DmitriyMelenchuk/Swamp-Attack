using System.Collections.Generic;
using UnityEngine;

public class SpawnerWeapon : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private List<GameObject> _weapons = new List<GameObject>();

    private void OnEnable()
    {
        _player.ChangedWeapon += OnWeaponSelected;
        _player.BuyedWeapon += OnBuyedWeapon;
    }

    private void OnDisable()
    {
        _player.ChangedWeapon -= OnWeaponSelected;
        _player.BuyedWeapon -= OnBuyedWeapon;
    }

    private void Spawn(Weapon weapon)
    {
        var view = Instantiate(weapon, _spawnPoint.position, weapon.transform.rotation , _spawnPoint).gameObject;
        view.SetActive(false);

        _weapons.Add(view);
    }

    private void OnWeaponSelected(Weapon weapon)
    {
        if (_weapons.Count == 0)
        {
            Spawn(weapon);
            _weapons[_player.CurrentWeaponNumber].SetActive(true);
        }
        else
        {
            _weapons[_player.PreviousWeaponNumber].SetActive(false);
            _weapons[_player.CurrentWeaponNumber].SetActive(true);
        }
    }

    private void OnBuyedWeapon(Weapon weapon)
    {
        Spawn(weapon);
    }
}
