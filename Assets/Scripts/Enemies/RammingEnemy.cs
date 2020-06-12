using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RammingEnemy : Enemy
{

	[Header("Ramming Variables:")]
	public int rammingDamage = 1;


	void Update()
	{

	}

	private void FixedUpdate()
	{
		// rotate towards Player
		transform.forward = Vector3.RotateTowards(transform.forward, (-transform.position + target.transform.position).normalized, rotationSpeed, 0);

		// accelerate
		if (speed < maxSpeed) speed += acceleration;

		// move towards player
		rb.MovePosition(transform.position + transform.forward * speed);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			// deal dmg to player
			target.TakeDamage(rammingDamage);

			// self destruct
			this.TakeDamage(health);
		}
	}
}
