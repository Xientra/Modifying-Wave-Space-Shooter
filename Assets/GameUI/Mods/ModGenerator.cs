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
		ModDrop drop     = drops[Random.Range(0, drops.Length)];
		ModPrefab prefab = Instantiate(basePrefab, transform);
		Modification mod = GetModification(drop.typ);
		mod.SetPlayerMod(drop.playerMod);
		prefab.InitPrefab(drop.icon, mod);
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
        // Define return value
        // -------------------
        Modification mod;

        switch (type)
        {
			case ModType.HommingMotion:
                mod = new HommingMotionModifier();
                (mod as HommingMotionModifier).SetDriftAngle(10);
                mod.SetPlayerMod(false);
                break;
			case ModType.Piercing:
                mod = new PiercingModifier();
                break;
			case ModType.Speed:
                mod = new SpeedModifier();
                break;
            case ModType.ZickZack:
                mod = new ZickZackMotionModifier();
                (mod as ZickZackMotionModifier).SetJitterStrength(10);
                mod.SetPlayerMod(false);
                break;
			case ModType.Shield:
				mod = new ShieldModifier();
                break;
            case ModType.ChainHit:
                mod = new ChainHitModifier();
                break;
            default:
				Debug.LogError("No modifier for "+type+" found!");
                return null;
        }

        // Return mod
        // ----------
        return mod;
    }
}

public enum ModType
{
    HommingMotion, Piercing, Speed, ZickZack, Shield, ChainHit
}

[System.Serializable]
class ModDrop
{
    public ModType typ;
    public Sprite icon;
	public bool playerMod;
}
