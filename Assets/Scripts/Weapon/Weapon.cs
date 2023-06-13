using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyed = false;
    [SerializeField] private float _delay;

    [SerializeField] protected Bullet Bullet;

    public float Delay => _delay;
    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _icon; 
    public bool IsBuyed => _isBuyed;

    public abstract void Shoot(Transform shootPoint);

    public void Buy(Weapon weapon)
    {
        _isBuyed = true;
    }
}
