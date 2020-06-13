using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModGenerator : MonoBehaviour
{
    [SerializeField]
    private ModDrop[] drops;

    [SerializeField]
    private ModPrefab basePrefab;

    public ModPrefab GetRandomPrefab()
    {
        var arr = System.Enum.GetValues(typeof(ModType));
        int idx = Random.Range(0, arr.Length);
        ModType randomType = (ModType)arr.GetValue(idx);

        ModPrefab prefab = Instantiate(basePrefab, transform.position, Quaternion.identity);
        prefab.InitPrefab(GetSprite(randomType), GetModification(randomType));
        return prefab;
    }

    public Sprite GetSprite(ModType type) 
    {
        for (int i = 0; i < drops.Length; i++) 
        {
            if (drops[i].typ == type) 
            {
                return drops[i].icon;
            }
        }

        // default
        return null;
    }

    public Modification GetModification(ModType type)
    {
        switch (type)
        {
            case ModType.Speed:
                return new SpeedModifier();
            case ModType.HommingMotion:
                return new HommingMotionModifier();
            case ModType.ZickZack:
                return new ZickZackMotionModifier();
            default:
                return null;
        }
    }
}

public enum ModType
{
    HommingMotion, Piercing, Speed, ZickZack
}

[System.Serializable]
class ModDrop
{
    public ModType typ;
    public Sprite icon;
}
