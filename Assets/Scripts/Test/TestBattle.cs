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

	[SerializeField]
	private GameObject debugUI;

	private Label turnStateText;

	private Button switchTurnStateButton;

	public bool debugMode;

	protected override void Start() {
		base.Start();

		VisualElement debugRoot = debugUI.GetComponent<UIDocument>().rootVisualElement;

		turnStateText = debugRoot.Q<Label>("TurnState");
		switchTurnStateButton = debugRoot.Q<Button>("SwitchTurnState");
		switchTurnStateButton.RegisterCallback<ClickEvent>((evt) => { AdvanceTurnState(); });
	}

	protected override void Update() {
		base.Update();

		if (debugMode && !debugUI.activeSelf)
		{
			debugUI.SetActive(true);
		} else if (!debugMode && debugUI.activeSelf)
		{
			debugUI.SetActive(false);
		}
	}

	protected override void StartPlayerTurn() {
		base.StartPlayerTurn();

		MenuController.Instance.SelectCurrentButton();
	}

	public override void AdvanceTurnState() {
		base.AdvanceTurnState();

		turnStateText.text = turnState == TurnState.Player ? "Player Turn" : "Enemy Turn";
	}

	public void MoveArena() {
		ArenaController.Instance.MoveAndScaleArena(Random.Range(minPosX, maxPosX), Random.Range(minPosY, maxPosY), Random.Range(minSizeX, maxSizeX), Random.Range(minSizeY, maxSizeY), 1f);

		Debug.Log("MoveArena called");
	}
}