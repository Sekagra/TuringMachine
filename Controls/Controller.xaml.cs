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
    /// Interaktionslogik für Controller.xaml
    /// </summary>
    public partial class Controller : UserControl
    {
        public Controller()
        {
            InitializeComponent();
        }

        #region Fields
            public static readonly DependencyProperty MachineProperty = DependencyProperty.Register("Machine", typeof(Machine), typeof(Controller), null);
        #endregion

        #region Properties
            public Machine Machine
            {
                get { return (Machine)GetValue(MachineProperty); }
                set { SetValue(MachineProperty, value); }
            }
        #endregion

        #region Events
            private void cmdStart_Click(object sender, RoutedEventArgs e)
            {
                Machine.Start(CurrentApp.Instance.Delay);
            }

            private void cmdStep_Click(object sender, RoutedEventArgs e)
            {
                Machine.Step();
            }

            private void cmdStop_Click(object sender, RoutedEventArgs e)
            {
                Machine.Stop();
            }
        #endregion
    }
}
