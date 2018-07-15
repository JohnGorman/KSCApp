using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSCApp.Models
{
    public enum PlayerTypeEnum { Senior = 1, Junior }

    public enum PlayerStatusEnum { Active = 1, Injured, Retired, NonMember}

    public partial class Player
    {
        public Player()
        {
            //MatchesPlayerA = new HashSet<Match>();
            //MatchesPlayerB = new HashSet<Match>();
            RankHistory = new HashSet<RankHistory>();
            TeamPlayers = new HashSet<TeamPlayer>();
            //KSCAppUsers = new HashSet<KSCAppUser>();
            this.PlayingLeague = true;
        }

        public int PlayerId { get; set; }
        [Required, Display(Name ="Player Name")]
        public string PlayerName { get; set; }

        public int? Rank { get; set; }

        [EnumDataType(typeof(PlayerStatusEnum)), Display(Name ="Player Status")]
        public PlayerStatusEnum PlayerStatus { get; set; }

        [Required, Display(Name = "Playing League")]
        public bool PlayingLeague { get; set; }

        [EnumDataType(typeof(PlayerTypeEnum)), Display(Name = "Player Type")]
        public PlayerTypeEnum PlayerType { get; set; }

        [Required, Display(Name ="Contact No")]
        public string ContactNo { get; set; }

        [Display(Name = "Profile Picture")]
        public byte[] ProfilePicture { get; set; }

        public string UserId { get; set; }
        public virtual KSCAppUser KSCAppUser { get; set; }

        //public ICollection<Match> MatchesPlayerA { get; set; }
        //public ICollection<Match> MatchesPlayerB { get; set; }
        public ICollection<RankHistory> RankHistory { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; }
        //public ICollection<KSCAppUser> KSCAppUsers { get; set; }
    }
}
