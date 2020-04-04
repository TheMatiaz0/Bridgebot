using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class VectorExtension
{
	public static T GetClosestObject<T>(this Vector2 currentPosition, Vector2 targetPosition, IEnumerable<T> list, float range) where T : class
	{
		T bestTarget = null;
		foreach (T target in list)
		{
			if (target == null)
			{
				continue;
			}

			Vector2 directionToTarget = targetPosition - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if (dSqrToTarget < range)
			{
				range = dSqrToTarget;
				bestTarget = target;
			}
		}

		return bestTarget;
	}
}
