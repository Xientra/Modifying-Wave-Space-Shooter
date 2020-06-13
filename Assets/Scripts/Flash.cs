using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Flash : MonoBehaviour
{
	public float flashTime = 0.5f;
	public float alpha = 0.6f;

	private Image img;
	private bool flashing;
	private float t;
	private float halfTime;

	private void Start()
	{
		Player.Instance.onHealthChange += FlashOnDamage;
		img = GetComponent<Image>();
		halfTime = flashTime / 2;
	}

	private void Update()
	{
		if (flashing)
		{
			t += Time.deltaTime;
			if (t > flashTime)
			{
				// we are done
				t = 0;
				flashing = false;
			} else
			{
				float val = Mathf.PingPong(t, halfTime) / halfTime;
				img.color = new Color(1, 1, 1, val * alpha);
			}
		}
	}

	public void FlashOnDamage(int newHealth)
	{
		DoFlash();
	}

	public void DoFlash()
	{
		flashing = true;
		// If we are already flashing, and flash is decreasing, change to increasing
		// (e.g. 1 second flash time, 0.8 elapsed -> change to 0.3 (no visible change since it is symmetric))
		if (t > halfTime)
			t -= halfTime;
	}
}
