using UnityEngine;

[CreateAssetMenu(fileName = "NewAmmo", menuName = "ScriptableObjects/Ammo", order = 1)]
public class Ammo : ScriptableObject
{
    [SerializeField] float _damage;
    [SerializeField] float _speed;
    public float damage => _damage;
    public float speed => _speed;
}
