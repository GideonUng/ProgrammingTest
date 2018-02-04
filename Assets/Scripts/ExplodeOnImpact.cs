using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
	[SerializeField] Object explosion = null;

	GameObject explosionInstance;

	void Awake()
	{
		explosionInstance = (GameObject)Instantiate(explosion);
		explosionInstance.SetActive(false);
	}

	private void OnCollisionEnter(Collision collision)
	{
		explosionInstance.SetActive(true);
		explosionInstance.transform.position = collision.contacts[0].point;
	}
}
