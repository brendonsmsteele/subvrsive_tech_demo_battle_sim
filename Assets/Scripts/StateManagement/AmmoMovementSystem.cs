using UnityEngine;

public class AmmoMovementSystem : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;

    void Update()
    {
        var ammoStates = GameStateManager.Instance.GetAllAmmo();

        foreach (var kvp in ammoStates)
        {
            AmmoState ammo = kvp.Value;
            Vector3 movement = ammo.direction * ammo.speed * Time.deltaTime;

            AmmoState newState = new AmmoState(ammo.id, ammo.position + movement, ammo.direction, ammo.damage, ammo.speed);
            messageQueue.Publish(GlobalSlugs.AMMO_STATE_CHANGED, newState);
        }
    }
}
