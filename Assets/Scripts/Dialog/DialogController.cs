using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combustion.Dialog
{
    using Combustion.UI;
    using Utility;

    public class DialogController
    {
        private List<DialogLine> lines;

        public string currentText;

        public bool isDone;

        public DialogController() {
            lines = new List<DialogLine>();
            isDone = false;
            currentText = "";
        }

        public void AddLine(DialogLine line) {
            lines.Add(line);
        }

        public void AddLines(List<DialogLine> lines) {
            foreach (DialogLine line in lines)
            {
                this.lines.Add(line);
            }
        }

        public List<DialogLine> GetLines() {
            return lines;
        }

        public IEnumerator Parse() {
            foreach (DialogLine line in lines)
            {
                yield return MenuManager.Instance.StartCoroutine(line.Parse(this));
            }

            isDone = true;
        }

        public void Reset(bool keepLines = false) {
            currentText = "";
            isDone = false;
            if (!keepLines)
            {
                lines = new List<DialogLine>();
            }
        }
    }
}
