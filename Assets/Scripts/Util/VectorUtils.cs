using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Util
{
    public static class VectorUtils
    {
		public static bool IsCloseEnough(Vector2 start, Vector2 desired, float snap) {
			return Vector2.Distance(start, desired) < snap;
		}

		public static Vector3 SmoothStep(Vector3 start, Vector3 desired, float time) {
			float x, y, z;

			x = Mathf.SmoothStep(start.x, desired.x, time);
			y = Mathf.SmoothStep(start.y, desired.y, time);
			z = Mathf.SmoothStep(start.z, desired.z, time);

			return new Vector3(x, y, z);
		}

		public static Vector2 SmoothStep(Vector2 start, Vector2 desired, float time) {
			float x, y;

			x = Mathf.SmoothStep(start.x, desired.x, time);
			y = Mathf.SmoothStep(start.y, desired.y, time);

			return new Vector2(x, y);
		}
	}
}
