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
    /// Interaktionslogik für Logger.xaml
    /// </summary>
    public partial class Logger : UserControl
    {
        public Logger()
        {
            InitializeComponent();
        }

        #region Fields
            public static readonly DependencyProperty MessagesProperty = DependencyProperty.Register("Messages", typeof(LogMessageList), typeof(Logger), null);            
        #endregion

        #region Properties
            public string Messages
            {
                get { return (string)GetValue(MessagesProperty); }
                set { SetValue(MessagesProperty, value); }
            }
        #endregion

        #region Events
            //Since its a GUI operation its ok to do it here...
            private void txtMsg_TextChanged(object sender, TextChangedEventArgs e)
            {
                txtMsg.ScrollToEnd();
            }
        #endregion
    }
}
