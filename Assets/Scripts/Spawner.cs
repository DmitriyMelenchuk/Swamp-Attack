using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _pointSpawn;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private float _timeAfterLastSpawn;
    private int _spawned;
    private int _currentWaveNumber = 0;

    public event UnityAction AllEnemySpawned;
    public event UnityAction<int, int> EnemyCountChanged;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }
        if (_spawned >= _currentWave.Count)
        {
            if (_waves.Count > _currentWaveNumber + 1)
                AllEnemySpawned?.Invoke();

            _currentWave = null;
        }
    }

    public void NextWave()
    {
        SetWave(_currentWaveNumber++);
        _spawned = 0;
        EnemyCountChanged?.Invoke(0, 1);
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _pointSpawn.position, _pointSpawn.rotation, _pointSpawn).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
        EnemyCountChanged?.Invoke(_spawned + 1, _currentWave.Count);
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;


        _player.AddMoney(enemy.Reward);
    }
}

[Serializable]
public class Wave
{
     public int Count;
     public Enemy Template;
     public float Delay;
}
