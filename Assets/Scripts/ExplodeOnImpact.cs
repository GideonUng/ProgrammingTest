﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExplodeOnImpact : MonoBehaviour
{
	[SerializeField] string explosionPoolName = "";

	ObjectPool pool;

	void Start()
	{
		pool = GameObject.FindGameObjectWithTag(explosionPoolName).GetComponent<ObjectPool>();
		Debug.Assert(pool != null);
	}

	void OnCollisionEnter(Collision collision)
	{
		pool.GetInstance(
			(GameObject ob) =>
			{
				ob.transform.position = collision.contacts[0].point;
			}
		);
	}
}
