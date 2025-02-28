using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public Transform[] points = new Transform[16];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        foreach(Transform point in points)
        {
            Gizmos.DrawWireCube(point.position, Vector3.one);
            Gizmos.DrawRay(new Ray(point.position, point.forward));
        }
    }
}
