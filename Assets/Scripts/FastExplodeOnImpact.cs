using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastExplodeOnImpact : ExplodeOnImpact
{
	Vector3 lastPos;

	private void Start()
	{
		lastPos = transform.position;
	}

	void Update()
	{
		lastPos = transform.position;
	}

	protected override void OnCollisionEnter(Collision collision)
	{
		base.OnCollisionEnter(collision);
	}
}
