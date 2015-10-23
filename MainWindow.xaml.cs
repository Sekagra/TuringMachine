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
using System.Xml.Linq;
using TuringMachine.Model;

namespace TuringMachine
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = Application.Current as App;
            
            #region Write the opening message into the MessageLog
                CurrentApp.Instance.Messages.Add("---------------------------- Turing Machine Simulator 2.0 ----------------------------");
                CurrentApp.Instance.Messages.Add("--------------------------- © Roper C. McIntyre - May 2011 ---------------------------");
                CurrentApp.Instance.Messages.Add("------------------ Questions, ideas and bugs to: roper@sekagra.com -------------------");
                CurrentApp.Instance.Speed = 100;
            #endregion

            #region Create the default Turing machine from the XML in the resources
                var __stream = Application.GetResourceStream(new Uri("TuringMachine;component/Resources/DefaultMachine.xml", UriKind.Relative));
                CurrentApp.Instance.CurrentMachine = XDocument.Load(__stream.Stream).ToMachine();
            #endregion
        }
    }
}
