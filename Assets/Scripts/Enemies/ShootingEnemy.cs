using UnityEngine;

public class ShootingEnemy : Enemy
{

	public float minPreferredDistance = 10f;
	public float maxPreferredDistance = 12f;


	private float sidewaySpeed = 0;

	private void FixedUpdate()
	{
		if (DistanceToPlayer() > maxPreferredDistance)
		{
			speed += acceleration;
		}
		else if (DistanceToPlayer() < minPreferredDistance)
		{
			speed -= acceleration / 2;
		}
		else {
			if (speed > 0) speed -= acceleration;
			if (speed < 0) speed += acceleration;
		}

		speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

		// move towards player
		transform.Translate(transform.forward * speed, null);

		// Rotate towards point before player
		transform.forward = Vector3.RotateTowards(transform.forward, (-transform.position + WaveController.player.transform.position).normalized, rotationSpeed, 0);



		if (DistanceToPlayer() < maxPreferredDistance)
		{
			if (sidewaySpeed > maxSpeed / 2) sidewaySpeed -= acceleration;
			if (sidewaySpeed < maxSpeed / 2) sidewaySpeed += acceleration;

			// move sideways
			transform.Translate(transform.right * sidewaySpeed, null);

			// Rotate 90 to player
			//transform.forward = Vector3.RotateTowards(transform.forward, Quaternion.AngleAxis(90, Vector3.up) * (-transform.position + targetPosition).normalized, rotationSpeed, 0);
		}
	}

	private float DistanceToPlayer()
	{
		return (-transform.position + WaveController.player.transform.position).magnitude;
	}
}
