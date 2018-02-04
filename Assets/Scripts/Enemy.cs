using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
	[SerializeField] float minRespawnTime = 2;
	[SerializeField] float maxRespawnTime = 3;
	[SerializeField] uint randomPointTries = 4;
	[SerializeField] float range = 1;

	Transform body;

	bool GetRandomPosition(out Vector3 result)
	{
		Vector2 randomPoint = Random.insideUnitCircle.normalized * Random.value * range;
		NavMeshHit hit;
		if (NavMesh.SamplePosition(new Vector3(randomPoint.x, 0, randomPoint.y), out hit, 5, NavMesh.AllAreas))
		{
			result = hit.position;
			return true;
		}
		result = Vector3.zero;
		return false;
	}

	void MoveToRandomPosition()
	{
		Vector3 pos = new Vector3();
		for (int i = 0; i < randomPointTries; i++)
		{
			if (GetRandomPosition(out pos))
			{
				transform.position = pos;
				break;
			}
		}
		// just use same position if we exhaust position retries
	}

	void Start()
	{
		body = transform.GetChild(0);
		MoveToRandomPosition();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player") { return; }
		body.gameObject.SetActive(false);
		StartCoroutine(ReEnable());
	}

	IEnumerator ReEnable()
	{
		MoveToRandomPosition();
		yield return new WaitForSeconds(Random.Range(minRespawnTime, maxRespawnTime));
		body.gameObject.SetActive(true);
	}
}
