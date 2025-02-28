using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "ScriptableObjects/Character", order = 1)]
public class Character : ScriptableObject
{
    public float health;
    public float moveSpeed;
    public float rotateSpeed;
    public Weapon weapon;
}
