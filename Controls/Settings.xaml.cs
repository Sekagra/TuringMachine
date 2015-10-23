using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TuringMachine.Model;

namespace TuringMachine.Controls
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        #region Fields
            public static readonly DependencyProperty MachineProperty = DependencyProperty.Register("Machine", typeof(Machine), typeof(Settings), null);
            public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register("Speed", typeof(int), typeof(Settings), null);
        #endregion

        #region Properties
            public Machine Machine
            {
                get { return (Machine)GetValue(MachineProperty); }
                set { SetValue(MachineProperty, value); }
            }
            public int Speed
            {
                get { return (int)GetValue(SpeedProperty); }
                set { SetValue(SpeedProperty, value); }
            }
        #endregion
    }
}
