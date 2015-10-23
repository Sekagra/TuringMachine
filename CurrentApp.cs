using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TuringMachine
{
    public static class CurrentApp
    {
        public static App Instance
        {
            get { return ((App)Application.Current); }
        } 
    }
}
