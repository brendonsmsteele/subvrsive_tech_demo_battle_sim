using UnityEngine;

[CreateAssetMenu(fileName = "NewBattleStrategy", menuName = "ScriptableObjects/BattleStrategy", order = 1)]
public class BattleStrategy : ScriptableObject
{
    public float damage;
    public float speed;
}
