using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Combustion.Editor.BattleNodes
{
	public class BattleNodeGraphView : GraphView
	{
		public BattleNodeGraphView() {
			AddGridBackground();
			AddStyles();
		}

		private void AddGridBackground() {
			GridBackground gridBackground = new GridBackground();
			gridBackground.StretchToParentSize();

			Insert(0, gridBackground);
		}
		private void AddStyles() {
			StyleSheet styleSheet = EditorGUIUtility.Load("BattleNodes/BattleNodeGraphViewStyles.uss") as StyleSheet;
		}
	}
}