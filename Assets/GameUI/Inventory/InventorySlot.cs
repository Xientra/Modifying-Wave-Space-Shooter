using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    private Modification mod = null;
    private Sprite icon = null;

    private Image background;

    [SerializeField]
    private Color baseColor = new Color(14, 30, 46, 180);
    [SerializeField]
    public Color nonRemovableColor = new Color(219, 168, 57, 180); // orange
    /*[SerializeField]
    public Color equippedColor = new Color(140, 212, 57, 180); // green*/

    void Awake()
    {
        this.background = this.gameObject.GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("RightClick detected");
            if (mod.IsRemovable())
            {
                // TODO: depending on type: remove from player or projectile

               // reset slot
               mod = null;
               icon = null;
               background.color = baseColor;
            }
        }
        /*if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("LeftClick detected");
            content.SetEquipped(true);
            background.color = equippedColor;
        }*/
    }

    public void colorNonRemovable()
    {
        background.color = nonRemovableColor;
    }

    public bool isEmpty()
    {
        return mod == null && icon == null;
    }

    public void setMod(Modification content) 
    {
        this.mod = content;
    }

    public void setIcon(Sprite icon) 
    {
        this.icon = icon;
    }
}
