using UnityEngine;

public class Inventory : MonoBehaviour
{
    InventorySlot[] slots = new InventorySlot[8];

    private void Start()
    {
        Player.Instance.onModPickup += InsertModification;
		var childs = GetComponentsInChildren<InventorySlot>();
		if (slots.Length != childs.Length)
			Debug.LogWarning("Inventory slots not matching inventory childs");

		for (int i = 0; i < slots.Length; i++)
			slots[i] = childs[i];
    }

    // won't do anything for a full inventory
    public void InsertModification(ModPrefab prefab) 
    {
        for (int i = 0; i < slots.Length; i++) {
            
            if (slots[i].IsEmpty()) {

                Modification mod = prefab.GetMod();
				mod.SetModificationTarget(Player.Instance); // TODO: maybe player should add himself to ModificationManager as target
                Player.Instance.GetModificationManager().AddModification(mod);

                slots[i].SetMod(mod);
                slots[i].SetIcon(prefab.GetIcon());

                if (!mod.IsRemovable())
                {
                    slots[i].ColorNonRemovable();
                }

                break;
            }
        }
    }
}
