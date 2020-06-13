using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float speed = 1;
	public float damage = 10;

    private void Awake() { Destroy(gameObject, 10f); } // Destroy after some time for cleanup 

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Damage enemy");
        }
        Destroy(gameObject);
    }
}
