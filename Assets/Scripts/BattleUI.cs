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
        messageQueue.Subscribe(GlobalSlugs.BATTLE_STARTED, OnBattleStarted);
        messageQueue.Subscribe(GlobalSlugs.BATTLE_ENDED, OnBattleEnded);
    }

    private void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.BATTLE_STARTED, OnBattleStarted);
        messageQueue.Unsubscribe(GlobalSlugs.BATTLE_ENDED, OnBattleEnded);
    }

    void OnBattleStarted(object obj)
    {
        state = State.BattleStarted;
    }

    void OnBattleEnded(object obj)
    {
        state = State.BattleEnded;
    }
}
