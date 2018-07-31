using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KSCApp.Models;

namespace KSCApp.ViewModels
{
    public class PlayerVM
    {
        [Display(Name ="Player ID No.")]
        public int Id { get; set; }

        [Display(Name = "Player Name")]
        public string PlayerName { get; set; }

        public int? Rank { get; set; }

        [EnumDataType(typeof(PlayerTypeEnum)), Display(Name = "Player Status")]
        public PlayerStatusEnum PlayerStatus { get; set; }

        [Display(Name = "Playing League")]
        public bool PlayingLeague { get; set; }

        [EnumDataType(typeof(PlayerTypeEnum)), Display(Name = "Player Type")]
        public PlayerTypeEnum PlayerType { get; set; }

        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }

        [Display(Name = "Profile Picture")]
        public byte[] ProfilePicture { get; set; }

        public string Email { get; set; }

        public int Level { get; set; }

        public int MatchesPlayed { get; set; }

        public int MatchesWon { get; set; }

        public int GamesWon { get; set; }

        public int GamesLost { get; set; }

    }
}
