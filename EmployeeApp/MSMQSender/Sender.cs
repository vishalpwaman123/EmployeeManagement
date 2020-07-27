
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

        public void Senders(string input)
        {
            MessageQueue messageQ;

            if (MessageQueue.Exists(@".\Private$\messageq"))
            {
                messageQ = new MessageQueue(@".\Private$\messageq");
            }
            else
            {
                messageQ = MessageQueue.Create(@".\Private$\messageq");
            }

            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();
            message.Body = input;
            message.Label = "ResetPassword";
            message.Priority = MessagePriority.Normal;
            messageQ.Send(message);
        }

        public string Receivers()
        {
            MessageQueue messageQueue;
            messageQueue = new MessageQueue(@".\Private$\messageq");

            Message message = messageQueue.Receive();
            message.Formatter = new BinaryMessageFormatter();
            Senders(message.Body.ToString());
            return message.Body.ToString(); ;
            
        }

        /*public string ReceiversQueue()
        {
            MessageQueue messageQueue;
            messageQueue = new MessageQueue(@".\Private$\messageq");

            Message message = messageQueue.Receive();
            message.Formatter = new BinaryMessageFormatter();
            Senders(message.Body.ToString());
            return message.Body.ToString(); ;

        }*/

        public void clears()
        {
            MessageQueue messageQueue;
            messageQueue = new MessageQueue(@".\Private$\messageq");
            messageQueue.Purge();
        }
    }
}
