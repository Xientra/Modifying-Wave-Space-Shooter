using TMPro;
using UnityEngine;

public class ChainHitModifier : Modification
{
	private int additionalHits = 1;

	private Projectile target;


	public override void SetModificationTarget(ModificationObject modificationTarget)
	{
		base.SetModificationTarget(modificationTarget);

		if (m_modificationTarget is Projectile)
		{
			target = (Projectile)m_modificationTarget;
			target.onHitCallback += RotateToNextEnemy;

			target.onStartCallback += AddHits;
		}
	}

	private void AddHits()
	{
		if (m_modificationTarget is Projectile)
		{
			target.hitAmount += additionalHits;
		}
	}

	private void RotateToNextEnemy(Projectile theProjectileThatHit, Collision collision)
	{
		if (target == theProjectileThatHit)
		{
			// get neares enemy to this projectile
			GameObject nearestEnemy = GetNearesEnemyToPoint(target.transform.position, collision.gameObject);

			// then calcualte vector pointing it thats enemy direction and set this projectiles rotation to the new direction
			if (nearestEnemy != null)
				target.transform.forward = (-target.transform.position + nearestEnemy.transform.position).normalized;

			Physics.IgnoreCollision(target.GetComponent<Collider>(), collision.collider);
		}
	}

	public GameObject GetNearesEnemyToPoint(Vector3 point, GameObject notThisOne)
	{

		GameObject[] enemies = WaveController.Instance.GetActiveEnemies();

		GameObject result = null;

		for (int i = 0; i < enemies.Length; i++)
		{
			if (enemies[i] != null)
			{
				if (result == null || (result.transform.position - point).sqrMagnitude > (enemies[i].transform.position - point).sqrMagnitude)
				{
					if (enemies[i].gameObject != notThisOne)
						result = enemies[i];
				}
			}
		}

		return result;
	}
}
