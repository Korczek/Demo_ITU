using System.Diagnostics;
using UnityEngine;


public class Const : MonoBehaviourSingleton<Const>
{
    [SerializeField] private Materials materials;
    public static Materials Materials => Instance.materials;

    public static MapInitData GetPreparedLevel(int lvl)
    {
        return lvl switch
        {
            0 => new MapInitData()
            {
                Width = 8,
                Length = 8,
                StartInstruction = new int[]
                {
                    3, 3, 3, 0, 2, 2, 2, 2,
                    2, 2, 3, 2, 3, 3, 3, 2,
                    3, 2, 3, 2, 3, 2, 2, 2,
                    3, 2, 3, 2, 3, 2, 3, 3,
                    3, 2, 2, 2, 3, 2, 2, 2,
                    3, 3, 3, 2, 3, 3, 3, 2,
                    2, 2, 2, 2, 2, 2, 3, 2,
                    3, 3, 3, 3, 3, 3, 3, 1
                }
            },
            1 => new MapInitData()
            {
                Width = 12,
                Length = 12,
                StartInstruction = new[]
                {
                    3, 3, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2,
                    3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 3,
                    2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 3,
                    3, 3, 3, 3, 3, 3, 3, 3, 2, 3, 2, 3,
                    3, 2, 2, 2, 2, 2, 2, 3, 2, 3, 2, 3,
                    3, 2, 3, 3, 3, 3, 2, 3, 2, 2, 2, 3,
                    3, 2, 3, 2, 2, 3, 2, 3, 3, 3, 3, 3,
                    3, 2, 3, 2, 3, 3, 2, 3, 2, 2, 2, 2,
                    3, 2, 3, 2, 2, 2, 2, 3, 2, 3, 3, 3,
                    3, 2, 2, 2, 3, 3, 3, 3, 2, 2, 2, 3,
                    3, 3, 3, 2, 2, 2, 2, 2, 3, 3, 3, 3,
                    3, 3, 3, 3, 3, 3, 3, 1, 2, 2, 2, 2
                }
            },
            _ => new MapInitData()
            {
                Width = 16,
                Length = 16,
                StartInstruction = new[]
                {
                    0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3,
                    3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 3,
                    3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 3,
                    3, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 3, 2, 3,
                    3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 3, 2, 3,
                    3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 3, 2, 3, 2, 3,
                    3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 3,
                    3, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 3, 2, 3,
                    3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 3, 2, 3,
                    3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 3, 2, 3, 2, 3,
                    3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 3,
                    3, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 3, 2, 3,
                    3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 3, 2, 3,
                    3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 3, 2, 3, 2, 3,
                    3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 1,
                    3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3
                }
            }
        };
    }
}

[System.Serializable]
public class Materials
{
    public Material[] slotsMaterials;
    public Material stepMaterial;

    public Material GetStepMaterial
        => stepMaterial;
}

public struct MapInitData
{
    public int Width, Length;
    public int[] StartInstruction;
}
