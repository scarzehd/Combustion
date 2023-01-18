using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Combustion.BattleNodes
{
	using Combustion.BattleNodes.Data.Error;
	using Elements;
	using Utilities;

	public class BattleNodeGraphView : GraphView
	{
		private BattleNodeSearchWindow searchWindow;

		private BattleNodeEditor editorWindow;

		private SerializableDictionary<string, BattleNodeErrorData> ungroupedNodes;
		private SerializableDictionary<Group, SerializableDictionary<string, BattleNodeErrorData>> groupedNodes;
		private SerializableDictionary<string, BattleGroupErrorData> groups;

		private int repeatedNamesAmount;

		public int RepeatedNamesAmount {
			get {
				return repeatedNamesAmount;
			}
			set {
				repeatedNamesAmount = value;

				if (repeatedNamesAmount == 0)
				{
					editorWindow.EnableSaving();
				} else
				{
					editorWindow.DisableSaving();
				}
			}
		}

		public BattleNodeGraphView(BattleNodeEditor editorWindow) {
			AddGridBackground();
			AddStyles();
			AddManipulators();

			AddSearchWindow();
			this.editorWindow = editorWindow;

			ungroupedNodes = new SerializableDictionary<string, BattleNodeErrorData>();
			groupedNodes = new SerializableDictionary<Group, SerializableDictionary<string, BattleNodeErrorData>>();
			groups = new SerializableDictionary<string, BattleGroupErrorData>();
			

			OnElementsDeleted();
			OnGroupElementsAdded();
			OnGroupElementsRemoved();
			OnGroupRenamed();
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

			node.Initialize(this, position);
			node.Draw();

			AddUngroupedNode(node);

			AddElement(node);

			return node;
		}

		public BattleGroup CreateGroup(string title, Vector2 position) {
			BattleGroup group = new BattleGroup(title, position);

			AddGroup(group);

			AddElement(group);

			foreach (GraphElement selectedElement in selection)
			{
				if (selectedElement is BattleNode node)
				{
					group.AddElement(node);
				}
			}

			return group;
		}

		private void AddGroup(BattleGroup group) {
			string groupName = group.title;

			if (!groups.ContainsKey(groupName))
			{
				BattleGroupErrorData groupErrorData = new BattleGroupErrorData();

				groupErrorData.Groups.Add(group);

				groups.Add(groupName, groupErrorData);

				return;
			}

			List<BattleGroup> groupsList = groups[groupName].Groups;

			groupsList.Add(group);

			Color errorColor = groups[groupName].ErrorData.Color;

			group.SetErrorStyle(errorColor);

			if (groupsList.Count == 2)
			{
				RepeatedNamesAmount++;

				groupsList[0].SetErrorStyle(errorColor);
			}
		}

		private void RemoveGroup(BattleGroup group) {
			string oldGroupName = group.oldTitle;

			List<BattleGroup> groupsList = groups[oldGroupName].Groups;

			groupsList.Remove(group);

			group.ResetStyle();

			if (groupsList.Count == 1)
			{
				RepeatedNamesAmount--;

				groupsList[0].ResetStyle();
			}

			if (groupsList.Count == 0)
			{
				groups.Remove(oldGroupName);
			}
		}

		public void AddUngroupedNode(BattleNode node) {
			string nodeName = node.NodeName;

			if (!ungroupedNodes.ContainsKey(nodeName))
			{
				BattleNodeErrorData errorData = new BattleNodeErrorData();
				errorData.Nodes.Add(node);
				ungroupedNodes.Add(nodeName, errorData);

				return;
			}

			ungroupedNodes[nodeName].Nodes.Add(node);

			Color errorColor = ungroupedNodes[nodeName].ErrorData.Color;

			node.SetErrorStyle(errorColor);

			if (ungroupedNodes[nodeName].Nodes.Count == 2)
			{
				RepeatedNamesAmount++;

				ungroupedNodes[nodeName].Nodes[0].SetErrorStyle(errorColor);
			}
		}

		public void RemoveUngroupedNode(BattleNode node) {
			string nodeName = node.NodeName;

			ungroupedNodes[node.NodeName].Nodes.Remove(node);

			node.ResetStyle();

			if (ungroupedNodes[nodeName].Nodes.Count == 1)
			{
				RepeatedNamesAmount--;

				ungroupedNodes[nodeName].Nodes[0].ResetStyle();
			}

			if (ungroupedNodes[nodeName].Nodes.Count == 0)
			{
				ungroupedNodes.Remove(nodeName);
			}
		}
		
		public void AddGroupedNode(BattleNode node, BattleGroup group) {
			string nodeName = node.NodeName;

			node.Group = group;

			if (!groupedNodes.ContainsKey(group))
			{
				groupedNodes.Add(group, new SerializableDictionary<string, BattleNodeErrorData>());
			}

			if (!groupedNodes[group].ContainsKey(nodeName))
			{
				BattleNodeErrorData errorData = new BattleNodeErrorData();
				errorData.Nodes.Add(node);
				groupedNodes[group].Add(nodeName, errorData);

				return;
			}

			List<BattleNode> groupedNodesList = groupedNodes[group][nodeName].Nodes;
			groupedNodesList.Add(node);

			Color errorColor = groupedNodes[group][nodeName].ErrorData.Color;

			node.SetErrorStyle(errorColor);

			if (groupedNodesList.Count == 2)
			{
				RepeatedNamesAmount++;

				groupedNodesList[0].SetErrorStyle(errorColor);
			}
		}

		public void RemoveGroupedNode(BattleNode node, Group group) {
			node.Group = null;

			string nodeName = node.NodeName;
			List<BattleNode> groupedNodesList = groupedNodes[group][nodeName].Nodes;
			groupedNodesList.Remove(node);
			node.ResetStyle();
			if (groupedNodesList.Count == 1)
			{
				RepeatedNamesAmount--;

				groupedNodesList[0].ResetStyle();
			}
			if (groupedNodesList.Count == 0)
			{
				groupedNodes[group].Remove(nodeName);

				if (groupedNodes[group].Count == 0)
				{
					groupedNodes.Remove(group);
				}
			}
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

			evt.menu.AppendAction("Add Group", actionEvent => CreateGroup("Node Group", GetLocalMousePosition(actionEvent.eventInfo.mousePosition)));
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

		public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false) {
			Vector2 worldMousePosition = mousePosition;

			if (isSearchWindow)
			{
				worldMousePosition -= editorWindow.position.position;
			}

			Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);

			return localMousePosition;
		}

		private void OnElementsDeleted() {
			deleteSelection = (operationName, askUser) =>
			{
				List<BattleGroup> groupsToDelete = new List<BattleGroup>();
				List<Edge> edgesToDelete = new List<Edge>();
				List<BattleNode> nodesToDelete = new List<BattleNode>();

				foreach (GraphElement element in selection)
				{
					if (element is BattleNode node)
					{
						nodesToDelete.Add(node);

						continue;
					}

					if (element is Edge edge)
					{
						edgesToDelete.Add(edge);
					}

					if (element is BattleGroup group)
					{
						groupsToDelete.Add(group);
					}
				}

				foreach (BattleGroup group in groupsToDelete)
				{
					List<BattleNode> groupedNodes = new List<BattleNode>();

					foreach (GraphElement graphElement in group.containedElements)
					{
						if (graphElement is BattleNode node)
						{
							groupedNodes.Add(node);
						}
					}

					group.RemoveElements(groupedNodes);

					RemoveGroup(group);

					RemoveElement(group);
				}

				DeleteElements(edgesToDelete);

				foreach (BattleNode node in nodesToDelete)
				{
					if (node.Group != null)
					{
						node.Group.RemoveElement(node);
					}
					
					RemoveUngroupedNode(node);

					node.DisconnectAllPorts();

					RemoveElement(node);
				}
			};
		}

		private void OnGroupElementsAdded() {
			elementsAddedToGroup = (group, elements) =>
			{
				foreach (GraphElement element in elements)
				{
					if (element is BattleNode node)
					{
						BattleGroup nodeGroup = group as BattleGroup;

						RemoveUngroupedNode(node);
						AddGroupedNode(node, nodeGroup);
					}
				}
			};
		}

		private void OnGroupElementsRemoved() {
			elementsRemovedFromGroup = (group, elements) =>
			{
				foreach (GraphElement element in elements)
				{
					if (element is BattleNode node)
					{
						RemoveGroupedNode(node, group);
						AddUngroupedNode(node);
					}
				}
			};
		}

		private void OnGroupRenamed() {
			groupTitleChanged = (group, newTitle) =>
			{
				BattleGroup battleGroup = group as BattleGroup;

				RemoveGroup(battleGroup);

				battleGroup.oldTitle = newTitle;

				AddGroup(battleGroup);
			};
		}
	}
}