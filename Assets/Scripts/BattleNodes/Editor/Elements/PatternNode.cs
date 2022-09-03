using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Combustion.BattleNodes.Elements
{
	using Projectile;

	public class PatternNode : ActionNode
	{
		public Pattern Pattern { get; set; }

		public string PatternPath { get; set; }

		public override void Draw() {
			base.Draw();

			TextField patternPathTextField = new TextField("Pattern Path");

			patternPathTextField.AddToClassList("battle-node__textfield");

			extensionContainer.Add(patternPathTextField);

			RefreshExpandedState();
		}
	}

}