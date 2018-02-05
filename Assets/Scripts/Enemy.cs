using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(GameobjectBoundingSphere))]
public class Enemy : MonoBehaviour
{
	[SerializeField] float minRespawnTime = 2;
	[SerializeField] float maxRespawnTime = 3;
	[SerializeField] uint randomPointTries = 4;
	[SerializeField] float playAreaWidth = 1;
	[SerializeField] float playAreaHeight = 1;

	[SerializeField] List<Transform> dynamicObjectsToAvoid = new List<Transform>();

	public bool Destroyed { get; private set; }

	Transform body;
	GameobjectBoundingSphere bs;

	bool GetRandomPosition(out Vector3 result)
	{
		result = new Vector3();

		Vector3 randomPoint = new Vector3(
			Random.Range(-playAreaWidth / 2, playAreaWidth / 2),
			transform.position.y,
			Random.Range(-playAreaHeight / 2, playAreaHeight / 2)
			);

		NavMeshHit hit;
		if (!NavMesh.SamplePosition(randomPoint, out hit, 0.5f, NavMesh.AllAreas)) { return false; }
		randomPoint = hit.position;

		foreach (var other in dynamicObjectsToAvoid)
		{
			var otherBs = other.GetComponent<GameobjectBoundingSphere>();
			if (otherBs == null)
			{
				otherBs = other.gameObject.AddComponent<GameobjectBoundingSphere>();
			}
			if ((randomPoint - otherBs.Bounds.position).magnitude < bs.Bounds.radius + otherBs.Bounds.radius && otherBs.Bounds.radius > 0)
			{
				return false;
			}
		}

		result = randomPoint;
		return true;
	}

	void MoveToRandomPosition()
	{
		Vector3 pos = new Vector3();
		for (int i = 0; i < randomPointTries; i++)
		{
			if (GetRandomPosition(out pos))
			{
				transform.position = pos;
				return;
			}
		}
		// just use same position if we exhaust position retries
		Debug.Log("No valid position found");
	}

	void Start()
	{
		body = transform.GetChild(0);
		bs = GetComponent<GameobjectBoundingSphere>();

		foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			if (enemy != gameObject)
			{
				dynamicObjectsToAvoid.Add(enemy.transform);
			}
		}

		MoveToRandomPosition();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player") { return; }
		StartCoroutine(ReEnable());
		MoveToRandomPosition();
		Destroyed = true;
		body.gameObject.SetActive(!Destroyed);
	}

	IEnumerator ReEnable()
	{
		yield return new WaitForSeconds(Random.Range(minRespawnTime, maxRespawnTime));
		Destroyed = false;
		body.gameObject.SetActive(!Destroyed);
	}
}
