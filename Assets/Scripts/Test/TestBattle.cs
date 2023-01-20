using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion
{
	public class TestBattle : BattleManager
	{
		protected override void PlayerTurn() {
			state = BattleState.EnemyTurn;
		}
	}
}
