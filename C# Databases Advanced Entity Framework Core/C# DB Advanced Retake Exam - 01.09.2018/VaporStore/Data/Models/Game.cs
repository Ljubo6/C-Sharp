﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaporStore.Data.Models
{
    public class Game
    {
        //•	Id – integer, Primary Key
        //•	Name – text(required)
        //•	Price – decimal (non-negative, minimum value: 0) (required)
        //•	ReleaseDate – Date(required)
        //•	DeveloperId – integer, foreign key(required)
        //•	Developer – the game’s developer(required)
        //•	GenreId – integer, foreign key(required)
        //•	Genre – the game’s genre(required)
        //•	Purchases - collection of type Purchase
        //•	GameTags - collection of type GameTag.Each game must have at least one tag.
        public Game()
        {
            this.Purchases = new HashSet<Purchase>();
            this.GameTags = new HashSet<GameTag>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0,double.MaxValue),Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [ForeignKey(nameof(Developer)),Required]
        public int DeveloperId { get; set; }
        public Developer Developer { get; set; }

        [ForeignKey(nameof(Genre)), Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
        public ICollection<GameTag> GameTags { get; set; }
    }
}
