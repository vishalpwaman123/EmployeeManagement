
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Experimental.System.Messaging;
//using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace SimpleApplication
{
    public class Sender
    {
        public void Send(string input)
        {
            MessageQueue messageQ;

            if (MessageQueue.Exists(@".\Private$\messagequeue"))
            {
                messageQ = new MessageQueue(@".\Private$\messagequeue");
            }
            else
            {
                messageQ = MessageQueue.Create(@".\Private$\messagequeue");
            }

            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();
            message.Body = input;
            message.Label = "Registration";
            message.Priority = MessagePriority.Normal;
            messageQ.Send(message);
        }
    }
}
