using UnityEngine;
using TMPro;

public class ActiveBattleUI : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] TMP_Text alivePlayers;

    int totalPlayerCount;
    int alivePlayerCount;
    bool isDirty;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (isDirty)
        {
            BindPlayerCountText();
            isDirty = false;
        }
    }

    void OnEnable()
    {
        messageQueue.Subscribe(GlobalSlugs.TOTAL_PLAYER_COUNT_CHANGED, HandleTotalPlayerCountChanged);
        messageQueue.Subscribe(GlobalSlugs.ALIVE_PLAYER_COUNT_CHANGED, HandleAlivePlayerCountChanged);
    }

    private void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.TOTAL_PLAYER_COUNT_CHANGED, HandleTotalPlayerCountChanged);
        messageQueue.Unsubscribe(GlobalSlugs.ALIVE_PLAYER_COUNT_CHANGED, HandleAlivePlayerCountChanged);
    }

    void HandleAlivePlayerCountChanged(object obj)
    {
        alivePlayerCount = (int)obj;
        isDirty = true;
    }
    void HandleTotalPlayerCountChanged(object obj)
    {
        //var data = ()obj
        totalPlayerCount = (int)obj;
        isDirty = true;
    }

    void BindPlayerCountText()
    {
        alivePlayers.text = $"{alivePlayerCount}/{totalPlayerCount}";
    }
}
