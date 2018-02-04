using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	[SerializeField] float secsTillDestroy = 1;
	[SerializeField] float expandSpeed = 1;

	void OnEnable()
	{
		StartCoroutine(DeferredDestroy());
		transform.localScale = new Vector3(0, 0, 0);
	}

	void Update()
	{
		float newSale = transform.localScale.x + Time.deltaTime * expandSpeed;
		transform.localScale = new Vector3(newSale, newSale, newSale);
	}

	IEnumerator DeferredDestroy()
	{
		yield return new WaitForSeconds(secsTillDestroy);
		gameObject.SetActive(false);
	}
}
