using UnityEditor.EditorTools;
using UnityEditor;
using UnityEngine;

using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;

namespace Combustion.Editor
{

    [EditorTool("Animation Tools")]
    public class AnimationTools : EditorTool
    {
		public float circleRadius;

		public Vector3 position;

		public Quaternion rotation;

		public override void OnActivated() {
			rotation = Quaternion.identity;

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

			EditorGUI.BeginChangeCheck();

			Handles.TransformHandle(ref position, ref rotation, ref circleRadius);

			if (EditorGUI.EndChangeCheck())
			{
				for (int i = 0; i < gameObjects.Count; i++)
				{
					Vector2 pos = new Vector2(
						position.x + circleRadius * Mathf.Cos((i * 2 * Mathf.PI / gameObjects.Count) + rotation.eulerAngles.z * Mathf.Deg2Rad),
						position.y + circleRadius * Mathf.Sin((i * 2 * Mathf.PI / gameObjects.Count) + rotation.eulerAngles.z * Mathf.Deg2Rad)
					);

					Undo.RecordObject(gameObjects[i].transform, "Circle");

					if (gameObjects[i].IsPrefabInstance())
						PrefabUtility.RecordPrefabInstancePropertyModifications(gameObjects[i].transform);

					gameObjects[i].transform.position = pos;

					EditorUtility.SetDirty(gameObjects[i].transform);
				}	
			}
		}
	}
}
