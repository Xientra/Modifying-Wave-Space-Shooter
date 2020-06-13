using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    InventorySlot[] slots = new InventorySlot[8];

    [SerializeField]
    public Color nonRemovableColor = new Color(219, 168, 57, 180); // orange

    public void insertModification(Modification mod, Sprite icon) 
    {
        for (int i = 0; i < slots.Length; i++) {
            if (slots[i] == null) {
                slots[i].setContent(mod);
                slots[i].setIcon(icon);

                if (!mod.IsRemovable())
                {
                    slots[i].setColor(nonRemovableColor);
                }

                break;
            }
        }
    }
}
