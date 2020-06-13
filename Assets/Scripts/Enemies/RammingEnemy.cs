using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RammingEnemy : Enemy
{
	private void FixedUpdate()
	{
		if (target != null)
		{
			// rotate towards Player
			transform.forward = Vector3.RotateTowards(transform.forward, (-transform.position + target.transform.position).normalized, rotationSpeed, 0);

			// accelerate
			if (speed < maxSpeed) speed += acceleration;
		}
		// apply movement
		rb.MovePosition(transform.position + transform.forward * speed);
	}
}
