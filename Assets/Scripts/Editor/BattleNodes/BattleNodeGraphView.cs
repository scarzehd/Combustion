using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

using Combustion.Editor.BattleNodes.Elements;

namespace Combustion.Editor.BattleNodes
{
	public class BattleNodeGraphView : GraphView
	{
		public BattleNodeGraphView() {
			AddGridBackground();
			AddStyles();
			AddManipulators();
		}

		public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
			List<Port> compatiblePorts = new List<Port>();

			foreach (Port port in ports)
			{
				if (startPort != port && startPort.node != port.node && startPort.direction != port.direction)
				{
					compatiblePorts.Add(port);
				}
			}

			return compatiblePorts;
		}

		private BattleNode CreateNode(Type type, Vector2 position) {
			BattleNode node = Activator.CreateInstance(type) as BattleNode;

			node.Initialize(position);
			node.Draw();

			AddElement(node);

			return node;
		}

		private Group CreateGroup(string title, Vector2 position) {
			Group group = new Group()
			{
				title = title
			};

			group.SetPosition(new Rect(position, Vector2.zero));

			return group;
		}

		private void AddManipulators() {
			this.AddManipulator(new ContentDragger());
			this.AddManipulator(new ContentZoomer());
			this.AddManipulator(new SelectionDragger());
			this.AddManipulator(new RectangleSelector());
		}

		public override void BuildContextualMenu(ContextualMenuPopulateEvent evt) {
			base.BuildContextualMenu(evt);

			evt.menu.AppendAction("Add Node", actionEvent => AddElement(CreateNode(typeof(BattleNode), actionEvent.eventInfo.mousePosition)));
			evt.menu.AppendAction("Add Pattern Node", actionEvent => AddElement(CreateNode(typeof(PatternNode), actionEvent.eventInfo.mousePosition)));
			evt.menu.AppendAction("Add Composite Node", actionEvent => AddElement(CreateNode(typeof(CompositeNode), actionEvent.eventInfo.mousePosition)));

			evt.menu.AppendAction("Add Group", actionEvent => AddElement(CreateGroup("Node Group", actionEvent.eventInfo.mousePosition)));
		}

		private void AddGridBackground() {
			GridBackground gridBackground = new GridBackground();
			gridBackground.StretchToParentSize();

			Insert(0, gridBackground);
		}
		private void AddStyles() {
			StyleSheet graphViewStyleSheet = (StyleSheet) EditorGUIUtility.Load("BattleNodes/BattleNodeGraphViewStyles.uss");
			StyleSheet nodeStyleSheet = (StyleSheet)EditorGUIUtility.Load("BattleNodes/BattleNodeStyles.uss");

			styleSheets.Add(graphViewStyleSheet);
			styleSheets.Add(nodeStyleSheet);
		}
	}
}