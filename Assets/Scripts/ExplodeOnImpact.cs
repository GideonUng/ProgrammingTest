using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExplodeOnImpact : MonoBehaviour
{
	[SerializeField]
	private string explosionPoolTag = "";

	private ObjectPool pool;

	private void Start()
	{
		pool = GameObject.FindGameObjectWithTag(explosionPoolTag).GetComponent<ObjectPool>();
		Debug.Assert(pool != null);
	}

	private void OnCollisionEnter(Collision collision)
	{
		Explode(collision.contacts[0].point);
	}

	private void Explode(Vector3 point)
	{
		pool.GetInstance(
			go => { go.transform.position = point; }
		);
	}
}