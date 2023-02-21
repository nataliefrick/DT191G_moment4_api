using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DT191G_moment4_api.Models
{
    public class SongList
    {

        //properties


        public int SongId { get; set; }
        public string? Artist { get; set; }
        public string? SongName { get; set; }
        public string? AlbumTitle { get; set; }
        public int? SongLength { get; set; } // song length in seconds

        public string? Category { get; set; }
   


    }
}
