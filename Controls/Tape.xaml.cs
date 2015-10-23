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

namespace TuringMachine.Controls
{
    /// <summary>
    /// Interaktionslogik für Tape.xaml
    /// </summary>
    public partial class Tape : UserControl
    {
        public Tape()
        {
            InitializeComponent();
        }

        #region Fields
            public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(Tape), new PropertyMetadata(new PropertyChangedCallback(UpdateTape)));
            public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(int), typeof(Tape), new PropertyMetadata(new PropertyChangedCallback(UpdateTape)));
            public static readonly DependencyProperty BlankProperty = DependencyProperty.Register("Blank", typeof(string), typeof(Tape), new PropertyMetadata(new PropertyChangedCallback(UpdateTape)));
        #endregion

        //Idee neu: Tape hat ein DP auf den Text als char[] und die Position und eine Methode (Event auf die DPs) regelt die Darstellung
        #region Properties
            public string Text
            {
                get { return (string)GetValue(TextProperty); }
                set { SetValue(TextProperty, value); }
            }

            public int Position
            {
                get { return (int)GetValue(PositionProperty); }
                set 
                { 
                    SetValue(PositionProperty, value);
                    UpdateTape(this, new DependencyPropertyChangedEventArgs());
                }
            }

            public string Blank
            {
                get { return (string)GetValue(BlankProperty); }
                set { SetValue(BlankProperty, value); }
            }
        #endregion

        #region Methods
            private void control_SizeChanged(object sender, SizeChangedEventArgs e)
            {
                if(e.WidthChanged)
                {
                    //Clear the StackPanel
                    spTape.Children.Clear();
                        
                    #region Calculate the correct amount of TextBoxes to be displayed in the grid
                        var __newItems = Convert.ToInt32(Math.Ceiling((e.NewSize.Width - 50) / 25));

                        //The number always has to be odd
                        __newItems = __newItems | 1;
                    #endregion

                    #region Create the new amount of TextBoxes
                        for (int i=0; i<__newItems; i++)
                        {
                            var __txt = new TextBox();
                            #region Set some TextBox-Properties
                                __txt.Style = (Style)FindResource("TapeField");
                                if (i == (int)Math.Floor(__newItems / 2.0))
                                    __txt.Style = (Style)FindResource("SelectedTapeField");
                            #endregion
                            spTape.Children.Add(__txt);
                        }
                    #endregion

                    //Call The UpdateTape()-Method if the Application is completely loaded
                        if (Text != null)
                            UpdateTape(this, new DependencyPropertyChangedEventArgs());
                }
            }

            private static void UpdateTape(DependencyObject sender, DependencyPropertyChangedEventArgs args)
            {
                var __this = ((Tape)sender);
                #region Cast the text to char[] and add the correct subset to the StackPanel
                    var __chars = __this.Text.ToCharArray();
                
                    //Get the number of the center TextBox
                    var __center = (int)Math.Floor(__this.spTape.Children.Count / 2.0);

                    for (int i = 0; i < __this.spTape.Children.Count; i++)
                    {
                        #region Try to cast each child to a TextBox and determine the character to assign
                            var __txt = __this.spTape.Children[i] as TextBox;
                            if(__txt != null)
                            {
                                /* How to find out what to assign (Example)
                                
                                Center:	16
                                            Diff      
                                Foo1:	 5	 11
                                Foo2:	33	-17

                                Pos: 		2
                                TextLength:	4

                                Left Side:	If Diff >= 0:	
		                                    && Diff > Pos -> Blank
		                                    && Diff <= Pos -> Chars[Pos - Diff]

                                Right Side:	If Diff < 0
		                                    && +(Diff) >= (Chars.Length - Pos) -> Blank
		                                    && +(Diff) < (Chars.Length - Pos) -> Chars[pos + (Chars.Length - Pos)]
                                */
                               
                                //Difference to the center
                                var __diff = __center - i;

                                if (__diff >= 0)
                                {
                                    #region Right
                                        if (__diff > __this.Position) { __txt.Text = __this.Blank; }
                                        else { __txt.Text = __chars[__this.Position - __diff].ToString(); }
                                    #endregion
                                }
                                else
                                {
                                    #region Left
                                        if ((__diff * -1) >= (__chars.Length - __this.Position)) { __txt.Text = __this.Blank; }
                                        else { __txt.Text = __chars[__this.Position + (__diff * -1)].ToString(); }
                                    #endregion
                                }

                            }
                        #endregion
                    }
                #endregion
            }

            private void cmdForward_Click(object sender, RoutedEventArgs e)
            {
                Position++;
            }

            private void cmdBack_Click(object sender, RoutedEventArgs e)
            {
                Position--;
            }
        #endregion
    }
}

