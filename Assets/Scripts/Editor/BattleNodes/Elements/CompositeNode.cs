using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

using Combustion.Editor.BattleNodes.Utilities;

namespace Combustion.Editor.BattleNodes.Elements
{
	public class CompositeNode : BattleNode
	{
		public int Connections { get; set; }

		public override void Initialize(Vector2 position) {
			base.Initialize(position);

			Connections = 1;
		}

		public override void Draw() {
			base.Draw();

			Button addPortButton = BattleElementUtilities.CreateButton("+");

			mainContainer.Insert(1, addPortButton);

			for (int i = 0; i < Connections; i++)
			{
				Port port = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

				port.portName = "";

				Button deletePortButton = BattleElementUtilities.CreateButton("X");

				port.Add(deletePortButton);
				
				outputContainer.Add(port);
			}

			RefreshExpandedState();
		}
	}
}