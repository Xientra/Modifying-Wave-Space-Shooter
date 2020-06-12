using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{

	public int health;

	public float acceleration = 0.1f;
	public float maxSpeed = 1f;
	protected float speed = 0;

	public float rotationSpeed = 0.1f;

	protected Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	protected virtual void Start()
	{
		transform.LookAt(WaveController.player.transform.position);
	}

	/// <summary>
	/// Returns true if the damage kills.
	/// </summary>
	public bool TakeDamage(int dmg)
	{
		health -= dmg;
		if (health <= 0)
		{
			health = 0;

			Die();
			return true;
		}

		return false;
	}

	private void Die()
	{
		Destroy(this.gameObject);
	}
}
