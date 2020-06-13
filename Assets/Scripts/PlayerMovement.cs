using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	public float speedFactor = 10;
	public float maxSpeed = 10;
	public float drag = 1;

	private Rigidbody rbody;
	private Vector3 velocity;
	private Camera cam;
	private Quaternion lookDirection;

	private void Awake()
	{
		rbody = GetComponent<Rigidbody>();
		cam = Camera.main;
		lookDirection = transform.rotation;
	}

	private void Update()
	{
		Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		input = Vector2.ClampMagnitude(input, 1f);

		velocity += new Vector3(input.x, 0, input.y) * Time.deltaTime * speedFactor;

		Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
		Vector3 lookTarget = cam.ScreenToWorldPoint(mouse);
		lookTarget.y = transform.position.y;
		lookDirection = Quaternion.LookRotation(lookTarget - transform.position);
	}

	private void FixedUpdate()
	{
		velocity = velocity * (1 - Time.fixedDeltaTime * drag);
		velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
		rbody.velocity = velocity;
		rbody.MoveRotation(lookDirection);
	}
}
