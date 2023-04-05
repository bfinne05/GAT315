using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastQuery : MonoBehaviour
{
	public enum CastType
	{
		RAY,
		SPHERE
	}

	[SerializeField] CastType castType;
	[SerializeField] LayerMask layerMask;
	[SerializeField] KeyCode castKey;
	[SerializeField, Range(0.1f, 2)] float radius = 1;
	[SerializeField, Range(0.5f, 10)] float distance = 1;

	RaycastHit[] raycastHits;
		
	void Update()
	{
		if (Input.GetKeyDown(castKey))
		{
			Query();
			StartCoroutine(ClearColliders());
		}
	}

	void Query()
	{
		Ray ray = new Ray(transform.position, transform.forward);

		switch (castType)
		{
			case CastType.RAY:
				raycastHits = Physics.RaycastAll(ray, distance, layerMask);
				break;
			case CastType.SPHERE:
				raycastHits = Physics.SphereCastAll(ray, distance, layerMask);
				break;
			default:
				break;
		}
	}

	IEnumerator ClearColliders()
	{
		yield return new WaitForSeconds(1);
		raycastHits = null;
	}

	private void OnDrawGizmos()
	{
		// show hits if not playing
		if (!Application.isPlaying)
		{
			Query();
		}

		// draw cast
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, 0.2f);
		switch (castType)
		{
			case CastType.RAY:
				Gizmos.DrawRay(transform.position, transform.forward * distance);
				break;
			case CastType.SPHERE:
				int count = Mathf.CeilToInt(distance / radius);
				float segment = distance / count;
				for (int i = 0; i <= count; i++)
				{
					Gizmos.DrawWireSphere(transform.position + transform.forward * i * segment, radius);
				}
				break;
			default:
				break;
		}

		// draw hits
		if (raycastHits != null)
		{
			Gizmos.color = Color.red;
			foreach (var raycastHit in raycastHits)
			{
				Gizmos.DrawWireSphere(raycastHit.transform.position, 1);
			}
		}
	}
}
