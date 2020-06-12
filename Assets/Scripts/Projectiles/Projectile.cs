using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
	public float speed = 1;
	public int damage = 10;
	public bool isPlayerProjectile; // does it hit enemies or the player?

	protected Rigidbody body;
	protected string targetTag;

	private void Awake()
	{
		body = GetComponent<Rigidbody>();
		Destroy(gameObject, 10f); // Destroy after some time for cleanup
		targetTag = isPlayerProjectile ? "Enemy" : "Player";
	}

	protected virtual void FixedUpdate()
	{
		body.MovePosition(body.position + speed * transform.forward * Time.fixedDeltaTime);
	}

	void OnCollisionEnter(Collision hit)
	{
		if (hit.gameObject.CompareTag(targetTag))
		{
			IDamagable target = hit.gameObject.GetComponent<IDamagable>();
			target.TakeDamage(damage);
		}
		Destroy(gameObject);
	}
}
