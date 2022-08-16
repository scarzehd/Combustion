using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace Combustion.Editor.BattleNodes
{
	public class BattleNodeEditor : EditorWindow
	{
		[MenuItem("Window/Combustion/Battle Node Editor")]
		public static void Open() {
			GetWindow<BattleNodeEditor>("Battle Node Editor");
		}

		private void CreateGUI() {
			AddGraphView();
		}

		private void AddGraphView() {
			BattleNodeGraphView graphView = new BattleNodeGraphView();
			graphView.StretchToParentSize();
			rootVisualElement.Add(graphView);
		}
	}
}