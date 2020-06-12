using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RammingEnemy : Enemy
{

	public float acceleration = 0.1f;
	public float maxSpeed = 1f;
	private float speed = 0;

	public float rotationSpeed = 0.1f;

	/*
    protected override void Start()
    {
        base.Start();
    }
    */

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
