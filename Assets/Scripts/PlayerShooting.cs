using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	[SerializeField]
	private string shootAction = "Fire1";
	[SerializeField]
	private Transform shootPos;
	[SerializeField]
	private ObjectPool pool;

	private void Update()
	{
		if (Input.GetButtonUp(shootAction))
		{
			pool.GetInstance(
				go =>
				{
					go.transform.position = shootPos.position;
					go.transform.rotation = shootPos.rotation;
				}
			);
		}
	}
}