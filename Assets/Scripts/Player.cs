using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Player : MonoBehaviour, IDamagable
{
	public static Player Instance { get; private set; }
	
	[SerializeField]
	private int maxHealth = 100;
	private int health;
	public float shotsPerSecond = 2;
	public GameObject projectilePrefab;

	public delegate void OnHealthChange(int health);
	public OnHealthChange onHealthChange;

	private float cooldown;
	private Collider coll;

	[SerializeField]
	private Inventory inventory;
	private bool inventoryVisible = false;

	private void Awake() 
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
		coll = GetComponent<Collider>();
	}
	private void Start()
	{
		health = maxHealth;
	}

	public bool TakeDamage(int dmg)
	{
		health -= dmg;
		onHealthChange?.Invoke(health);

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

	void Update()
	{
		cooldown -= Time.deltaTime;
		if (cooldown <= 0 && Input.GetButton("Fire1"))
		{
			Shoot();
			cooldown = 1 / shotsPerSecond;
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			inventoryVisible ^= true;
			inventory.gameObject.SetActive(inventoryVisible);
		}
	}

	private void Shoot()
	{
		Projectile prjt = Instantiate(projectilePrefab, transform.position, transform.rotation).GetComponent<Projectile>();
		prjt.SetPlayerProjectile(true);

		Physics.IgnoreCollision(prjt.GetComponent<Collider>(), coll);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Modification"))
		{
			// TODO insert modification into inventory here
			// TODO automatic equip? 
		}
	}

	public int getMaxHealth()
	{
		return maxHealth;
	}
}
