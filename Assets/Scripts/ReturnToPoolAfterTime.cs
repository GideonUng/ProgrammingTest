using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Poolable))]
public class ReturnToPoolAfterTime : MonoBehaviour
{
	[SerializeField]
	private float time = 1;

	private Coroutine coroutine;

	private void OnEnable() // should be instantiate callback
	{
		coroutine = StartCoroutine(DefferedDestroy());
	}

	private void OnDisable() // should be release callback
	{
		StopCoroutine(coroutine);
	}

	private IEnumerator DefferedDestroy()
	{
		yield return new WaitForSeconds(time);
		GetComponent<Poolable>().Release();
	}
}