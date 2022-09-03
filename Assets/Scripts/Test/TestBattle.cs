using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Combustion.Battle;
using TMPro;

public class TestBattle : BattleManager
{
	[SerializeField]
	private float minSizeX, maxSizeX, minSizeY, maxSizeY, minPosX, maxPosX, minPosY, maxPosY;

	public void MoveArena() {
		ArenaController.Instance.MoveAndScaleArena(Random.Range(minPosX, maxPosX), Random.Range(minPosY, maxPosY), Random.Range(minSizeX, maxSizeX), Random.Range(minSizeY, maxSizeY), 1f);

		Debug.Log("MoveArena called");
	}

	public void SpawnTestPattern() {
		TestPattern2 testPattern = Resources.Load<TestPattern2>("Patterns/Test Pattern 2");
		testPattern.Spawn();
		currentPattern = testPattern;
	}
}