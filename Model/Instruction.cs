using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringMachine.Model
{
    public class Instruction
    {
        #region Properties
            public int State { get; set; }
            public char Read { get; set; }
            public char Write { get; set; }
            public Direction Direction { get; set; }
            public int NewState { get; set; }
        #endregion
    }

    public enum Direction
    {
        None = 0,
        Left = 1,
        Right = 2
    }
}
