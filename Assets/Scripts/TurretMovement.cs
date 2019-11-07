using UnityEngine;

public class TurretMovement : MonoBehaviour
{
	[SerializeField]
	private float rotationSpeed = 1;

	private Camera mainCamera;
	private LayerMask mouseRaycastLayer;

	private Quaternion targetRotation;

	private void Awake()
	{
		mouseRaycastLayer = LayerMask.NameToLayer("Mouse");
		mainCamera = Camera.main;
	}

	private void Update()
	{
		transform.rotation = Quaternion.RotateTowards(
			transform.rotation,
			targetRotation,
			rotationSpeed * Time.deltaTime
		);
	}

	private void FixedUpdate()
	{
		var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		if (!Physics.Raycast(ray, out var hit, 20f, 1 << mouseRaycastLayer.value))
		{
			return;
		}

		targetRotation = Quaternion.LookRotation(hit.point - transform.position, Vector3.up);
		// make sure rotation is only around y when mouse is very close to turret center
		targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
	}
}