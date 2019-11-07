using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	[SerializeField]
	private float secsTillDestroy = 1;
	[SerializeField]
	private float expandSpeed = 1;

	private void OnEnable()
	{
		StartCoroutine(DeferredDestroy());
		transform.localScale = new Vector3(0, 0, 0);
	}

	private void Update()
	{
		var newSale = transform.localScale.x + Time.deltaTime * expandSpeed;
		transform.localScale = new Vector3(newSale, newSale, newSale);
	}

	private IEnumerator DeferredDestroy()
	{
		yield return new WaitForSeconds(secsTillDestroy);
		Destroy(gameObject);
	}
}