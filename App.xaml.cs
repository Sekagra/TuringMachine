using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.IO;
using TuringMachine.Model;
using System.ComponentModel;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace TuringMachine
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {
        //The Application isn't a DependencyObject. Therefore DependencyProperties are not availiable.
        #region Fields
            private Machine _currentMachine;
            private FileInfo _loadedFile;
            private int _speed;
            private LogMessageList _messages = new LogMessageList();
        #endregion

        #region Properties
            public Machine CurrentMachine
            { 
                get { return _currentMachine; }
                set { _currentMachine = value; OnPropertyChanged("CurrentMachine"); } 
            }
            
            public FileInfo LoadedFile
            {
                get 
                {
                    if (_loadedFile == null)
                        return new FileInfo("Fubar");
                    else
                        return _loadedFile; 
                }
                set { _loadedFile = value; OnPropertyChanged("LoadedFile"); }
            }
            
            public int Speed
            {
                get { return _speed; }
                set { _speed = value; OnPropertyChanged("Speed"); }
            }

            public int Delay
            {
                get { return (104 - Speed) * 25; }
            }

            public LogMessageList Messages
            {
                get { return _messages; }
                set { _messages = value; OnPropertyChanged("Messsages"); }
            }      
        #endregion

        #region Methoden
            /// <summary>
            /// Loading a previously saved Turing machine from an external file.
            /// </summary>
            /// <param name="path">The path where the Turing machine is located.</param>
            public void MachineLoad(string path)
            {
                try
                {
                    CurrentMachine = XDocument.Load(path).ToMachine();
                    Messages.Add("Turing machine successfully loaded.");
                    LoadedFile = new FileInfo(path);
                }
                catch (Exception ex)
                {
                    Messages.Add("Error loading the file '" + path + "'.");
                }
                
            }

            /// <summary>
            /// Saves the currently active machine to an external file.
            /// </summary>
            /// <param name="path">The path to the new file.</param>
            public void MachineSave(string path)
            {
                try 
                {
                    CurrentMachine.ToXML().Save(path);
                    Messages.Add("Turing machine successfully saved as '" + path + "'.");
                }
                catch(Exception ex)
                {
                    Messages.Add("Error saving the file '" + path + "'. Check your premissions.");
                }
            }
        #endregion

        #region INotifyPropertyChanged Handler
            private void OnPropertyChanged(string property)
            {
                var handler = PropertyChanged;
                if (handler != null) { handler(this, new PropertyChangedEventArgs(property)); }
            }

            public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
