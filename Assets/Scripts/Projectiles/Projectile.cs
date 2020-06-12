using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
	public float speed = 1;
	public float damage = 10;

	protected Rigidbody body;

	private void Awake()
	{
		body = GetComponent<Rigidbody>();
		Destroy(gameObject, 10f); // Destroy after some time for cleanup
	}

	protected virtual void FixedUpdate()
	{
		body.MovePosition(body.position + speed * transform.forward * Time.fixedDeltaTime);
	}

	void OnCollisionEnter(Collision hit)
	{
		if (hit.gameObject.CompareTag("Enemy"))
		{
			Debug.Log("Damage enemy");
		}
		Destroy(gameObject);
	}
}
