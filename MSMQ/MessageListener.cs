using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSMQ
{
    public class MessageListener
    {
       public static void Main(string[] args)
        {
            Console.WriteLine("Message");

            MessageQueue messageQueue;
            messageQueue = new MessageQueue(@".\Private$\messagequeue");

            Message message = messageQueue.Receive();
            message.Formatter = new BinaryMessageFormatter();

            Console.WriteLine(message.Body.ToString());
            Console.ReadLine();
        }
    }
}
