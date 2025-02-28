using UnityEngine;

[CreateAssetMenu(fileName = "NewBattleScenario", menuName = "ScriptableObjects/BattleScenario", order = 1)]
public class BattleScenario : ScriptableObject
{
    public Character[] players = new Character[16];
}
