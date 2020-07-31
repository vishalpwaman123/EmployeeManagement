
namespace SimpleApplication
{
    using Experimental.System.Messaging;
    using System;

    /// <summary>
    /// Define sender class 
    /// </summary>
    public class Sender
    {
        /// <summary>
        /// Define send method
        /// </summary>
        /// <param name="input">Passing input string</param>
        public void Send(string email, string token)
        {
            try
            {
                // Created the referrence of MessageQueue
                MessageQueue messageQueue = null;

                // Check if Message Queue Exists
                if (MessageQueue.Exists(@".\Private$\messagequeue"))
                {
                    messageQueue = new MessageQueue(@".\Private$\messagequeue");
                    messageQueue.Label = "Testing Queue";
                }
                else
                {
                    MessageQueue.Create(@".\Private$\messagequeue");
                    messageQueue = new MessageQueue(@".\Private$\messagequeue");
                    messageQueue.Label = "Newly Created Queue";
                }

                // Message send to Queue
                messageQueue.Send(email, token);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Declare Senders method
        /// </summary>
        /// <param name="input">Passing input string</param>
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

        /// <summary>
        /// Declare receivers method
        /// </summary>
        /// <returns>Return string value</returns>
        public string Receivers()
        {
            MessageQueue messageQueue;
            messageQueue = new MessageQueue(@".\Private$\messageq");

            Message message = messageQueue.Receive();
            message.Formatter = new BinaryMessageFormatter();
            Senders(message.Body.ToString());
            return message.Body.ToString(); ;
            
        }

        /// <summary>
        /// Declare Clears method
        /// </summary>
        public void clears()
        {
            MessageQueue messageQueue;
            messageQueue = new MessageQueue(@".\Private$\messageq");
            messageQueue.Purge();
        }
    }
}
