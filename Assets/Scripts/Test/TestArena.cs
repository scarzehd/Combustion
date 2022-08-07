using UnityEngine;

using Combustion.Battle;
using TMPro;

public class TestArena : MonoBehaviour
{
	[SerializeField]
	private float minSizeX, maxSizeX, minSizeY, maxSizeY, minPosX, maxPosX, minPosY, maxPosY;

	[SerializeField]
	private TextMeshProUGUI turnStateText;

	public void MoveArena() {
		ArenaController.Instance.MoveAndScaleArena(Random.Range(minPosX, maxPosX), Random.Range(minPosY, maxPosY), Random.Range(minSizeX, maxSizeX), Random.Range(minSizeY, maxSizeY), 1f);
	}

	public void SpawnTestPattern() {
		TestPattern2 testPattern = Resources.Load<TestPattern2>("Patterns/Test Pattern 2");
		testPattern.Spawn();
		BattleManager.Instance.currentPattern = testPattern;
	}
}
