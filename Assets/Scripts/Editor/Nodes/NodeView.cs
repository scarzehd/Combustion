using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;

using Combustion.Battle.Nodes;

namespace Combustion.Editor.Nodes
{
	public class NodeView : Node {
		public BattleNode node;

		public Port input;
		public Port output;

		public Action<NodeView> OnNodeSelected;

		public NodeView(BattleNode node) {
			this.node = node;
			this.title = node.name;
			this.viewDataKey = node.guid;
			
			style.left = node.position.x;
			style.top = node.position.y;

			CreateInputPorts();
			CreateOutputPorts();
		}

		private void CreateInputPorts() {
			switch (node)
			{
				case ActionNode node:
					input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
					break;
				case CompositeNode node:
					input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
					break;
				case ModifyNode node:
					input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
					break;
				case RootNode node:
					break;
			}

			if (input != null)
			{
				input.portName = "";
				inputContainer.Add(input);
			}
		}

		private void CreateOutputPorts() {
			switch (node)
			{
				case ActionNode node:
					break;
				case CompositeNode node:
					output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
					break;
				case ModifyNode node:
					output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
					break;
				case RootNode node:
					output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
					break;
			}

			if (output != null)
			{
				output.portName = "";
				outputContainer.Add(output);
			}
		}

		public override void SetPosition(Rect newPos) {
			base.SetPosition(newPos);

			node.position.x = newPos.xMin;
			node.position.y = newPos.yMin;
		}

		public override void OnSelected() {
			base.OnSelected();

			if (OnNodeSelected != null)
			{
				OnNodeSelected.Invoke(this);
			}
		}
	}
}