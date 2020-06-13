using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IPointerClickHandler
{
    private bool visible = false;
    private Modification content;
    private Color baseColor = new Color(14, 30, 46, 180);

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) 
        {
            Debug.Log("RightClick detected");
            if (content.isRemovable())
            {
                // depending on type: remove from player or projectile

                // reset
                content = null;
                this.gameObject.GetComponent<Image>().color = baseColor;
            }
        }
        /*if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("LeftClick detected");
            content.SetEquipped(true);
            this.gameObject.GetComponent<Image>().color = Color.green;
        }*/
    }

    //TODO move this somewhere outside -> game controller?
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            visible ^= true;
            // TODO replace with an Inventory variable / method
            this.gameObject.SetActive(visible);
        }
    }

    public void insertModification(Modification mod) 
    {
        this.content = mod;
        if (!mod.isRemovable())
        {
            this.gameObject.GetComponent<Image>().color = new Color(219, 168, 57, 180); // orange
        }
        else 
        {
            this.gameObject.GetComponent<Image>().color = baseColor;
        }
    }
}
