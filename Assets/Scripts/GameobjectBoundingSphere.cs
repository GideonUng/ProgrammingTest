using System.Collections.Generic;
using UnityEngine;

public class GameobjectBoundingSphere : MonoBehaviour
{
	public BoundingSphere Bounds
	{
		get
		{
			if (dirty)
			{
				RecalculateBoundingSphere();
			}
			return new BoundingSphere(transform.position + offset, radius);
		}
	}

	private Vector3 offset;
	private float radius;
	private bool dirty = true;

	public void SetDirty()
	{
		dirty = true;
	}

	private void RecalculateBoundingSphere()
	{
		var colliders = new List<Collider>(GetComponents<Collider>());
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
		dirty = false;
	}
}