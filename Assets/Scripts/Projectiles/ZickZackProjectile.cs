using UnityEngine;

public class ZickZackProjectile : Projectile
{
	public float zickTime = 0.5f;
	public float zickAngle = 30f;
	private bool flyRight;
	private float t;
	private Vector3 fwd;
	private Quaternion right, left;

	private void Start()
	{
		flyRight = Random.value < 0.5;
		fwd = transform.forward;
		right = transform.rotation * Quaternion.AngleAxis(zickAngle, transform.up);
		left = transform.rotation * Quaternion.AngleAxis(-zickAngle, transform.up);
	}

	private void Update()
	{
		t += Time.deltaTime;
		if (t > zickTime)
		{
			flyRight = !flyRight;
			t -= zickTime;
		}
		Debug.DrawRay(transform.position, fwd);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		body.MoveRotation(flyRight ? right : left);
	}
}
