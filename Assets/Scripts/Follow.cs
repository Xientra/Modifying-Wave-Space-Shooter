using UnityEngine;

public class Follow : MonoBehaviour
{
	public Transform target;
	private Vector3 offset;

	private void Start()
	{
		offset = transform.position - target.position;
	}

	private void Update()
	{
		transform.position = target.position + offset;
	}
}
