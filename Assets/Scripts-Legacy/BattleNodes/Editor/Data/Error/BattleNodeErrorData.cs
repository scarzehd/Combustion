using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.BattleNodes.Data.Error
{
	using Elements;
	
	public class BattleNodeErrorData
	{
		public BattleErrorData ErrorData { get; set; }

		public List<BattleNode> Nodes { get; set; }

		public BattleNodeErrorData() {
			ErrorData = new BattleErrorData();

			Nodes = new List<BattleNode>();
		}
	}
}