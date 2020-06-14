using UnityEngine;

public class MoonDamage : ApplyDamageOnHit
{
	public float cooldown = 2f; // prevent multi collisions
	public float maxSpeed = 50f; // normal player speed is around 50

	private float t;

	private void Update()
	{
		if (t > 0) t -= Time.deltaTime;
	}

	protected void OnCollisionEnter(Collision collision)
	{
		//collider.attachedRigidbody.velocity;
		// Early exit (Wrong tag)
		// ----------------------
		if (collision.transform.tag != m_tag) return;

		if (t > 0) return;

		t = cooldown;

		Rigidbody body = collision.gameObject.GetComponent<Rigidbody>();
		float speed = body.velocity.magnitude;

		// Fetch Damageable interface
		// --------------------------
		IDamagable damageableInterface = collision.gameObject.GetComponent<IDamagable>();

		// Early exit (No IDamageable object)
		// ----------------------------------
		if (damageableInterface == null) return;

		// Apply damage
		// ------------
		float fac = (speed / maxSpeed);
		int dmg = (int)Mathf.Round(fac * m_damage);
		damageableInterface.TakeDamage(dmg);
	}
}
