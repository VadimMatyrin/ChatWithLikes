using System;
using System.Collections.Generic;
using System.Text;

namespace ChatWithLikes
{
    public class Message
    {
        public int Message_id { get; private set; }
        public int User_id { get; private set; }
        public string Text { get; private set; }
        public int Answer_to_id { get; private set; }
        public DateTime Time { get; private set; }
        public int Mark { get; private set; }

        public Message(int message_id, int user_id, string text, DateTime time, int mark, int answer_to_id = 0)
        {
            Message_id = message_id;
            User_id = user_id;
            Text = text;
            Answer_to_id = answer_to_id;
            Time = time;
            Mark = mark;
        }
    }
}
