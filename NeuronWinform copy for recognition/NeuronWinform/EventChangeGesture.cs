using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static NeuronWinform.MainWindow;

namespace NeuronWinform
{
    public class EventChangeGesture : EventArgs
    {
        public EventChangeGesture(Position s)
        {
            State = s;
        }

        private Position state;
        public Position State
        {
            get{ return state; }
            set { state = value; }
        }
    }
}
