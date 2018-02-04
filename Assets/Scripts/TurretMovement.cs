using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
	[SerializeField] float rotationSpeed = 1;

	Camera mainCamera;
	LayerMask mouseRaycastLayer;

	Quaternion targetRotation;

	void Awake()
	{
		mouseRaycastLayer = LayerMask.NameToLayer("Mouse");
	}

	void Start()
	{
		mainCamera = Camera.main;
	}

	void Update()
	{
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}

	void FixedUpdate()
	{
		var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (!Physics.Raycast(ray, out hit, 20f, 1 << mouseRaycastLayer.value)) { return; }

		targetRotation = Quaternion.LookRotation(hit.point - transform.position, Vector3.up);
		// make sure rotation is only around y when mouse is very close to turret center
		targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
	}
}
