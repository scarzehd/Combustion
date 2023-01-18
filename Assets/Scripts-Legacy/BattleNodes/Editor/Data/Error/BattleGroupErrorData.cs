using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Combustion.BattleNodes.Data.Error
{
	using Elements;
	
	public class BattleGroupErrorData
	{
		public BattleErrorData ErrorData { get; set; }
		public List<BattleGroup> Groups { get; set; }

		public BattleGroupErrorData() {
			ErrorData = new BattleErrorData();
			Groups = new List<BattleGroup>();
		}
	}
}