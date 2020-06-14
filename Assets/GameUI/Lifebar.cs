using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image image;
    private int maxValue;
#pragma warning restore 0649

    void Start()
    {
        Player.Instance.onHealthChange += UpdateLifebarUI;
        maxValue = Player.Instance.getMaxHealth();
    }

    public void UpdateLifebarUI(int amount) {
		// Floor on first decimal place: 0.62 -> 0.6, 0.88888 -> 0.8, 0.2123 -> 0.2
		image.fillAmount = Mathf.Clamp01(Mathf.Ceil(amount / (float)maxValue * 10) / 10);
    }
}
