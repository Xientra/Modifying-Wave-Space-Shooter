using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private int health;

	public void TakeDamage(int dmg)
	{
		health -= dmg;
		if (health <= 0)
			Die();
	}

	public void Die()
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Modification"))
		{
			// TODO: insert modification into inventory here
		}
	}
}
