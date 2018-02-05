using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExplodeOnImpact : MonoBehaviour
{
	[SerializeField] string explosionPoolTag = "";

	ObjectPool pool;

	void Start()
	{
		pool = GameObject.FindGameObjectWithTag(explosionPoolTag).GetComponent<ObjectPool>();
		Debug.Assert(pool != null);
	}

	void OnCollisionEnter(Collision collision)
	{
		Explode(collision.contacts[0].point);
	}

	void Explode(Vector3 point)
	{
		pool.GetInstance(
			(GameObject go) =>
			{
				go.transform.position = point;
			}
		);
	}
}
