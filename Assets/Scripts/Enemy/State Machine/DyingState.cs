using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DyingState : State
{
    [SerializeField] private float _timeToDissapear;

    private Animator _animator;
    private float _lastTimeAfterDeath;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    private void Update()
    {
        _animator.Play("Death");
        
        _lastTimeAfterDeath += Time.deltaTime;

        if (_lastTimeAfterDeath >= _timeToDissapear)
            Destroy(gameObject);
    }
}
