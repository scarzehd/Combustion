using System;
using System.Collections;

namespace Combustion.Dialog
{
    public abstract class DialogLine
    {
        public float typeDelay;

        public string text;

        public abstract IEnumerator Parse(DialogContainer parser);
	}

}
