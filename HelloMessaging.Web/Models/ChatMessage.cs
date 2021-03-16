using System;

namespace HelloMessaging.Web.Models
{
    public class ChatMessage
    {
        public string Message { get; set; }
        public string User { get; set; }
        public DateTime DateTime { get; set; }

        public ChatMessage()
        {
            DateTime = DateTime.Now;
        }
    }
}