using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
	[SerializeField]
	private int health;

	public bool TakeDamage(int dmg)
	{
		health -= dmg;
		if (health <= 0)
		{
			Die();
			return true;
		}
		return false;
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
