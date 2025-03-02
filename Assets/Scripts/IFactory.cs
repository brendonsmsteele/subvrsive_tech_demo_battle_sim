using UnityEngine;

public interface IFactory<T>
{
    public T GetObject(Vector3 position, Quaternion rotation);
    public void ReturnObject(GameObject go);
}
