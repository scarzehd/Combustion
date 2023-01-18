using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Combustion.BattleNodes
{
	using Utilities;

	public class BattleNodeEditor : EditorWindow
	{
		private readonly string defaultFilename = "fileName";

		private Button saveButton;

		[MenuItem("Window/Combustion/Battle Node Editor")]
		public static void Open() {
			GetWindow<BattleNodeEditor>("Battle Node Editor");
		}

		private void CreateGUI() {
			AddGraphView();

			AddToolbar();
		}

		private void AddToolbar() {
			Toolbar toolbar = new Toolbar();

			TextField fileNameTextField = BattleElementUtilities.CreateTextField(defaultFilename, "File Name:");

			saveButton = BattleElementUtilities.CreateButton("Save");

			toolbar.Add(fileNameTextField);
			toolbar.Add(saveButton);

			rootVisualElement.Add(toolbar);
		}

		private void AddGraphView() {
			BattleNodeGraphView graphView = new BattleNodeGraphView(this);
			graphView.StretchToParentSize();
			rootVisualElement.Add(graphView);
		}

		public void EnableSaving() {
			saveButton.SetEnabled(true);
		}

		public void DisableSaving() {
			saveButton.SetEnabled(false);
		}
	}
}