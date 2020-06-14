/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

/*========================*\
|*   CLASS:  Projectile   *|
\*========================*/

public class Projectile : ModificationObject
{
    /*=====================*\
    |*   Unity Functions   *|
    \*=====================*/

    void Awake() 
    {
        // Destroy after some time for cleanup 
        // ------------------------------------
        Destroy(gameObject, 10f);

        // Define base Motion
        // ------------------
        BaseMotionModifier baseMotionModifier = new BaseMotionModifier();
        baseMotionModifier.SetSpeed(25);
        baseMotionModifier.SetModificationTarget(this);
        m_modificationManager.AddModification(baseMotionModifier);




        /*
        PiercingModifier pm = new PiercingModifier();
        pm.SetModificationTarget(this);
        m_modificationManager.AddModification(pm);
        */

        
        ChainHitModifier cm = new ChainHitModifier();
        cm.SetModificationTarget(this);
        m_modificationManager.AddModification(cm);
        

        /*
        ZickZackMotionModifier zickzackModifier = new ZickZackMotionModifier();
        zickzackModifier.SetJitterStrength(20);
        zickzackModifier.SetModificationTarget(this);
        m_modificationManager.AddModification(zickzackModifier);
        */

        
        //HommingMotionModifier hommingModifier = new HommingMotionModifier();
        //hommingModifier.SetModificationTarget(this);
        //m_modificationManager.AddModification(hommingModifier);
        

        /*
        SpeedModifier speedModifier = new SpeedModifier();
        speedModifier.SetModificationTarget(this);
        speedModifier.SetAdditionalSpeed(1);
        m_modificationManager.AddModification(speedModifier);
        */

        /*
        StartSlowSpeedUpMotionModifier sssumm = new StartSlowSpeedUpMotionModifier();
        sssumm.SetModificationTarget(this);
        m_modificationManager.AddModification(sssumm);
        */

        SetPlayerProjectile(isPlayerProjectile);
    }
    private void Start()
	{
		onStartCallback?.Invoke();
	}
    
    /*============*\
    |*   Events   *|
    \*============*/

    void OnCollisionEnter(Collision hit)
    {
		if (hit.gameObject.CompareTag(targetTag))
		{
			IDamagable target = hit.gameObject.GetComponent<IDamagable>();
            if (target.TakeDamage(damage))
                onKillCallback?.Invoke();
        }

        // create effect
        if (hitEffectPrefab != null)
        {
            GameObject go = Instantiate(hitEffectPrefab, transform.position, hitEffectPrefab.transform.rotation);
            Destroy(go, 2);
        }

        onHitCallback?.Invoke(hit);
        if (hitAmount == 0)
            Destroy(gameObject);
        else hitAmount--;
    }

    /*===============*\
    |*   Delegates   *|
    \*===============*/

	public delegate void OnStartCallback();
	public OnStartCallback onStartCallback;

    public delegate void OnHitCallback(Collision collider);
    public OnHitCallback onHitCallback;

    public delegate void OnKillCallback();
    public OnKillCallback onKillCallback;

    /*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

    /*===============*\
    |*   Utilities   *|
    \*===============*/

    public void SetPlayerProjectile(bool isPlayer)
	    {
		    isPlayerProjectile = isPlayer;
		    targetTag = isPlayerProjectile ? "Enemy" : "Player";
		    gameObject.layer = LayerMask.NameToLayer(isPlayerProjectile ? "Default" : "EnemyBullets"); // TODO: improve this design
	    }
        
    /*================================*\
    |*   Protected Member Functions   *|
    \*================================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        public GameObject hitEffectPrefab;
        protected Rigidbody body;
	    protected string targetTag;

    /// <summary> How often the projectile can hit something bevore it destroys itself. </summary>
    public int hitAmount = 0;

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/
        
	    [SerializeField] private int damage = 10;
	    [SerializeField] private bool isPlayerProjectile; // does it hit enemies or the player?
}
