using UnityEngine;

[RequireComponent(typeof(Poolable))]
public class ReturnToPoolOnImpact : MonoBehaviour
{
	private Poolable poolable;

	private void Awake()
	{
		poolable = GetComponent<Poolable>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		poolable.Release();
	}
}