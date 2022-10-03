namespace CarRentalSystem
{
    public class MessageQueueSettings
    {
        public MessageQueueSettings(string host, string username, string password)
        {
            this.Host = host;
            this.Username = username;
            this.Password = password;
        }

        public string Host { get; }

        public string Username { get; }

        public string Password { get; }
    }
}
