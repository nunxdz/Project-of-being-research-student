using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronWinform
{
    public class Event : EventArgs
    {
        public Event(Hashtable h)
        {
            Hash = h;
        }

        private Hashtable hash;
        public Hashtable Hash
        {
            get { return hash; }
            set { hash = value; }
        }
    }
}
