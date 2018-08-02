using System;
using System.Collections.Generic;
using System.Text;

namespace ChatWithLikes
{
    public class MessageMark
    {
        public int Message_to_id { get; private set; }
        public int User_id { get; private set; }
        public int Mark { get; private set; }

        public MessageMark(int message_to_id, int user_id, int mark)
        {
            if (mark != 1 || mark != -1)
                throw new ArgumentException("Incorrect mark");

            Message_to_id = message_to_id;
            User_id = user_id;
            Mark = mark;
        }
    }
}
