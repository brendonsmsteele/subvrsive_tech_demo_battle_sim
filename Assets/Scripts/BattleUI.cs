using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] ActiveBattleUI activeBattleUI;
    [SerializeField] WonBattleUI wonBattleUI;

    enum State
    {
        None,
        BattleStarted,
        BattleEnded
    }

    State _state;
    State state { get { return _state; } 
        set
        {
            if(_state != value)
            {
                _state = value;
                activeBattleUI.gameObject.SetActive(_state == State.BattleStarted);
                wonBattleUI.gameObject.SetActive(_state == State.BattleEnded);
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        messageQueue.Subscribe(GlobalSlugs.BATTLE_STARTED, HandleBattleStarted);
        messageQueue.Subscribe(GlobalSlugs.BATTLE_ENDED, HandleBattleEnded);
    }

    private void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.BATTLE_STARTED, HandleBattleStarted);
        messageQueue.Unsubscribe(GlobalSlugs.BATTLE_ENDED, HandleBattleEnded);
    }

    void HandleBattleStarted(object obj)
    {
        state = State.BattleStarted;
    }

    void HandleBattleEnded(object obj)
    {
        state = State.BattleEnded;
    }
}
