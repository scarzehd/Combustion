using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Combustion.Dialog
{
    public abstract class DialogLine : ScriptableObject
    {
        public float typeDelay;

        public string text;

        public abstract IEnumerator Parse(DialogController parser);
	}

}
