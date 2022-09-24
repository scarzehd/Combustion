using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Combustion.BattleNodes.Elements
{
	using Utilities;
	
	public class BattleNode : Node
	{
		public string NodeName { get; set; }

		private Color defaultBackgroundColor;

		private BattleNodeGraphView graphView;

		public BattleGroup Group { get; set; }

		public virtual void Initialize(BattleNodeGraphView graphView, Vector2 position) {
			NodeName = "Node";

			SetPosition(new Rect(position, Vector2.zero));

			extensionContainer.AddToClassList("battle-node__extension-container");

			defaultBackgroundColor = new Color(29f / 255, 29f / 255, 30f / 255);

			this.graphView = graphView;
		}

		public virtual void Draw() {
			TextField nodeNameTextField = BattleElementUtilities.CreateTextField(NodeName, null, callback =>
			{
				if (Group == null)
				{
					graphView.RemoveUngroupedNode(this);

					NodeName = callback.newValue;

					graphView.AddUngroupedNode(this);

					return;
				}

				//use this because RemoveGroupedNode() sets the Node's Group property to null
				BattleGroup currentGroup = Group;

				graphView.RemoveGroupedNode(this, Group);

				NodeName = callback.newValue;

				graphView.AddGroupedNode(this, currentGroup);
			});

			nodeNameTextField.AddClasses(
				"battle-node__textfield",
				"battle-node__textfield__hidden",
				"battle-node__filename-textfield"
			);

			titleContainer.Insert(0, nodeNameTextField);

			Port inputPort = this.CreatePort("Input", Orientation.Horizontal, Direction.Input, Port.Capacity.Multi);
			inputPort.portName = "Input";

			inputContainer.Add(inputPort);
		}

		public void DisconnectAllPorts() {
			DisconnectInputPorts();
			DisconnectOutputPorts();
		}

		private void DisconnectInputPorts() {
			DisconnectPorts(inputContainer);
		}

		private void DisconnectOutputPorts() {
			DisconnectPorts(outputContainer);
		}

		private void DisconnectPorts(VisualElement container) {
			foreach (VisualElement element in container.Children())
			{
				if (element is Port port)
				{
					if (port.connected)
					{
						graphView.DeleteElements(port.connections);
					}
				}
			}
		}

		public void SetErrorStyle(Color color) {
			mainContainer.style.backgroundColor = color;
		}

		public void ResetStyle() {
			mainContainer.style.backgroundColor = defaultBackgroundColor;
		}

		public override void BuildContextualMenu(ContextualMenuPopulateEvent evt) {
			evt.menu.AppendAction("Disconnect Input Ports", actionEvent => DisconnectInputPorts());
			evt.menu.AppendAction("Disconnect Output Ports", actionEvent => DisconnectOutputPorts());

			base.BuildContextualMenu(evt);
		}
	}
}