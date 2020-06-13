using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    InventorySlot[] slots = new InventorySlot[8];

    private void Awake()
    {
        Player.Instance.onModPickup += insertModification;
    }

    // won't do anything for a full inventory
    public void insertModification(ModPrefab prefab) 
    {
        for (int i = 0; i < slots.Length; i++) {
            
            if (slots[i].isEmpty()) {

                Modification mod = prefab.getMod();
                Player.Instance.GetModificationManager().AddModification(mod);

                slots[i].setMod(mod);
                slots[i].setIcon(prefab.getIcon());

                if (!mod.IsRemovable())
                {
                    slots[i].colorNonRemovable();
                }

                break;
            }
        }
    }
}
