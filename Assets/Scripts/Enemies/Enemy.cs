using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{

	public float health;

	public float acceleration = 0.1f;
	public float maxSpeed = 1f;
	protected float speed = 0;

	public float rotationSpeed = 0.1f;

	protected Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	protected virtual void Start()
	{
		transform.LookAt(WaveController.player.transform.position);
	}
}
