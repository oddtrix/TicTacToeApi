﻿using ApplicationCore.Enums;

namespace Domain.DTOs.Game
{
    public class GameUpdateDTO : BaseDTO
    {
        public GameStatus GameStatus { get; set; }

        public bool isPrivate { get; set; }

        public Entities.Player Winner { get; set; }

        public Guid ChatId { get; set; }

        public Guid FieldId { get; set; }
    }
}
