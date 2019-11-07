using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool : MonoBehaviour
{
	[SerializeField]
	private Object toSpawn;
	[SerializeField]
	private uint minPoolSize;
	[SerializeField]
	private uint maxpoolSize = 1;

	private readonly List<Poolable> pool = new List<Poolable>();

	public Poolable GetInstance(Action<GameObject> initDel)
	{
		Poolable ret = null;
		foreach (var p in pool.Where(p => !p.InUse))
		{
			ret = p;
		}
		if (ret == null)
		{
			ret = CreateInstance();
		}

		initDel?.Invoke(ret.gameObject);

		ret.InUse = true;
		ret.gameObject.SetActive(true);
		return ret;
	}

	public void ReleaseInstance(Poolable poolable)
	{
		if (pool.Count > maxpoolSize)
		{
			DestroyInstance(poolable);
		}
		poolable.InUse = false;
		poolable.gameObject.SetActive(false);
	}

	private Poolable CreateInstance()
	{
		var go = (GameObject) Instantiate(toSpawn);
		go.SetActive(false);
		var poolable = go.GetComponent<Poolable>();
		Debug.Assert(poolable);
		poolable.Pool = this;
		pool.Add(poolable);
		return poolable;
	}

	private void DestroyInstance(Poolable poolable)
	{
		pool.Remove(poolable);
		Destroy(poolable.gameObject);
	}

	private void Start()
	{
		Debug.Assert(toSpawn != null);
		for (var i = 0; i < minPoolSize; i++)
		{
			CreateInstance();
		}
	}
}