using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        messageQueue.Subscribe("", OnCharacterHealthChanged);
    }

    private void OnDisable()
    {
        messageQueue.Unsubscribe("", OnCharacterHealthChanged);
    }

    void OnCharacterHealthChanged(object obj)
    {
        //var data = ()obj
        Debug.Log($"Character health changed - Character: {"data.car"} {"data.health"}");
    }
}
