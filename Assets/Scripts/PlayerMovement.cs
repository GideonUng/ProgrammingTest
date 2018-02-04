using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float speed = 1;
	[SerializeField] float rotationSpeed = 1;

	Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		rb.velocity = transform.forward * Input.GetAxis("Vertical") * speed;
		rb.angularVelocity = new Vector3(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);
		rb.angularVelocity *= Input.GetAxis("Vertical") >= 0 ? 1 : -1;
	}
}
