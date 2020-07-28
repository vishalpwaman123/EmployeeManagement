
namespace MSMQ
{
    using Experimental.System.Messaging;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Declare message listener class
    /// </summary>
    public class MessageListener
    {
        /// <summary>
        /// define main class
        /// </summary>
        /// <param name="args"></param>
       public static void Main(string[] args)
        {
            Console.WriteLine("Message");

            //Define Message queue variable
            MessageQueue messageQueue;

            messageQueue = new MessageQueue(@".\Private$\messagequeue");
            
            Message message = messageQueue.Receive();
           
            message.Formatter = new BinaryMessageFormatter();

            Console.WriteLine(message.Body.ToString());
            
            Console.ReadLine();
        }
    }
}
