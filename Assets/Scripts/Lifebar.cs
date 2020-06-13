using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image image;
    private int maxValue;

    void Start()
    {
        Player.Instance.onHealthChange += UpdateLifebarUI;
        maxValue = Player.Instance.getMaxHealth();
    }

    public void UpdateLifebarUI(int amount) {
        if (amount <= 0.1 * maxValue)
        {
            image.fillAmount = 0.1f;
        }
        else if (amount <= 0.2 * maxValue) 
        {
            image.fillAmount = 0.2f;
        }
        else if (amount <= 0.3 * maxValue)
        {
            image.fillAmount = 0.3f;
        }
        else if (amount <= 0.4 * maxValue)
        {
            image.fillAmount = 0.4f;
        }
        else if (amount <= 0.5 * maxValue)
        {
            image.fillAmount = 0.5f;
        }
        else if (amount <= 0.6 * maxValue)
        {
            image.fillAmount = 0.6f;
        }
        else if (amount <= 0.7 * maxValue)
        {
            image.fillAmount = 0.7f;
        }
        else if (amount <= 0.8 * maxValue)
        {
            image.fillAmount = 0.8f;
        }
        else if (amount <= 0.9 * maxValue)
        {
            image.fillAmount = 0.9f;
        }
        else if (amount == maxValue)
        {
            image.fillAmount = 1.0f;
        }
    }
}
