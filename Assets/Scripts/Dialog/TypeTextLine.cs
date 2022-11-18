using Combustion.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Dialog
{
	[CreateAssetMenu(fileName = "New Type Text Line", menuName = "Combustion/Dialog/Type Text Line")]
	public class TypeTextLine : DialogLine
	{
		public override IEnumerator Parse(DialogController controller) {
			for (int i = 0; i < text.Length; i++)
			{
				controller.currentText += text[i];
				yield return new WaitForSeconds(typeDelay);
			}
		}
	}
}
