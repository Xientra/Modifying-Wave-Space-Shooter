﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
	public Image iconHolder;
    private Modification mod = null;
    private Sprite icon = null;

    private Image background;

    [SerializeField]
    public Color nonRemovableColor = new Color(219, 168, 57, 180); // orange
    /*[SerializeField]
    public Color equippedColor = new Color(140, 212, 57, 180); // green*/

    void Awake()
    {
        background = gameObject.GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
			Remove();
        }
        /*if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("LeftClick detected");
            content.SetEquipped(true);
            background.color = equippedColor;
        }*/
    }

	public void Remove()
	{
		if (mod != null && mod.IsRemovable())
		{
			Player.Instance.GetModificationManager().RemoveModiciation(mod);

			// reset slot
			SetMod(null);
			SetIcon(null);
			iconHolder.enabled = false;
		}
	}
	

    public void ColorNonRemovable()
    {
        background.color = nonRemovableColor;
    }

    public bool IsEmpty()
    {
        return mod == null && icon == null;
    }

	public Modification GetMod()
	{
		return mod;
	}

    public void SetMod(Modification content) 
    {
        this.mod = content;
    }

    public void SetIcon(Sprite icon) 
    {
        this.icon = icon;
		iconHolder.sprite = icon;
		iconHolder.enabled = true;
    }
}
