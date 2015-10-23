using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TuringMachine.Model
{
    public class LogMessageList : ObservableCollection<string>
    {
        public LogMessageList() : base() { } //Just to make the code clean. The parameterless base constructor would be called implicitly anyway.

        /// <summary>
        /// Returns the same as ToString(), this just makes it usable through binding
        /// </summary>
        public string AsString
        {
            get { return this.ToString(); }
        }

        /// <summary>
        /// Adds a new message to the list and generating a timestamp.
        /// </summary>
        /// <param name="text">The Message to append as string.</param>
        public new void Add(string text)
        {
            base.Add(string.Format("{0:T}", DateTime.Now) + " " + text);
            OnPropertyChanged(new PropertyChangedEventArgs("AsString"));
        }

        /// <summary>
        /// Override the original ToString()-Method to provide a complete string containing all items.
        /// </summary>
        /// <returns>A string representation of the collection.</returns>
        public override string ToString()
        {
            var __sbList = new StringBuilder("");
            #region Add all items value to the string
                foreach (var __item in this)
                {
                    __sbList.Append(__item.ToString() + Environment.NewLine);
                }
            #endregion
            return __sbList.ToString();
        }
    }
}
