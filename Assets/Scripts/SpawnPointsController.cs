using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.LowLevel;

public class SpawnPointsController : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] Transform[] _points = new Transform[0];
    public List<Transform> points => new List<Transform>(_points);

    void Start()
    {
        _points = GetComponentsInChildren<Transform>().Where(t => t != transform).ToArray();
        Debug.Log(points.Count);
    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        messageQueue.Subscribe(GlobalSlugs.PRE_BATTLE_STARTED, HandlePreBattleStarted);
        messageQueue.Subscribe(GlobalSlugs.BATTLE_ENDED, HandleBattleEnded);
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.PRE_BATTLE_STARTED, HandlePreBattleStarted);
        messageQueue.Unsubscribe(GlobalSlugs.BATTLE_ENDED, HandleBattleEnded);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        foreach(Transform point in _points)
        {
            Gizmos.DrawWireCube(point.position, Vector3.one);
            Gizmos.DrawRay(new Ray(point.position, point.forward));
        }
    }

    void HandlePreBattleStarted(object obj)
    {
        ClearPlayers();
        SpawnPlayers();
    }

    void HandleBattleEnded(object obj)
    {
    }

    void ClearPlayers()
    {
        PlayerFactory.Instance.ReturnAllObjects();
    }

    void SpawnPlayers()
    {
        foreach (Transform point in points)
        {
            var go = SpawnPlayer(point);
        }
    }

    GameObject SpawnPlayer(Transform point)
    {
        var player = PlayerFactory.Instance.GetObject(point.position, point.rotation);
        player.transform.SetParent(transform, true);
        return player.gameObject;
    }
}