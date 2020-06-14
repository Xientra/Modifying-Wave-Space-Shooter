using UnityEngine;

public class Inventory : MonoBehaviour
{
	private Animator anim;
    private InventorySlot[] slots = new InventorySlot[8];

    private void Start()
    {
		anim = GetComponent<Animator>();
		var childs = GetComponentsInChildren<InventorySlot>();
		if (slots.Length != childs.Length)
			Debug.LogWarning("Inventory slots not matching inventory childs");

		for (int i = 0; i < slots.Length; i++)
			slots[i] = childs[i];
    }

	public void Show(bool show)
	{
		anim.SetBool("Show", show);
	}

	public void RemoveMod(Modification mod)
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].GetMod() == mod)
			{
				slots[i].Remove();
			}
		}
	}

    // won't do anything for a full inventory
    public bool InsertModification(Modification mod, Sprite icon) 
    {
        for (int i = 0; i < slots.Length; i++) {
            
            if (slots[i].IsEmpty()) {

				if (mod.IsPlayerMod())
					mod.SetModificationTarget(Player.Instance); // TODO: maybe player should add himself to ModificationManager as target

                Player.Instance.GetModificationManager().AddModification(mod);

                slots[i].SetMod(mod);
                slots[i].SetIcon(icon);

                if (!mod.IsRemovable())
                {
                    slots[i].ColorNonRemovable();
                }

                return true;
            }
        }

        return false;
    }
}
