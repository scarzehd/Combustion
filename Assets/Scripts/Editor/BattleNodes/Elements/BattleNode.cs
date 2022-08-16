using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

using Combustion.Editor.BattleNodes.Utilities;

namespace Combustion.Editor.BattleNodes.Elements
{
	public class BattleNode : Node
	{
		public string NodeName { get; set; }

		public virtual void Initialize(Vector2 position) {
			NodeName = "Node";

			SetPosition(new Rect(position, Vector2.zero));

			extensionContainer.AddToClassList("battle-node__extension-container");
		}

		public virtual void Draw() {
			TextField nodeNameTextField = BattleElementUtilities.CreateTextField(NodeName);

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
	}
}