using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour, IDamagable
{
	[SerializeField]
	private float dropChance = 0.1f;
	
	public GameObject onDeathEffect;

	public int health;

	public float acceleration = 0.1f;
	public float maxSpeed = 1f;
	private float defaultMaxSpeed;
	protected float speed = 0;

	public float rotationSpeed = 0.1f;

	public int collisionDamage;

	protected Player target;

	protected Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	protected virtual void Start()
	{
		transform.LookAt(target.transform.position);
		defaultMaxSpeed = maxSpeed;
	}

	public void SetTarget(Player target)
	{
		this.target = target;
	}

	protected float DistanceToPlayer()
	{
		return (-transform.position + target.transform.position).magnitude;
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
		// chance to spawn a modification
		float r = Random.value;
		if (r <= dropChance) {
			ModPrefab prefab = ModGenerator.Instance.GetRandomPrefab();
			prefab.transform.position = this.transform.position;
			prefab.gameObject.SetActive(true);
		}

		GameObject temp = Instantiate(onDeathEffect, transform.position, onDeathEffect.transform.rotation);
		Destroy(temp, 4);
		Destroy(this.gameObject);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			// deal dmg to player
			target.TakeDamage(collisionDamage);

			// self destruct
			this.TakeDamage(health);
		}
	}
}
