﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorChatApp4.Shared
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public string Sentiment { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
