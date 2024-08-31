using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }
    public virtual void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this as T;
    }
}