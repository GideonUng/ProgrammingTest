using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
	[SerializeField] Object explosion = null;

	private void OnCollisionEnter(Collision collision)
	{
		var explosionInstance = (GameObject)Instantiate(explosion);
		explosionInstance.transform.position = collision.contacts[0].point;
	}
}
