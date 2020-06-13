using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IPointerClickHandler
{
    // private Modification<Object> content;

    private Image background;
    [SerializeField]
    private Color baseColor = new Color(14, 30, 46, 180);
    [SerializeField]
    public Color nonRemoveableColor = new Color(219, 168, 57, 180); // orange
    [SerializeField]
    public Color equippedColor = new Color(140, 212, 57, 180); // green

    void Awake() {
        this.background = this.gameObject.GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) 
        {
            //Debug.Log("RightClick detected");
            //if (content.IsRemovable)
            //{
            //    // depending on type: remove from player or projectile

            //    // reset
            //    content = null;
            //    background.color = baseColor;
            //}
        }
        /*if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("LeftClick detected");
            content.SetEquipped(true);
            background.color = equippedColor;
        }*/
    }

    //public void insertModification(Modification<Object> mod) 
    //{
    //    this.content = mod;
    //    if (!mod.IsRemovable)
    //    {
    //        background.color = nonRemoveableColor;
    //    }
    //    else 
    //    {
    //        background.color = baseColor;
    //    }
    //}
}
