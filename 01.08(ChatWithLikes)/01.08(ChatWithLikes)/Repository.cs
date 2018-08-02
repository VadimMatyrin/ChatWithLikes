using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ChatWithLikes
{
    public class Repository : IDisposable
    {
        const string conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Учёба\warehouse.mdf;Integrated Security=True;Connect Timeout=30";

        SqlConnection _con;

        public Repository()
        {
            _con = new SqlConnection(conStr);
        }

        public void Dispose()
        {
            _con.Dispose();
        }

        public Dictionary<int, Message> GetMessages()
        {
            string commandString = "SELECT * FROM [Messages]";

            SqlCommand command = new SqlCommand(commandString, _con);

            var result = new Dictionary<int, Message>();

            _con.Open();

            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                {
                    result[(int)reader["message_id"]] = new Message(
                        (int)reader["message_id"],
                        (int)reader["user_id"],
                        (string)reader["text"],
                        (DateTime)reader["time"],
                        Convert.ToInt32(reader["mark"]),
                        reader["answer_to"] == DBNull.Value ? 0 : (int)reader["answer_to"]
                        );

                }

            }

            _con.Close();

            return result;
        }
        public Dictionary<int, User> GetUsers()
        {
            string commandString = "SELECT * FROM [ChatUsers]";

            SqlCommand command = new SqlCommand(commandString, _con);

            var result = new Dictionary<int, User>();

            _con.Open();

            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                {
                    result[(int)reader["user_id"]] = new User(
                        (int)reader["user_id"],
                        (string)reader["username"]
                        );

                }

            }

            _con.Close();

            return result;
        }
        public List<MessageMark> GetMessageMarks()
        {
            string commandString = "SELECT * FROM [MessageMark]";

            SqlCommand command = new SqlCommand(commandString, _con);

            var result = new List<MessageMark>();

            _con.Open();

            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                {
                    result.Add(new MessageMark(
                        (int)reader["message_id"],
                        (int)reader["user_id"],
                        (int)reader["mark"]
                        ));

                }

            }

            _con.Close();

            return result;
        }
        public void CreateMessage(Message message)
        {
            string sql = @"INSERT INTO [Messages] ([user_id], [text], [answer_to], [time])
                        VALUES (@user_id, @text, @answer_to, @time) ";

            SqlCommand command = new SqlCommand(sql, _con);
            command.Parameters.AddWithValue("@user_id", message.User_id);
            command.Parameters.AddWithValue("@text", message.Text);
            command.Parameters.AddWithValue("@answer_to", message.Answer_to_id);
            command.Parameters.AddWithValue("@time", message.Time);


            _con.Open();

            command.ExecuteNonQuery();

            _con.Close();
        }
        public void CreateMessageMark(MessageMark mark)
        {
            string sql = @"INSERT INTO [MessagesMarks] ([message_id], [user_id], [mark])
                        VALUES (@message_id, @user_id, @mark) ";

            SqlCommand command = new SqlCommand(sql, _con);
            command.Parameters.AddWithValue("@message_id", mark.Message_to_id);
            command.Parameters.AddWithValue("@user_id", mark.User_id);
            command.Parameters.AddWithValue("@mark", mark.Mark);

            _con.Open();

            command.ExecuteNonQuery();

            _con.Close();
        }
    }
}
