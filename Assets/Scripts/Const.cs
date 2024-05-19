using UnityEngine;

public class Const : MonoBehaviourSingleton<Const>
{
    [SerializeField] private Materials materials;
    public static Materials Materials => Instance.materials;
}

[System.Serializable]
public class Materials
{
    public Material[] slotsMaterials;
    
}