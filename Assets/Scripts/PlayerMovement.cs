using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float speed = 1;
	[SerializeField] float rotationSpeed = 1;

	Rigidbody rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		float vertical = Input.GetAxis("Vertical");
		rb.velocity = transform.forward * vertical * speed;
		rb.angularVelocity = new Vector3(0, Input.GetAxis("Horizontal") * rotationSpeed, 0) * (vertical >= 0 ? 1 : -1);
	}
}
