﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.Data.Models
{
    public class Seat
    {
        public int Id { get; set; }

        [Required]
        public int HallId { get; set; }

        public Hall Hall { get; set; }
    }
}
