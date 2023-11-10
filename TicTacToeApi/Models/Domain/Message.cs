﻿namespace TicTacToeApi.Models.Domain
{
    public class Message
    {
        public Guid Id { get; set; }

        public string messageBody { get; set; }

        public DateTime dateTime { get; set; }

        public Guid ChatId { get; set; }

        public Chat Chat { get; set; }

        public Guid PlayerId { get; set; }

        public Player Player { get; set; }
    }
}
