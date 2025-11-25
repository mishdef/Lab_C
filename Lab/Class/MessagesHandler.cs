using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Class
{
    internal static class MessagesHandler
    {
        public delegate void InfoMessage(string message);
        static Action<string>? WarningMessage;
        public delegate void ErrorMessage(string message);

        static InfoMessage? infoMessage;
        static ErrorMessage? errorMessage;

        static public void SendInfoMessage(string message) {
            if (!(infoMessage == null)) infoMessage.Invoke(message);
            else throw new Exception("Please configure message handlers.");
        }
        static public void SendWarningMessage(string message)
        {
            if (!(WarningMessage == null)) WarningMessage.Invoke(message);
            else throw new Exception("Please configure message handlers.");
        }
        static public void SendErrorMessage(string message)
        {
            if (!(errorMessage == null)) errorMessage.Invoke(message);
            else throw new Exception("Please configure message handlers.");
        }

        static public void SetInfoMessage(InfoMessage message) => infoMessage = message;
        static public void SetWarningMessage(Action<string> message) => WarningMessage = message;
        static public void SetErrorMessage(ErrorMessage message) => errorMessage = message;
    }
}
