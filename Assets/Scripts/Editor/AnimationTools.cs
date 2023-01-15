using UnityEditor.EditorTools;
using UnityEditor;
using UnityEngine;

using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Combustion.Editor
{

    [EditorTool("Animation Tools")]
    public class AnimationTools : EditorTool
    {
		public float circleRadius;

		public Vector3 position;

		public override void OnActivated() {
			circleRadius = 1f;

			GameObject[] gameObjects = Selection.GetFiltered<GameObject>(SelectionMode.TopLevel);

			position = Vector3.zero;

			foreach (GameObject gameObject in gameObjects)
			{
				position += gameObject.transform.position;
			}

			position /= gameObjects.Length;
		}

		public override void OnToolGUI(EditorWindow window) {
			if (!(window is SceneView sceneView))
				return;

			List<GameObject> gameObjects = new List<GameObject>();
			
			foreach (Object obj in targets)
			{
				if (obj is GameObject go)
					gameObjects.Add(go);
			}

			circleRadius = Handles.RadiusHandle(Quaternion.identity, position, circleRadius);
			position = Handles.PositionHandle(position, Quaternion.identity);

			for (int i = 0; i < gameObjects.Count; i++)
			{
				Vector2 pos = new Vector2(
					position.x + circleRadius * Mathf.Cos(i * 2 * Mathf.PI / gameObjects.Count),
					position.y + circleRadius * Mathf.Sin(i * 2 * Mathf.PI / gameObjects.Count)
				);

				gameObjects[i].transform.position = pos;
			}
		}
	}
}
