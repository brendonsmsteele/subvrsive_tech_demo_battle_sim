using UnityEngine;

public interface IFactory<T>
{
    public T GetObject(Vector3 position, Quaternion rotation, bool setActive = true);
    public void ReturnObject(GameObject go);
}
