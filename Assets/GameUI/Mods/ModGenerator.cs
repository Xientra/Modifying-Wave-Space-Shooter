using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModGenerator : MonoBehaviour
{
    public static ModGenerator Instance { get; private set; }

    [SerializeField]
    private ModDrop[] drops;

    [SerializeField]
    private ModPrefab basePrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        basePrefab.gameObject.SetActive(false);
    }

    public ModPrefab GetRandomPrefab()
    {
        var arr = System.Enum.GetValues(typeof(ModType));
        int idx = Random.Range(0, arr.Length);
        ModType randomType = (ModType)arr.GetValue(idx);

        ModPrefab prefab = Instantiate(basePrefab, transform);
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
