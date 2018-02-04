using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[SerializeField] Object toSpawn = null;
	[SerializeField] uint minPoolSize = 0;
	[SerializeField] uint maxpoolSize = 1;

	List<Poolable> pool = new List<Poolable>();

	public Poolable GetInstance(Vector3 pos = new Vector3(), Quaternion rot = new Quaternion())
	{
		Poolable ret = null;
		foreach (var p in pool)
		{
			if (!p.inUse)
			{
				ret = p;
			}
		}
		if (ret == null)
		{
			ret = CreateInstance();
		}

		ret.gameObject.transform.position = pos;
		ret.gameObject.transform.rotation = rot;

		ret.inUse = true;
		ret.gameObject.SetActive(true);
		return ret;
	}

	public void ReleaseInstance(Poolable poolable)
	{
		if (pool.Count > maxpoolSize)
		{
			DestroyInstance(poolable);
		}
		poolable.inUse = false;
		poolable.gameObject.SetActive(false);
	}

	Poolable CreateInstance()
	{
		var gobj = (GameObject)Instantiate(toSpawn);
		gobj.SetActive(false);
		var poolable = gobj.GetComponent<Poolable>();
		Debug.Assert(poolable);
		poolable.pool = this;
		pool.Add(poolable);
		return poolable;
	}

	void DestroyInstance(Poolable poolable)
	{
		pool.Remove(poolable);
		Destroy(poolable.gameObject);
	}

	void Start()
	{
		Debug.Assert(toSpawn != null);

		for (int i = 0; i < minPoolSize; i++)
		{
			CreateInstance();
		}
	}
}
