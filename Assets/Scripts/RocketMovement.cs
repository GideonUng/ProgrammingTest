using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RocketMovement : ProjectileMovement
{
	[SerializeField] float rotationSpeed = 1;
	Transform currentEnemy = null;

	void FindEnemy()
	{
		currentEnemy = null;
		float shortestDist = Mathf.Infinity;
		foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			float distToEnemy = (transform.position - enemy.transform.position).magnitude;
			if (enemy.transform.GetChild(0).gameObject.activeInHierarchy && shortestDist > distToEnemy)
			{
				shortestDist = distToEnemy;
				currentEnemy = enemy.transform;
			}
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		FindEnemy();
	}

	void Update()
	{
		if(currentEnemy == null || !currentEnemy.GetChild(0).gameObject.activeInHierarchy)
		{
			FindEnemy();
		}

		Quaternion targetRotation;
		if (currentEnemy != null)
		{
			Vector3 targetPosition = new Vector3(currentEnemy.position.x, transform.position.y, currentEnemy.position.z);
			targetRotation = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);
		}
		else
		{
			// when in idle rotate to the right
			targetRotation = Quaternion.LookRotation(transform.right, Vector3.up);
		}
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		UpdateVelocity();
	}
}
