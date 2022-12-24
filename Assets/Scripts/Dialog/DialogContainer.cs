using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Dialog
{
    using Combustion.UI;

    [CreateAssetMenu(fileName = "New Dialog Container", menuName = "Combustion/Dialog/Dialog Container")]
    public class DialogContainer : ScriptableObject
    {
        public List<DialogLine> lines;

        public string currentText;

        public bool IsDone { get; private set; }

        public DialogContainer() {
            lines = new List<DialogLine>();
            IsDone = false;
            currentText = "";
        }

        public IEnumerator Parse() {
            foreach (DialogLine line in lines)
            {
                yield return MenuManager.Instance.StartCoroutine(line.Parse(this));
            }

            IsDone = true;
        }

        public void Reset(bool keepLines = false) {
            currentText = "";
            IsDone = false;
            if (!keepLines)
            {
                lines = new List<DialogLine>();
            }
        }
    }
}
