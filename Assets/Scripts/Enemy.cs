using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GameobjectBoundingSphere))]
public class Enemy : MonoBehaviour
{
	[SerializeField]
	private float minRespawnTime = 2;
	[SerializeField]
	private float maxRespawnTime = 3;
	[SerializeField]
	private uint randomPointTries = 4;
	[SerializeField]
	private float playAreaWidth = 1;
	[SerializeField]
	private float playAreaHeight = 1;
	[SerializeField]
	private List<Transform> dynamicObjectsToAvoid = new List<Transform>();

	public bool Destroyed { get; private set; }

	private Transform body;
	private GameobjectBoundingSphere bs;

	private bool GetRandomPosition(out Vector3 result)
	{
		result = new Vector3();

		var randomPoint = new Vector3(
			Random.Range(-playAreaWidth / 2, playAreaWidth / 2),
			transform.position.y,
			Random.Range(-playAreaHeight / 2, playAreaHeight / 2)
		);

		if (!NavMesh.SamplePosition(randomPoint, out var hit, 0.5f, NavMesh.AllAreas))
		{
			return false;
		}

		randomPoint = hit.position;

		foreach (var other in dynamicObjectsToAvoid)
		{
			var otherBs = other.GetComponent<GameobjectBoundingSphere>();
			if (otherBs == null)
			{
				otherBs = other.gameObject.AddComponent<GameobjectBoundingSphere>();
			}

			if ((randomPoint - otherBs.Bounds.position).magnitude < bs.Bounds.radius + otherBs.Bounds.radius &&
			    otherBs.Bounds.radius > 0)
			{
				return false;
			}
		}

		result = randomPoint;
		return true;
	}

	private void MoveToRandomPosition()
	{
		for (var i = 0; i < randomPointTries; i++)
		{
			if (!GetRandomPosition(out var pos))
				continue;
			transform.position = pos;
			return;
		}

		// just use same position if we exhaust position retries
		Debug.Log("Enemy failed to find valid spawn position. Consider increasing retry count");
	}

	private void Awake()
	{
		body = transform.GetChild(0);
		bs = GetComponent<GameobjectBoundingSphere>();
	}

	private void Start()
	{
		// subtract enemy size from play area. Not really necessary but decreases chance of lumping at the play area edges
		playAreaWidth -= bs.Bounds.radius;
		playAreaHeight -= bs.Bounds.radius;

		foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			if (enemy == gameObject)
			{
				continue;
			}

			dynamicObjectsToAvoid.Add(enemy.transform);
		}

		MoveToRandomPosition();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!collision.gameObject.CompareTag("Projectile"))
		{
			return;
		}

		Destroyed = true;
		body.gameObject.SetActive(!Destroyed);
		MoveToRandomPosition();
		StartCoroutine(ReEnable());
	}

	private IEnumerator ReEnable()
	{
		yield return new WaitForSeconds(Random.Range(minRespawnTime, maxRespawnTime));
		Destroyed = false;
		body.gameObject.SetActive(!Destroyed);
	}
}