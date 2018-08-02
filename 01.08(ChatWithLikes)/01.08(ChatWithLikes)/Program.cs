using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ChatWithLikes
{
    class Program
    {
        static void ShowInOrder(Dictionary<int, Message> messages, Dictionary<int, User> users)
        {
            foreach (var message in messages.Values.OrderBy(m => m.Time))
            {
                if (message.Answer_to_id == 0)
                {
                    Console.WriteLine($"\n{users[message.User_id].Username}:\n{message.Text}\n{message.Time.ToLongTimeString()} \t {(message.Mark > 0  ? '+' : '\0')}{message.Mark}\n");
                }
                else
                {
                    if (messages.Keys.Contains(message.Answer_to_id))
                    {
                        var answeredMessage = messages[message.Answer_to_id];
                        Console.WriteLine($"\n{users[message.User_id].Username}:\n\t|{users[answeredMessage.User_id].Username}:\n\t|{answeredMessage.Text}\n{message.Text}\n{message.Time.ToLongTimeString()} \t {(message.Mark > 0 ? '+' : '\0')}{message.Mark}\n");
                    }
                    else
                    {
                        Console.WriteLine($"\n{users[message.User_id].Username}:\n\t|DELETED MESSAGE\n{message.Text}\n{message.Time.ToLongTimeString()} \t {(message.Mark > 0 ? '+' : '\0')}{message.Mark}\n");
                    }
                }

            }
        }
        static void Main(string[] args)
        {
            var messages = new Dictionary<int,Message>();
            var users = new Dictionary<int, User>();

            using (var repository = new Repository())
            {
                messages = repository.GetMessages();
                users = repository.GetUsers();
            }
            ShowInOrder(messages, users);
        }
    }
}
