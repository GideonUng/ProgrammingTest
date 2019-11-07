using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	private float speed = 1;
	[SerializeField]
	private float rotationSpeed = 1;

	private Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		var vertical = Input.GetAxis("Vertical");
		rb.velocity = transform.forward * (vertical * speed);
		rb.angularVelocity = new Vector3(0, Input.GetAxis("Horizontal") * rotationSpeed, 0) * (vertical >= 0 ? 1 : -1);
	}
}