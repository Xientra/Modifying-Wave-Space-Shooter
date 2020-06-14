using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingModifier : Modification
{
	private int additionalHits = 2;

	Projectile target;

	public override void SetModificationTarget(ModificationObject modificationTarget)
	{
		base.SetModificationTarget(modificationTarget);

		if (m_modificationTarget is Projectile)
		{
			target = (Projectile)m_modificationTarget;
			target.onStartCallback += AddHits;
		}
	}

	private void AddHits() {
		if (m_modificationTarget is Projectile)
		{
			target.hitAmount += additionalHits;
			Debug.Log("addotional hits");
		}
	}
}
