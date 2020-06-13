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
		Debug.LogError("No sprite for " + type + " found!");
        return null;
    }

    public Modification GetModification(ModType type)
    {
        switch (type)
        {
			case ModType.HommingMotion:
				return new HommingMotionModifier();
			case ModType.Piercing:
				return new PiercingModifier();
			case ModType.Speed:
                return new SpeedModifier();
            case ModType.ZickZack:
                return new ZickZackMotionModifier();
			case ModType.Shield:
				return new ShieldModifier();
            default:
				Debug.LogError("No modifier for "+type+" found!");
                return null;
        }
    }
}

public enum ModType
{
    HommingMotion, Piercing, Speed, ZickZack, Shield
}

[System.Serializable]
class ModDrop
{
    public ModType typ;
    public Sprite icon;
}
