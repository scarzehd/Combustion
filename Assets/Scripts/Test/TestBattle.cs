using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Combustion.Battle;
using Combustion.UI;
using TMPro;
using UnityEngine.UIElements;

public class TestBattle : BattleManager
{
	[SerializeField]
	private float minSizeX, maxSizeX, minSizeY, maxSizeY, minPosX, maxPosX, minPosY, maxPosY;

	public void MoveArena() {
		ArenaController.Instance.MoveAndScaleArena(Random.Range(minPosX, maxPosX), Random.Range(minPosY, maxPosY), Random.Range(minSizeX, maxSizeX), Random.Range(minSizeY, maxSizeY), 1f);

		Debug.Log("MoveArena called");
	}
}