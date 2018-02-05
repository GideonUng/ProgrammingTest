using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileMovement : MonoBehaviour
{
	[SerializeField] protected float movementSpeed = 1;

	protected Rigidbody rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	protected virtual void OnEnable()
	{
		UpdateVelocity();
	}

	public void UpdateVelocity()
	{
		rb.velocity = transform.forward * movementSpeed;
		rb.angularVelocity = new Vector3();
	}
}
