using UnityEngine;

[CreateAssetMenu(fileName = "NewAmmo", menuName = "ScriptableObjects/Ammo", order = 1)]
public class Ammo : ScriptableObject
{
    [SerializeField] float _damage = 1f;
    [SerializeField] float _speed = 1f;
    [SerializeField] float _range = 10f;
    public float damage => _damage;
    public float speed => _speed;
    public float range => _range;
}
