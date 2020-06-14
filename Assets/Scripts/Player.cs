/*===================*\
|*   System Usings   *|
\*===================*/

using System.Collections.Generic;

/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

/*===================*\
|*   CLASS: Player   *|
\*===================*/

public class Player : ModificationObject, IDamagable
{
    /*=====================*\
    |*   Unity Functions   *|
    \*=====================*/

    void Awake() 
	{
		// Create Player Singleton
		// -----------------------
		if (Instance != null)
		{
			Destroy(Instance);
		}
		Instance = this;
	}
    void Start()
	{
        // Define runtime
        // --------------
		health = maxHealth;
		m_modificationManager.modAdded += ModWasAdded;
		m_modificationManager.modRemoved += ModWasRemoved;
	}

	void Update()
	{
        // Update timer
        // ------------
		cooldown -= Time.deltaTime;

        // On timer done and fire button pressed
        // -------------------------------------
		if (cooldown <= 0 && Input.GetButton("Fire1"))
		{
			Shoot();

            // Add gatling modifier
            // --------------------
            float fireRate = 0;
            List<GatlingModifier> gatlings = m_modificationManager.CollectModifiers<GatlingModifier>();
            foreach (GatlingModifier gatling in gatlings) fireRate += gatling.GetSpeed();

			cooldown = 1 / ( shotsPerSecond + fireRate);
		}

        // On Inventory ButtonUp
        // ---------------------
		if (Input.GetButtonUp("Inventory"))
		{
			inventoryVisible ^= true;
			inventory.Show(inventoryVisible);
		}
	}

    /*============*\
    |*   Events   *|
    \*============*/

    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Modification"))
		{
			ModPrefab prefab = other.gameObject.GetComponent<ModPrefab>();
			if (inventory.InsertModification(prefab.GetMod(), prefab.GetIcon())) {
				Destroy(other.gameObject);
			}
		}
	}

    /*===============*\
    |*   Delegates   *|
    \*===============*/

    public delegate void OnHealthChange(int health);
	public delegate void OnModPickup(ModPrefab mod);
    public delegate void OnDeath();
	
    /*=============================*\
    |*   Public Member Variables   *|
    \*=============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        public OnHealthChange onHealthChange  = null;
	    public OnModPickup onModPickup        = null;
        public OnDeath onDeath                = null;
		public GameObject shield;
		public GameObject gatling;
		public GameObject homing;
    
    /*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

        /*============*\
        |*   Getter   *|
        \*============*/

        public int getMaxHealth()
	    {
		    return maxHealth;
	    }

        /*==============*\
        |*   Virtuals   *|
        \*==============*/

        public virtual void Die()
	    {
            // Create on Death Effect
            // ----------------------
		    GameObject temp = Instantiate(onDeathEffect, transform.position, onDeathEffect.transform.rotation);
		    
            // Destroy on Death Effect after 4 secs
            // ------------------------------------
            Destroy(temp, 4);

            // Destory this
            // ------------
		    Destroy(gameObject);

            // Invoke onDeath events
            // ---------------------
		    onDeath?.Invoke();
	    }
        /*===============*\
        |*   Utilities   *|
        \*===============*/

		public void ModWasAdded(Modification mod)
		{
			if (mod is ShieldModifier)
			{
				shield.SetActive(true);
			} else
			if (mod is HommingMotionModifier)
			{
				homing.SetActive(true);
			} else
			if (mod is GatlingModifier)
			{
				gatling.SetActive(true);
			}
		}

		// Count all active modifications of a type
		private int CountModifiers<T>() where T : Modification
		{
			return m_modificationManager.CollectModifiers<T>().Count;
		}

		public void ModWasRemoved(Modification mod)
		{
			if (mod is ShieldModifier)
			{
				if (CountModifiers<ShieldModifier>() == 0)
					shield.SetActive(false);
			} else
			if (mod is HommingMotionModifier)
			{
				if (CountModifiers<HommingMotionModifier>() == 0)
					homing.SetActive(false);
			} else
			if (mod is GatlingModifier)
			{
				if (CountModifiers<GatlingModifier>() == 0)
					gatling.SetActive(false);
			}
	}

		public void ShowShield(bool show)
		{
			shield.SetActive(show);
		}

	    public bool TakeDamage(int dmg)
	    {
            // Decrease help by damage
            // -----------------------
		    health -= ComputeDamage(dmg);
		    
            // Play audioclip
            // ---------------
            audioSource.Play();
		    onHealthChange?.Invoke(health);

            // Early exit (Still alive)
            // -----------------------
		    if (health > 0) return false;
		    
            // Call Die
            // --------
			Die();

            // Return true
            // -----------
		    return true;
	    }

    /*=============================*\
    |*   Public Static Functions   *|
    \*=============================*/

        /*===============*\
        |*   Utilities   *|
        \*===============*/

        public static Player Instance { get; private set; }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] private AudioSource audioSource        = null;
        [SerializeField] private Collider coll                  = null;
        [SerializeField] private Inventory inventory            = null;
        
    	[SerializeField] private GameObject onDeathEffect       = null;
	    [SerializeField] private GameObject projectilePrefab    = null;

        [SerializeField] private float shotsPerSecond           = 2;
	    [SerializeField] private int maxHealth                  = 100;

        /*====================*\
        |*   Runtime memory   *|
        \*====================*/

        private bool inventoryVisible = true;
	    private int health;
    	private float cooldown;
	
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
			List<ShieldModifier> exhaustedShields = new List<ShieldModifier>();
			foreach (ShieldModifier shield in shields)
			{
				int val = shield.GetShieldValue();
				if (dmg  < val)
				{
					// Shield did absorb all damage
					shield.SetShieldValue(val - dmg);
					dmg = 0;
					break; //don't return here -> maybe some shields are exhausted
				} else
				{
					// Too much damage, shield exhausted
					dmg -= val;
					exhaustedShields.Add(shield);
				}
			}
			foreach (ShieldModifier shield in exhaustedShields)
			{
				inventory.RemoveMod(shield);
			}

            // Return resulting damage or zero
            // -------------------------------
            return Mathf.Max(dmg, 0);
        }

        private void Shoot()
	    {
            // Create projectile
            // -----------------
		    Projectile prjt = Instantiate(projectilePrefab, transform.position, transform.rotation).GetComponent<Projectile>();
		    
            // Flag projectile as player projectile
            // ------------------------------------
            prjt.SetPlayerProjectile(true);

            // Add modifiers to projectile
            // ---------------------------
            foreach(Modification mod in m_modificationManager.GetModifications())
            {
                // Skip (Player mod)
                // -----------------
                if (mod.IsPlayerMod()) continue;

                // Write modifier to projectile
                // ----------------------------
                prjt.GetModificationManager().AddModification(mod);

				if (mod is ChainHitModifier) 
				{
					ChainHitModifier chm = new ChainHitModifier();
					chm.SetModificationTarget(prjt);
					prjt.GetModificationManager().AddModification(chm);
				}
				if (mod is PiercingModifier)
				{
					PiercingModifier pm = new PiercingModifier();
					pm.SetModificationTarget(this);
					prjt.GetModificationManager().AddModification(pm);
				}
			}

            // Disable player collision
            // ------------------------
		    Physics.IgnoreCollision(prjt.GetComponent<Collider>(), coll);
	    }
}
