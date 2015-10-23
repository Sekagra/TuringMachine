using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace TuringMachine.Model
{
    public static class Extensions
    {
        #region XML to machine
            /// <summary>
            /// Recreates a complete Turing machine-Object out of a XML-file.
            /// </summary>
            /// <param name="xdoc">The XML as XDocument.</param>
            /// <returns>The new machine with the recreated settings, state and instructions.</returns>
            public static Machine ToMachine(this XDocument xdoc)
            {
                #region Create the machine with its basic properties
                    var __m = new Machine(CurrentApp.Instance.Messages)
                    {
                        Tape = xdoc.Root.Descendants("text").Single().Value,
                        Blank = xdoc.Root.Descendants("blank").Single().Value[0],
                        Position = Convert.ToInt32(xdoc.Root.Descendants("position").Single().Value),
                        State = Convert.ToInt32(xdoc.Root.Descendants("state").Single().Value)
                    };
                #endregion

                #region Build the list of instructions
                    __m.Instructions = new ObservableCollection<Instruction>();
                    foreach (var __instruct in xdoc.Root.Descendants("instructions").Single().Descendants("instruction"))
                    {
                        __m.Instructions.Add(__instruct.ToInstruction());
                    }
                #endregion

                return __m;
            }

            /// <summary>
            /// Creates a single instruction out of an <instruction /> element
            /// </summary>
            /// <param name="xelm">The instruction as XElement</param>
            /// <returns>The new instruction.</returns>
            public static Instruction ToInstruction(this XElement xelm)
            {
                return new Instruction()
                {
                    State = Convert.ToInt32(xelm.Attribute("state").Value),
                    Read = xelm.Attribute("read").Value[0],
                    Write = xelm.Attribute("write").Value[0],
                    Direction = (Direction)Convert.ToInt32(xelm.Attribute("direction").Value),
                    NewState = Convert.ToInt32(xelm.Attribute("newstate").Value)
                };
            }
        #endregion

        #region Machine to XML
            /// <summary>
            /// Stores all values of a Turing machine-object in a XDocument in order to save it later on.
            /// </summary>
            /// <param name="machine">The machine to get the XML from.</param>
            /// <returns></returns>
            public static XDocument ToXML(this Machine machine)
            {
                #region Get the instructions first
                    var __instructions = new XElement("instructions");
                    foreach(var __instruct in machine.Instructions)
                    {
                        __instructions.Add(__instruct.ToXML());
                    }
                #endregion
                
                return new XDocument(new XElement("machine",
                    new XElement("text", machine.Tape),
                    new XElement("blank", machine.Blank),
                    new XElement("position", machine.Position),
                    new XElement("state", machine.State),
                    new XElement(__instructions)));
            }

            /// <summary>
            /// Creats the a XElement out of a single instruction.
            /// </summary>
            /// <param name="instruction">The instruction to transform.</param>
            /// <returns>XElement with all values as attributes</returns>
            public static XElement ToXML(this Instruction instruction)
            {
                return new XElement("instruction",
                    new XAttribute("state", instruction.State),
                    new XAttribute("read", instruction.Read),
                    new XAttribute("write", instruction.Write),
                    new XAttribute("direction", instruction.Direction),
                    new XAttribute("newstate", instruction.NewState));
            }
        #endregion
    }
}
