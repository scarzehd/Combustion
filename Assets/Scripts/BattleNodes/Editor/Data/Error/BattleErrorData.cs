using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.BattleNodes.Data.Error
{
	public class BattleErrorData
	{
		public Color Color { get; set; }

		public BattleErrorData() {
			GenerateRandomColor();
		}

		private void GenerateRandomColor() {
			Color = new Color32(
				(byte) Random.Range(65, 256),
				(byte) Random.Range(50, 176),
				(byte) Random.Range(50, 176),
				255
			);
		}

	}
}