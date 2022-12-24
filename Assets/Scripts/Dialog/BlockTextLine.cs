using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Dialog
{
	public class BlockTextLine : DialogLine
	{
		public override IEnumerator Parse(DialogContainer parser) {
			parser.currentText += text;
			yield return new WaitForSeconds(typeDelay);
		}
	}
}
