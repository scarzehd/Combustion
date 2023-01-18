using Combustion.Dialog;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Dialog
{
	public class TypeTextLine : DialogLine
	{
		public override IEnumerator Parse(DialogContainer controller) {
			for (int i = 0; i < text.Length; i++)
			{
				controller.currentText += text[i];
				yield return new WaitForSeconds(typeDelay);
			}
		}
	}
}
