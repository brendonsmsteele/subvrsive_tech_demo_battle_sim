using UnityEngine;

[CreateAssetMenu(fileName = "NewAmmo", menuName = "ScriptableObjects/Ammo", order = 1)]
public class Ammo : ScriptableObject
{
    public float damage;
    public float speed;
}
