﻿/*==================*\
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
        baseMotionModifier.SetSpeed(1);
        baseMotionModifier.SetJitterStrength(1);
        baseMotionModifier.SetModificationTarget(this);
        m_modificationManager.AddModification(baseMotionModifier);

        ZickZackMotionModifier zickzackModifier = new ZickZackMotionModifier();
        zickzackModifier.SetModificationTarget(this);
        m_modificationManager.AddModification(zickzackModifier);

        HommingMotionModifier hommingModifier = new HommingMotionModifier();
        hommingModifier.SetModificationTarget(this);
        hommingModifier.SetHommingTarget(target);
        m_modificationManager.AddModification(hommingModifier);

        //SpeedModifier speedModifier = new SpeedModifier();
        //speedModifier.SetModificationTarget(this.transform);
        //speedModifier.SetAdditionalSpeed(1);
        //AddModification(speedModifier);

        SetPlayerProjectile(isPlayerProjectile);
    }
    private void Start()
	{
		onStartCallback?.Invoke();
	}

    public Transform target = null;

    /*============*\
    |*   Events   *|
    \*============*/

    void OnCollisionEnter(Collision hit)
    {
		if (hit.gameObject.CompareTag(targetTag))
		{
			IDamagable target = hit.gameObject.GetComponent<IDamagable>();
			target.TakeDamage(damage);
		}
		Destroy(gameObject);
    }

    /*===============*\
    |*   Delegates   *|
    \*===============*/

	public delegate void OnStartCallback();
	public OnStartCallback onStartCallback;

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

        protected Rigidbody body;
	    protected string targetTag;

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/
        
	    [SerializeField] private int damage = 10;
	    [SerializeField] private bool isPlayerProjectile; // does it hit enemies or the player?
}
