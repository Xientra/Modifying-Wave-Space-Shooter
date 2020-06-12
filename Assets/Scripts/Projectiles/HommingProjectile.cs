using UnityEngine;

public class HommingProjectile : Projectile
{
	public Transform target;
	public float rotationSpeed = 1;

    protected override void FixedUpdate()
	{
		base.FixedUpdate();
		Vector3 targetDirection = target.position - transform.position;
		Debug.DrawRay(transform.position, targetDirection);
		Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.fixedDeltaTime, 0);
		body.MoveRotation(Quaternion.LookRotation(newDirection));
	}
}
