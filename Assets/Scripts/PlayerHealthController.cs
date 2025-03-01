using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] bool _isDead = false;
    public bool isDead => _isDead;

    [SerializeField] float _health;
    public float health => _health;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        //messageQueue.Subscribe(GlobalSlugs.PLAYER_ADDED_TO_BATTLE, HandlePlayerAddedToBattle);
        //messageQueue.Subscribe(GlobalSlugs.PLAYER_REMOVED_FROM_BATTLE, HandlePlayerRemovedFromBattle);
    }

    void OnDisable()
    {
        //messageQueue.Unsubscribe(GlobalSlugs.PLAYER_ADDED_TO_BATTLE, HandlePlayerAddedToBattle);
        //messageQueue.Unsubscribe(GlobalSlugs.PLAYER_REMOVED_FROM_BATTLE, HandlePlayerRemovedFromBattle);

    }
}
