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
using System.Collections.ObjectModel;
using TuringMachine.Model;

namespace TuringMachine.Controls
{
    /// <summary>
    /// Interaktionslogik für InstructionTable.xaml
    /// </summary>
    public partial class InstructionEditor : UserControl
    {
        public InstructionEditor()
        {
            InitializeComponent();
        }

        #region Fields
            public static readonly DependencyProperty InstructionsProperty = DependencyProperty.Register("Instructions", typeof(ObservableCollection<Instruction>), typeof(InstructionEditor), null);
            public static readonly DependencyProperty SelectedInstructionProperty = DependencyProperty.Register("SelectedInstruction", typeof(Instruction), typeof(InstructionEditor), null);
            public static readonly DependencyProperty PathProperty = DependencyProperty.Register("Path", typeof(string), typeof(InstructionEditor), null);
        #endregion

        #region Properties
            public ObservableCollection<Instruction> Instructions
            {
                get { return (ObservableCollection<Instruction>)GetValue(InstructionsProperty); }
                set { SetValue(InstructionsProperty, value); }
            }

            public Instruction SelectedInstruction
            {
                get { return (Instruction)GetValue(SelectedInstructionProperty); }
                set { SetValue(SelectedInstructionProperty, value); }
            }

            public string Path
            {
                get { return (string)GetValue(PathProperty); }
                set { SetValue(PathProperty, value); }
            }
        #endregion

        #region Events
            private void cmdDelete_Click(object sender, RoutedEventArgs e)
            {
                if (SelectedInstruction != null)
                    Instructions.Remove(SelectedInstruction);
            }

            private void cmdClear_Click(object sender, RoutedEventArgs e)
            {
                Instructions.Clear();
            }

            private void cmdLoad_Click(object sender, RoutedEventArgs e)
            {
                #region Create and get a path from the OpenFileDialog
                    Microsoft.Win32.OpenFileDialog dlgOpen = new Microsoft.Win32.OpenFileDialog();
                    dlgOpen.Title = "TuringMachine laden...";
                    dlgOpen.Filter = "TuringMachinen|*.tm|All Files|*.*";
                    dlgOpen.FilterIndex = 0;
                    dlgOpen.ShowDialog();
                #endregion

                //In case the user clicked "Abort" there will be an empty string
                if (dlgOpen.FileName == string.Empty) { return; }

                CurrentApp.Instance.MachineLoad(dlgOpen.FileName);
            }

            private void cmdSave_Click(object sender, RoutedEventArgs e)
            {
                #region Create and get a path from the SaveFileDialog
                    Microsoft.Win32.SaveFileDialog dlgSave = new Microsoft.Win32.SaveFileDialog();
                    dlgSave.Title = "TuringMachine laden...";
                    dlgSave.Filter = "TuringMachinen (*.tm)|*.tm";
                    dlgSave.FilterIndex = 0;
                    dlgSave.ShowDialog();
                #endregion

                //In case the user clicked "Abort" there will be an empty string
                if (dlgSave.FileName == string.Empty) { return; }

                CurrentApp.Instance.MachineSave(dlgSave.FileName);
            }
        #endregion
    }
}
