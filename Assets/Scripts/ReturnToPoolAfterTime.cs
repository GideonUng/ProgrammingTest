using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Poolable))]
public class ReturnToPoolAfterTime : MonoBehaviour
{
	[SerializeField] float time = 1;

	Coroutine coroutine = null;

	void OnEnable()
	{
		coroutine =  StartCoroutine(DefferedDestroy());
	}

	void OnDisable()
	{
		StopCoroutine(coroutine);
	}

	IEnumerator DefferedDestroy()
	{
		yield return new WaitForSeconds(time);
		GetComponent<Poolable>().Release();
	}
}
