using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour, IDamagable
{

	public int health;

	public float acceleration = 0.1f;
	public float maxSpeed = 1f;
	protected float speed = 0;

	public float rotationSpeed = 0.1f;

	protected Player target;

	protected Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	protected virtual void Start()
	{
		transform.LookAt(target.transform.position);
	}

	public void SetTarget(Player target)
	{
		this.target = target;
	}

	/// <summary>
	/// Returns true if the damage kills.
	/// </summary>
	public bool TakeDamage(int dmg) // TODO: move TakeDamage, Shoot, health, and cooldown in superclass with Player?
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

	public void Die()
	{
		Destroy(this.gameObject);
	}
}
