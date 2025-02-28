using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBattleScenario", menuName = "ScriptableObjects/BattleScenario", order = 1)]
public class BattleScenario : ScriptableObject
{
    [SerializeField] List<Character> _players = new List<Character>(16);
    public List<Character> players => new List<Character>(_players);
}
