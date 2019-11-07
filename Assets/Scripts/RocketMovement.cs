using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RocketMovement : ProjectileMovement
{
	[SerializeField]
	private float rotationSpeed = 1;
	private Transform currentEnemy;

	private void FindEnemy()
	{
		currentEnemy = null;
		var shortestDist = Mathf.Infinity;
		foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			var distToEnemy = (transform.position - enemy.transform.position).magnitude;
			if (!enemy.transform.GetChild(0).gameObject.activeInHierarchy || !(shortestDist > distToEnemy))
				continue;

			shortestDist = distToEnemy;
			currentEnemy = enemy.transform;
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		FindEnemy();
	}

	private void Update()
	{
		if (currentEnemy == null || !currentEnemy.gameObject.activeInHierarchy ||
		    currentEnemy.GetComponent<Enemy>().Destroyed)
		{
			FindEnemy();
		}

		Quaternion targetRotation;
		if (currentEnemy != null)
		{
			var targetPosition = new Vector3(currentEnemy.position.x, transform.position.y, currentEnemy.position.z);
			targetRotation = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);
		}
		else
		{
			// when in idle rotate to the right
			targetRotation = Quaternion.LookRotation(transform.right, Vector3.up);
		}
		transform.rotation =
			Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		UpdateVelocity();
	}
}