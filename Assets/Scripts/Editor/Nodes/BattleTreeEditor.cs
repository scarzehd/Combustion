using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Combustion.Battle.Nodes;

namespace Combustion.Editor.Nodes
{
	[CustomEditor(typeof(BattleTree))]
	public class BattleTreeEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			if (GUILayout.Button("Choose Pattern"))
			{
				Debug.Log(((BattleTree)target).ChoosePattern().name);
			}
		}
	}
}
