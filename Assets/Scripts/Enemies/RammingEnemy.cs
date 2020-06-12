using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RammingEnemy : Enemy
{
	void Update()
	{

	}

	private void FixedUpdate()
	{
		// accelerate
		if (speed < maxSpeed) speed += acceleration;

		// move
		transform.Translate(transform.forward * speed, null);

		// rotate towards Player
		//transform.forward = Vector3.Lerp(transform.forward, (-transform.position + WaveController.player.transform.position).normalized, rotationSpeed);
		transform.forward = Vector3.RotateTowards(transform.forward, (-transform.position + WaveController.player.transform.position).normalized, rotationSpeed, 0);
	}
}
