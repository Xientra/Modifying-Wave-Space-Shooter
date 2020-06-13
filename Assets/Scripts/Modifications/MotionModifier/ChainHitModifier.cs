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

			target.onStartCallback += SetAdditionalHits;
		}
	}

	private void SetAdditionalHits()
	{
		target.hitAmount += additionalHits;
	}

	private void RotateToNextEnemy()
	{
		// get neares enemy to this projectile
		GameObject nearestEnemy = GetSecondNearesEnemyToPoint(target.transform.position);

		// then calcualte vector pointing it thats enemy direction and set this projectiles rotation to the new direction
		if (nearestEnemy != null)
			target.transform.forward = (-target.transform.position + nearestEnemy.transform.position).normalized;
	}

	public GameObject GetSecondNearesEnemyToPoint(Vector3 point)
	{

		GameObject[] enemies = WaveController.Instance.GetActiveEnemies();

		GameObject nearest = null;
		GameObject secondNearest = null;

		for (int i = 0; i < enemies.Length; i++)
		{
			if (enemies[i] != null)
			{
				if (nearest == null || (nearest.transform.position - point).sqrMagnitude > (enemies[i].transform.position - point).sqrMagnitude)
				{
					secondNearest = nearest;
					nearest = enemies[i];
				}
			}
		}

		return nearest;
	}
}
