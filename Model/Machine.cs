using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.ComponentModel;
using System.Windows.Threading;
using System.Timers;

namespace TuringMachine.Model
{
    public class Machine : INotifyPropertyChanged
    {
        #region Fields
            private string _tape;
            private int _position;
            private int _state;
            private char _blank;
            private Timer _timer;
            private bool _isRunning;
        #endregion

        #region Properties
            public string Tape
            {
                get { return _tape; }
                set
                {
                    #region If the new string is smaller, the position has to be resetted
                        if (_tape != null && value.Length < _tape.Length)
                            Position = 0;
                    #endregion
                    #region If string.empty is assigned use the blank character instead
                        if (value == string.Empty)
                            value = Blank.ToString();
                    #endregion
                    _tape = value; 
                    OnPropertyChanged("Tape");
                }
            }

            public int Position
            {
                get { return _position; }
                //Do the Tapechange here in case of the new index is out of range, so the Buttons can just ++ as well as the Move-Method
                set
                {
                    #region Check for the new index value being out of range and fill up the Tape with a blank character
                        if (value == -1)
                        {
                            Tape = Blank + Tape;
                            _position = 0;
                        }
                        else if (value == Tape.Length)
                        {
                            Tape = Tape + Blank;
                            _position = value;
                        }
                        else
                            _position = value;
                    #endregion

                    OnPropertyChanged("Position"); 
                }
            }

            public int State
            {
                get { return _state; }
                set { _state = value; OnPropertyChanged("State"); }
            }

            public char Blank
            {
                get { return _blank; }
                set { _blank = value; OnPropertyChanged("Blank"); }
            }

            public ObservableCollection<Instruction> Instructions { get; set; }

            public bool IsRunning
            {
                get { return _isRunning; }
                set { _isRunning = value; OnPropertyChanged("IsRunning"); }
            }

            private LogMessageList Messages { get; set; }
        #endregion

        #region Constructor
            public Machine()
            {
                IsRunning = false;
            }

            public Machine(LogMessageList messages)
            {
                IsRunning = false;
                Messages = messages;
            }
        #endregion

        #region Methods
            /// <summary>
            /// Starts the machine with a given delay between each step
            /// </summary>
            /// <param name="delay">Delay between the steps in milliseconds</param>
            public void Start(int delay)
            {
                _timer = new Timer(delay);
                _timer.Elapsed += new ElapsedEventHandler(MachineTick);
                IsRunning = true;
                #region If there is a registered LogMessageList, write a message
                    if (Messages != null)
                        Messages.Add("The Turing machine starts with " + delay + " milliseconds delay between steps.");
                #endregion                
                _timer.Start();
            }

            /// <summary>
            /// Stops the machine and provides a message about what caused the stop
            /// </summary>
            /// <param name="result">The last step's result.</param>
            private void Stop(MachineResult result)
            {
                _timer.Stop();
                IsRunning = false;
            }

            /// <summary>
            /// Stops the machine.
            /// </summary>
            public void Stop()
            {
                //Only this Stop() Method is public, so it always is an abortion triggered by the user.
                Stop(MachineResult.ABORTED);
            }

            private void MachineTick(object sender, ElapsedEventArgs e)
            {
                var __ret = Step();

                #region Stop the machine of the result isn't MachineResults.OK
                    if (__ret != MachineResult.OK)
                        Stop(__ret);
                #endregion
            }

            /// <summary>
            /// Executes one step of the machine. 
            /// </summary>
            /// <returns>The result of the step (succees, failed, HALT reached etc.)</returns>
            public MachineResult Step()
            {
                //Read the current character from the tape
                var __read = Tape[Position];

                //Get the correct instruction based of the current state and the character that has been read
                var __instruction = Instructions.Where(i => i.State == State && i.Read == __read).FirstOrDefault();

                //Return in case no instruction matches
                if (__instruction == null) { return MessageWrapper(MachineResult.NO_RULE); }

                #region Execute the instruction
                    Write(__instruction.Write);
                    Move(__instruction.Direction);
                    State = __instruction.NewState;
	            #endregion

                #region Return HALT if the new state is -1
                    if(State == -1)
                        return MessageWrapper(MachineResult.HALT);
                    else
                        return MessageWrapper(MachineResult.OK);
                #endregion
            }
            
            /// <summary>
            /// Moves the current position either +1, 0 or -1
            /// </summary>
            /// <param name="direction">The direction to move (left, right or nothing)</param>
            private void Move(Direction direction)
            {
                if (direction == Direction.Left) { Position--; }
                if (direction == Direction.Right) { Position++; }
            }

            /// <summary>
            /// Writes the character to the current position
            /// </summary>
            /// <param name="character">The new character</param>
            private void Write(char character)
            {
                #region Cast to char array in order to write the character
                    var __tape = Tape.ToCharArray();
                    __tape[Position] = character;
                    Tape = new String(__tape);
                #endregion
            }

            /// <summary>
            /// Writes a message about what caused the machine to stop to the MessageList
            /// </summary>
            /// <param name="result">The result to write a message about.</param>
            /// <returns>The unedited input value.</returns>
            private MachineResult MessageWrapper(MachineResult result)
            {
                if (Messages != null)
                {
                    #region Write a message about what caused the machine to stop
                        if (result == MachineResult.HALT)
                            Messages.Add("The Turing machine reached a defined end. (HALT)");
                        else if (result == MachineResult.NO_RULE)
                            Messages.Add("Turing machine stopped. There is no instruction for the current state.");
                        else if (result == MachineResult.ABORTED)
                            Messages.Add("The execution of the Turing machine has been stopped.");
                    #endregion
                }

                return result;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged(string property)
            {
                var handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs(property));
            }
        #endregion
    }

    public enum MachineResult
    {
        OK = 0,
        HALT = 1,
        NO_RULE = 2,
        ABORTED = 3
    }
}

