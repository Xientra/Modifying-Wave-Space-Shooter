using UnityEngine;

public class Inventory : MonoBehaviour
{
    InventorySlot[] slots = new InventorySlot[8];

    private void Start()
    {
		var childs = GetComponentsInChildren<InventorySlot>();
		if (slots.Length != childs.Length)
			Debug.LogWarning("Inventory slots not matching inventory childs");

		for (int i = 0; i < slots.Length; i++)
			slots[i] = childs[i];
    }

    // won't do anything for a full inventory
    public void InsertModification(Modification mod, Sprite icon) 
    {
        for (int i = 0; i < slots.Length; i++) {
            
            if (slots[i].IsEmpty()) {

				mod.SetModificationTarget(Player.Instance); // TODO: maybe player should add himself to ModificationManager as target
                Player.Instance.GetModificationManager().AddModification(mod);

                slots[i].SetMod(mod);
                slots[i].SetIcon(icon);

                if (!mod.IsRemovable())
                {
                    slots[i].ColorNonRemovable();
                }

                break;
            }
        }
    }
}
