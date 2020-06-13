using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModPrefab : MonoBehaviour
{
    private Sprite icon;
    private Modification mod;

    public void InitPrefab(Sprite icon, Modification mod)
    {
        this.icon = icon;
        this.mod = mod;
    }
}
