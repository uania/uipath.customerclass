using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA.UiPath.Classlib.Exceptions
{
    public class TerminalFlowException : Exception
    {
        /// <summary>
        /// uipath terminate reason
        /// </summary>
        public string Reason { get; set; }

        public TerminalFlowException()
        {

        }

        public TerminalFlowException(string message) : base(message)
        {

        }

        public TerminalFlowException(string message, string reason) : base(message)
        {
            Reason = reason;
        }

        public TerminalFlowException(string message, Exception inner) : base(message, inner)
        {

        }

        public TerminalFlowException(string message, string reason, Exception inner) : base(message, inner)
        {
            Reason = reason;
        }
    }
}
