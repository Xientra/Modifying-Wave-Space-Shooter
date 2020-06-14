using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour, IDamagable
{
    /*=====================*\
    |*   Unity Functions   *|
    \*=====================*/

    protected virtual void Start()
	{
		transform.LookAt(target.transform.position);
        // defaultMaxSpeed = maxSpeed;

        trailRenderer = trailRendererChildObj.GetComponent<TrailRenderer>();
    }

    private void Update()
    {

        if (target != null)
            if ((-transform.position + target.transform.position).magnitude > maxDistanceToPlayer)
            {
                transform.position = WaveController.Instance.GetRandomPositionAroundPlayer();
                trailRenderer.Clear();
            }
    }

    /*============*\
    |*   Events   *|
    \*============*/

    void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			// deal dmg to player
			target.TakeDamage(collisionDamage);

            // self destruct
            Die();
		}
	}

    public UnityEvent onDie;

    /*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

        /*============*\
        |*   Setter   *|
        \*============*/

        public void SetTarget(Player target)
	    {
		    this.target = target;
	    }

        /*===============*\
        |*   Utilities   *|
        \*===============*/

        public void Die()
	    {
            // chance to spawn a modification
		    float r = Random.value;
		    if (r <= dropChance) {
			    ModPrefab prefab = ModGenerator.Instance.GetRandomPrefab();
			    prefab.transform.position = this.transform.position;
			    prefab.gameObject.SetActive(true);
		    }

            // Add points
            // ---------
            WaveController.Instance.AddEnemyKillScore();

            // Create on Death Effect
            // ----------------------
		    GameObject temp = Instantiate(onDeathEffect, transform.position, onDeathEffect.transform.rotation);
		    
            // Destroy on Death Effect after 4 secs
            // ------------------------------------
            Destroy(temp, 4);

            // Destory this
            // ------------
            onDie.Invoke();
		    Destroy(gameObject);
	    }

    /*================================*\
    |*   Protected Member Variables   *|
    \*================================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

	    protected float speed = 0;
	    [SerializeField] protected Rigidbody rb = null;
        [SerializeField] protected float acceleration  = 0.1f;
	    [SerializeField] protected float maxSpeed      = 1f;
	    [SerializeField] protected float rotationSpeed = 0.1f;
        [SerializeField] protected float maxDistanceToPlayer = 50f;
        public GameObject trailRendererChildObj;
        private TrailRenderer trailRenderer;

        /*====================*\
        |*   Runtime memory   *|
        \*====================*/

        protected Player target = null;

    /*================================*\
    |*   Protected Member Functions   *|
    \*================================*/

        /*===============*\
        |*   Utilities   *|
        \*===============*/

        protected float DistanceToPlayer()
	    { return (target.transform.position - transform.position).magnitude; }

        public bool TakeDamage(int dmg)
	    {
            // Decrease help by damage
            // -----------------------
            health -= dmg;

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

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] private GameObject onDeathEffect       = null;
	    
        [SerializeField] private float dropChance       = 0.1f;
        [SerializeField] private int collisionDamage    = 10;

        /*====================*\
        |*   Runtime memory   *|
        \*====================*/

        [SerializeField]
        private float health = 0;
        // private float defaultMaxSpeed;
}
