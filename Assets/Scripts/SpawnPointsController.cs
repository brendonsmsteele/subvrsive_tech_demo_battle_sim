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
}