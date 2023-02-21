using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DT191G_moment4_api.Models
{
    public class Song
    {

        //properties
        [Key]
        public int SongId { get; set; }
        [Required]
        public string? Artist { get; set; }
        [Required]
        public string? SongName { get; set; }
        public string? AlbumTitle { get; set; }
        public int? SongLength { get; set; } // song length in seconds

        [ForeignKey("Category")]
        public int? CategoryId { get; set; } // id of category
        public Category? Category { get; set; } // navigational property
   


    }
}
