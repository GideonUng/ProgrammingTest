using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectBoundingSphere : MonoBehaviour
{
	public BoundingSphere Bounds
	{
		get
		{
			if (!initialized)
			{
				RecalculateBoundingSphere();
			}
			return new BoundingSphere(transform.position + offset, radius);
		}
	}
	Vector3 offset = new Vector3();
	float radius = 0;
	bool initialized = false;

	public void RecalculateBoundingSphere()
	{
		List<Collider> colliders = new List<Collider>(GetComponents<Collider>());
		colliders.AddRange(GetComponentsInChildren<Collider>());

		Bounds? bounds = null;
		foreach (var collider in colliders)
		{
			if (bounds == null)
			{
				bounds = collider.bounds;
			}
			else
			{
				bounds.Value.Encapsulate(collider.bounds);
			}
		}

		if (bounds == null)
		{
			offset = new Vector3();
			radius = 0;
		}
		else
		{
			offset = bounds.Value.center - transform.position;
			radius = bounds.Value.extents.magnitude;
		}
		initialized = true;
	}

	void Start()
	{
		if (!initialized)
		{
			RecalculateBoundingSphere();
		}
	}
}
