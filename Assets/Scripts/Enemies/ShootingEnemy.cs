﻿using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ShootingEnemy : Enemy
{
	[Header("Shooting Enemy Moving Behavior:")]

	public float minPreferredDistance = 10f;
	public float maxPreferredDistance = 12f;

	private float sidewaySpeed = 0;

	[Header("Shooting:")]

	public GameObject projectilePrefab;

	public float cooldown = 3f;
	private float currentCooldown;

	private Collider coll;

	protected override void Start()
	{
		base.Start();

		currentCooldown = cooldown;
		coll = GetComponent<Collider>();
	}

	private void FixedUpdate()
	{
		if (target != null)
		{
			// accelerate if to far away accelerate backwarts if to close
			if (DistanceToPlayer() > maxPreferredDistance)
			{
				speed += acceleration;
			}
			else if (DistanceToPlayer() < minPreferredDistance)
			{
				speed -= acceleration / 2;
			}
			else
			{
				if (speed > 0) speed -= acceleration;
				if (speed < 0) speed += acceleration;
			}

			speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

			// Rotate towards point before player
			transform.forward = Vector3.RotateTowards(transform.forward, (-transform.position + target.transform.position).normalized, rotationSpeed, 0);

			// move to prefferedDistance in player direction
			Vector3 toMove = transform.position + (transform.forward * speed);

			// start circling if close enougth
			if (DistanceToPlayer() < maxPreferredDistance)
			{
				if (sidewaySpeed > maxSpeed / 2) sidewaySpeed -= acceleration;
				if (sidewaySpeed < maxSpeed / 2) sidewaySpeed += acceleration;

				// move sideways
				toMove += transform.right * sidewaySpeed;

				CheckShoot();
			}

			// apply movement
			rb.MovePosition(toMove);
		}
	}

	private float DistanceToPlayer()
	{
		return (-transform.position + target.transform.position).magnitude;
	}

	private void CheckShoot()
	{
		currentCooldown -= Time.fixedDeltaTime;

		if (currentCooldown <= 0)
		{
			currentCooldown = cooldown;

			Shoot();
		}
	}

	private void Shoot()
	{
		Projectile prjt = Instantiate(projectilePrefab, transform.position, Quaternion.LookRotation(-transform.position + target.transform.position)).GetComponent<Projectile>();
		prjt.SetPlayerProjectile(false);

		Physics.IgnoreCollision(prjt.GetComponent<Collider>(), coll);
	}
}
