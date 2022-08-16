using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

using Combustion.Editor.BattleNodes.Elements;
using Combustion.Editor.BattleNodes.Utilities;

namespace Combustion.Editor.BattleNodes
{
	public class BattleNodeGraphView : GraphView
	{
		private BattleNodeSearchWindow searchWindow;
		public BattleNodeGraphView() {
			AddGridBackground();
			AddStyles();
			AddManipulators();

			AddSearchWindow();
		}

		private void AddSearchWindow() {
			if (searchWindow == null)
			{
				searchWindow = ScriptableObject.CreateInstance<BattleNodeSearchWindow>();
				searchWindow.Initialize(this);
			}

			nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
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

		public BattleNode CreateNode(Type type, Vector2 position) {
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

			evt.menu.AppendAction("Add Node", actionEvent => AddElement(CreateNode(typeof(BattleNode), GetLocalMousePosition(actionEvent.eventInfo.mousePosition))));
			evt.menu.AppendAction("Add Pattern Node", actionEvent => AddElement(CreateNode(typeof(PatternNode), GetLocalMousePosition(actionEvent.eventInfo.mousePosition))));
			evt.menu.AppendAction("Add Composite Node", actionEvent => AddElement(CreateNode(typeof(CompositeNode), GetLocalMousePosition(actionEvent.eventInfo.mousePosition))));

			evt.menu.AppendAction("Add Group", actionEvent => AddElement(CreateGroup("Node Group", GetLocalMousePosition(actionEvent.eventInfo.mousePosition))));
		}

		private void AddGridBackground() {
			GridBackground gridBackground = new GridBackground();
			gridBackground.StretchToParentSize();

			Insert(0, gridBackground);
		}
		private void AddStyles() {
			this.AddStyleSheets(
				"BattleNodes/BattleNodeGraphViewStyles.uss",
				"BattleNodes/BattleNodeStyles.uss"
			);
		}

		public Vector2 GetLocalMousePosition(Vector2 mousePosition) {
			Vector2 worldMousePosition = mousePosition;

			Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);

			return localMousePosition;
		}
	}
}