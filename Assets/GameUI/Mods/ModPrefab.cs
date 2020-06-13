using UnityEngine;

public class ModPrefab : MonoBehaviour
{
    private Sprite icon;
    private Modification mod;

    public void InitPrefab(Sprite icon, Modification mod)
    {
        this.icon = icon;
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = icon;

        this.mod = mod;
    }

    public Sprite GetIcon() { return icon; }
    public Modification GetMod() { return mod; }
}
