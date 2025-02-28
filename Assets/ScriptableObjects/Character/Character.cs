using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "ScriptableObjects/Character", order = 1)]
public class Character : ScriptableObject
{
    [SerializeField] string _friendlyName;
    [SerializeField] float _health;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotateSpeed;
    [SerializeField] Weapon _weapon;

    public string friendlyName => _friendlyName;
    public float health => _health;
    public float moveSpeed => _moveSpeed;
    public float rotateSpeed => _rotateSpeed;
    public Weapon weapon => _weapon;
}
