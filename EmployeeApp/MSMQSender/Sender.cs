
namespace SimpleApplication
{
    using Experimental.System.Messaging;
    
    /// <summary>
    /// Define sender class 
    /// </summary>
    public class Sender
    {
        /// <summary>
        /// Define send method
        /// </summary>
        /// <param name="input">Passing input string</param>
        public void Send(string input)
        {
            /// <summary>
            /// Define message queue variable
            /// </summary>
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
