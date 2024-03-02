using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MessengerApp.Models
{
    public class ChatHub : Hub
    {
        private AppDbContext _dbContext;

        public ChatHub(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}

        public async Task SendMessage(string user, string message)
        {
            var chatMessage = new Message
            {
                User = user,
                Content = message,
                Timestamp = DateTime.UtcNow
            };

            _dbContext.Messages.Add(chatMessage);
            await _dbContext.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

    }
}
