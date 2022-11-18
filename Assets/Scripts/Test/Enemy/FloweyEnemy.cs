using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Combustion.Battle;
using Combustion.Projectile;
using Combustion.Dialog;

public class FloweyEnemy : Enemy
{
	public override Pattern ChoosePattern() {
		return patterns[0];
	}

	public void ActBeg() {
		BattleManager.Instance.AdvanceTurnState();
	}
}