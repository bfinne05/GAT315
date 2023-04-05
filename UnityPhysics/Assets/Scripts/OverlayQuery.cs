using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayQuery : MonoBehaviour
{
	public enum QueryType
	{
		BOX,
		SPHERE
	}

	[SerializeField] QueryType queryType = QueryType.SPHERE;
	[SerializeField] LayerMask layerMask;
	[SerializeField] KeyCode queryKey;
	[SerializeField, Range(0.5f, 10)] float size = 1;

	Collider[] colliders;

	void Update()
	{
		if (Input.GetKeyDown(queryKey))
		{
			Query();			
			StartCoroutine(ClearColliders());
		}
	}

	void Query()
	{
		switch (queryType)
		{
			case QueryType.BOX:
				colliders = Physics.OverlapBox(transform.position, Vector3.one * size * 0.5f, transform.rotation, layerMask);
				break;
			case QueryType.SPHERE:
				colliders = Physics.OverlapSphere(transform.position, size * 0.5f, layerMask);
				break;
			default:
				break;
		}
	}

	IEnumerator ClearColliders()
	{
		yield return new WaitForSeconds(1);
		colliders = null;
	}

	private void OnDrawGizmos()
	{
		// show colliders if not playing
		if (!Application.isPlaying)
		{
			Query();
		}

		// draw overlay
		Gizmos.color = Color.green;
		switch (queryType)
		{
			case QueryType.BOX:
				Gizmos.DrawWireCube(transform.position, Vector3.one * size);
				break;
			case QueryType.SPHERE:
				Gizmos.DrawWireSphere(transform.position, size * 0.5f);
				break;
			default:
				break;
		}

		// draw colliders
		if (colliders != null)
		{
			Gizmos.color = Color.red;
			foreach (var collider in colliders)
			{
				Gizmos.DrawWireSphere(collider.transform.position, 1);
			}
		}
	}
}
