using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpeedUpMod : ProjectileModification
{
	public float multiplier = 2;

	public override void Apply()
	{
		target.onStartCallback += IncreaseSpeed;
	}

	private void IncreaseSpeed() {
		target.speed *= multiplier;
	}
}
