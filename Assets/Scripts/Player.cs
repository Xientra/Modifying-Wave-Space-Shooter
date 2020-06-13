using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]
public class Player : ModificationObject, IDamagable
{
	public static Player Instance { get; private set; }

	public GameObject onDeathEffect;

	[SerializeField]
	private int maxHealth = 100;
	private int health;
	public float shotsPerSecond = 2;
	public GameObject projectilePrefab;

	public delegate void OnHealthChange(int health);
	public OnHealthChange onHealthChange;

	private float cooldown;
	private Collider coll;

	private AudioSource audioSource;

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
		audioSource = GetComponent<AudioSource>();
	}
	private void Start()
	{
		health = maxHealth;
	}

	public bool TakeDamage(int dmg)
	{
		health -= ComputeDamage(dmg);
		audioSource.Play();
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
		GameObject temp = Instantiate(onDeathEffect, transform.position, onDeathEffect.transform.rotation);
		Destroy(temp, 4);
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

		if (Input.GetButtonUp("Inventory"))
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
			ModPrefab prefab = other.gameObject.GetComponent<ModPrefab>();
			inventory.InsertModification(prefab.GetMod(), prefab.GetIcon());
			Destroy(other.gameObject);
		}
	}

	public int getMaxHealth()
	{
		return maxHealth;
	}

    /*==============================*\
    |*   Private Member Functions   *|
    \*==============================*/

        /*=================*\
        |*   Auxiliaries   *|
        \*=================*/

        private int ComputeDamage(int dmg)
        {
            // Collect ShieldModifier
            // ----------------------
            List<ShieldModifier> shields = m_modificationManager.CollectModifiers<ShieldModifier>();

            // Decrease initial damage by shield value
            // ---------------------------------------
            foreach(ShieldModifier shield in shields) dmg -= shield.GetShieldValue();

            // Return resulting damage or zero
            // -------------------------------
            return Mathf.Max(dmg, 0);
        }
}
