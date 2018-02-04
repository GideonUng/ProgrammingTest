using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	[SerializeField] string shootAction = "Fire1";
	[SerializeField] Transform shootPos = null;
	[SerializeField] ObjectPool pool = null;

	void Update()
	{
		if (Input.GetButtonUp(shootAction))
		{
			pool.GetInstance(shootPos.position, shootPos.rotation);
		}
	}
}
