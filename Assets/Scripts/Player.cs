using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Player : MonoBehaviour, IDamagable
{
	public static Player Instance { get; private set; }
	
	[SerializeField]
	private int health = 100;
	public float shotsPerSecond = 2;
	public GameObject projectilePrefab;

	public delegate void OnLifePointsChange(int lifepoints);
	public OnLifePointsChange onLifePointsChange;

	private float cooldown;
	private Collider coll;

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

	void Update()
	{
		cooldown -= Time.deltaTime;
		if (cooldown <= 0 && Input.GetButton("Fire1"))
		{
			Shoot();
			cooldown = 1 / shotsPerSecond;
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
			// TODO: insert modification into inventory here
		}
	}
}
