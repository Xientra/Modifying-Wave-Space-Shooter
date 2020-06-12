using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
	public static Player Instance { get; private set; }
	
	[SerializeField]
	private int health;

	public delegate void OnLifePointsChange(int lifepoints);
	public OnLifePointsChange onLifePointsChange;

	private void Awake() 
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			i
		}
	}

	public bool TakeDamage(int dmg)
	{
		health -= dmg;
		onLifePointsChange?.Invoke(health);

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
