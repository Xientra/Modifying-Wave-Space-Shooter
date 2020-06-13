using System;
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

	public delegate void OnStartCallback();
	public OnStartCallback onStartCallback;

	private void Awake()
	{
		body = GetComponent<Rigidbody>();
		Destroy(gameObject, 10f); // Destroy after some time for cleanup
		SetPlayerProjectile(isPlayerProjectile);
	}

	private void Start()
	{
		onStartCallback?.Invoke();
	}

	public void SetPlayerProjectile(bool isPlayer)
	{
		isPlayerProjectile = isPlayer;
		targetTag = isPlayerProjectile ? "Enemy" : "Player";
		gameObject.layer = LayerMask.NameToLayer(isPlayerProjectile ? "Default" : "EnemyBullets"); // TODO: improve this design
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
