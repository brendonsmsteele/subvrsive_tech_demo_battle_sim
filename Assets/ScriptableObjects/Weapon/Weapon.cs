using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "ScriptableObjects/Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    [SerializeField] float _attackSpeed;
    [SerializeField] float _range;
    [SerializeField] Ammo _ammo;

    public float attackSpeed => _attackSpeed;
    public float range => _range;
    public Ammo ammo => _ammo;
}
