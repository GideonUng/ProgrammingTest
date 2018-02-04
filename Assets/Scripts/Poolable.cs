using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour
{
	[NonSerialized] public bool inUse = false;
	[NonSerialized] public ObjectPool pool = null;

	public void Release()
	{
		pool.ReleaseInstance(this);
	}
}
