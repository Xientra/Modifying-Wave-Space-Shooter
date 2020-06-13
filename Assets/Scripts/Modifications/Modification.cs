using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modification<T> : MonoBehaviour
{
	public bool isRemoveable;

	public T target;

	public virtual T GetTarget()
	{
		return target;
	}

	public virtual void SetTarget(T target)
	{
		this.target = target;
	}


	public abstract void Apply();
}