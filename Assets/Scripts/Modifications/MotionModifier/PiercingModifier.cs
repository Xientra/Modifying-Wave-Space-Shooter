using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingModifier : Modification
{
	private int additionalHits = 2;

	private bool applied = false;

	public override void ApplyModification()
	{
		if (applied == false)
		{
			if (m_modificationTarget is Projectile)
			{
				((Projectile)m_modificationTarget).hitAmount += additionalHits;
			}
			applied = true;
		}
	}
}
