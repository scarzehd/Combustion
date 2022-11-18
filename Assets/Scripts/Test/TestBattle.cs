using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Combustion.Battle;
using Combustion.UI;
using System;
using UnityEngine.Events;
using Combustion.Dialog;

public class TestBattle : BattleManager
{
	[SerializeField]
	private float minSizeX, maxSizeX, minSizeY, maxSizeY, minPosX, maxPosX, minPosY, maxPosY;

	[SerializeField] private string boxText;

	[SerializeField] private float typeDelay;

	[SerializeField] private AudioClip buttonSelectAudio;

	public List<DialogLine> lines;

	protected override void Start() {
		base.Start();

		//MenuManager.Instance hasn't been assigned yet at this point. Possible Solutions:
		//1. Change the Script Execution Order to make BattleManager scripts execute later
		//2. Create a Setup method that executes after the Start method of all other scripts

		//1 is a more temporary solution, so I'll probably go with 2
		MenuManager.Instance.onButtonSelect += PlayButtonSelectAudio;
		MenuManager.Instance.onMenuItemSelect += PlayButtonSelectAudio;
	}

	public void PlayButtonSelectAudio(int index) {
		SoundManager.Instance.PlaySound(buttonSelectAudio);
	}


	public void MoveArena() {
		ArenaController.Instance.MoveAndScaleArena(UnityEngine.Random.Range(minPosX, maxPosX), UnityEngine.Random.Range(minPosY, maxPosY), UnityEngine.Random.Range(minSizeX, maxSizeX), UnityEngine.Random.Range(minSizeY, maxSizeY), 1f);

		Debug.Log("MoveArena called");
	}

	protected override void StartPlayerTurn() {
		base.StartPlayerTurn();

		MenuManager.Instance.ShowDialog(lines);
	}
}