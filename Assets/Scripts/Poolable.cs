using System;
using UnityEngine;

public class Poolable : MonoBehaviour
{
	[NonSerialized]
	public bool InUse = false;
	[NonSerialized]
	public ObjectPool Pool = null;

	public void Release()
	{
		Pool.ReleaseInstance(this);
	}
}