using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Poolable))]
public class ReturnToPoolOnImpact : MonoBehaviour
{
	Poolable poolable;

	void Awake()
	{
		poolable = GetComponent<Poolable>();
	}

	void OnCollisionEnter(Collision collision)
	{
		poolable.Release();
	}
}
