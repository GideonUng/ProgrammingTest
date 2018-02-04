using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Poolable))]
public class ReturnToPoolOnImpact : MonoBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
		GetComponent<Poolable>().Release();
	}
}
