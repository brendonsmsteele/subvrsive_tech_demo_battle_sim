using UnityEngine;

[CreateAssetMenu(fileName = "NewBattleStrategy", menuName = "ScriptableObjects/BattleStrategy", order = 1)]
public class BattleStrategy : ScriptableObject
{
    [SerializeField] bool _chooseCharacterAtRandom;
    [SerializeField] bool _chooseLowestHealthCharacter;
    [SerializeField] bool _chooseHighestHealthCharacter;
    [SerializeField] float _cooldownAfterAttack;
    [SerializeField] bool _shootAlivePlayers;
    [SerializeField] bool _shootDeadPlayers;

    public bool chooseCharacterAtRandom => _chooseCharacterAtRandom;
    public bool chooseLowestHealthCharacter => _chooseLowestHealthCharacter;
    public bool chooseHighestHealthCharacter => _chooseHighestHealthCharacter;
    public float cooldownAfterAttack => _cooldownAfterAttack;
    public bool shootAlivePlayers => _shootAlivePlayers;
    public bool shootDeadPlayers => _shootDeadPlayers;
}