using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using Combustion.Battle.Nodes;
using System;
using System.Linq;

namespace Combustion.Editor.Nodes
{
    public class BattleNodesGraphView : GraphView
    {
		public new class UxmlFactory : UxmlFactory<BattleNodesGraphView, GraphView.UxmlTraits> { }

        BattleTree tree;

		public Action<NodeView> OnNodeSelected;

		public BattleNodesGraphView() {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
			
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/Nodes/BattleNodesEditor.uss");
            styleSheets.Add(styleSheet);
        }

		NodeView FindNodeView(BattleNode node) {
			return GetNodeByGuid(node.guid) as NodeView;
		}

		public void PopulateView(BattleTree tree) {
			this.tree = tree;

			graphViewChanged -= OnGraphViewChanged;
			DeleteElements(graphElements);
			graphViewChanged += OnGraphViewChanged;

			if (tree.rootNode == null)
			{
				tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
				EditorUtility.SetDirty(tree);
				AssetDatabase.SaveAssets();
			}

			foreach (BattleNode node in tree.nodes)
			{
                CreateNodeView(node);
			}

			foreach (BattleNode node in tree.nodes)
			{
				var children = tree.GetChildren(node);
				
				foreach (BattleNode child in children)
				{
					NodeView parentView = FindNodeView(node);

					NodeView childView = FindNodeView(child);

					Edge edge = parentView.output.ConnectTo(childView.input);
					AddElement(edge);
				}
			}
		}

		private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange) {
			if (graphViewChange.elementsToRemove != null)
			{
				foreach (var element in graphViewChange.elementsToRemove)
				{
					NodeView nodeView = element as NodeView;
					if (nodeView != null)
					{
						tree.DeleteNode(nodeView.node);
					}

					Edge edge = element as Edge;
					if (edge != null)
					{
						NodeView parentView = edge.output.node as NodeView;
						NodeView childView = edge.input.node as NodeView;
						tree.RemoveChild(parentView.node, childView.node);
					}
				}
			}

			if (graphViewChange.edgesToCreate != null)
			{
				foreach (Edge edge in graphViewChange.edgesToCreate)
				{
					NodeView parentView = edge.output.node as NodeView;
					NodeView childView = edge.input.node as NodeView;

					tree.AddChild(parentView.node, childView.node);
				}
			}
			
			return graphViewChange;
		}

		public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
			return ports.ToList().Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
		}

		public override void BuildContextualMenu(ContextualMenuPopulateEvent evt) {
			{
                var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
				foreach (var type in types)
				{
					evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
				}
			}

			{
				var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
				foreach (var type in types)
				{
					evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
				}
			}

			{
				var types = TypeCache.GetTypesDerivedFrom<ModifyNode>();
				foreach (var type in types)
				{
					evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
				}
			}
		}

		void CreateNode(System.Type type) {
			BattleNode node = tree.CreateNode(type);
			CreateNodeView(node);
		}

		void CreateNodeView(BattleNode node) {
            NodeView view = new NodeView(node);
			view.OnNodeSelected = OnNodeSelected;
            AddElement(view);
		}
	}
}
