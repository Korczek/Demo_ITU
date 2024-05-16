using UnityEngine;


public class SpawnableObject : MonoBehaviour
{
    public int spawnAmount = 1;
    [HideInInspector] public bool callableObjectInUse;

    public virtual void OnSpawn() { }
    public virtual void OnDespawn() { }
}