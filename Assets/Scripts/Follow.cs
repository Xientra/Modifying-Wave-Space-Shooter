using UnityEngine;

public class Follow : MonoBehaviour
{
	public Transform target;
	public float smoothFactor = 2;

	private Vector3 offset;

	private void Start()
	{
		offset = transform.position - target.position;
	}

	private void LateUpdate()
	{
		transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothFactor * Time.deltaTime);
	}
}
