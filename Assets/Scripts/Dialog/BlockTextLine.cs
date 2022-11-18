using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Dialog
{
	[CreateAssetMenu(fileName = "New Block Text Line", menuName = "Combustion/Dialog/Block Text Line")]
	public class BlockTextLine : DialogLine
	{
		public override IEnumerator Parse(DialogController parser) {
			parser.currentText += text;
			yield return new WaitForSeconds(typeDelay);
		}
	}
}
