using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DT191G_moment4_api.Data;
using DT191G_moment4_api.Models;

namespace DT191G_moment4_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly SongListContext _context;

        public SongController(SongListContext context)
        {
            _context = context;
        }

        // GET All: api/Song
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongList>>> GetSong()
        {
          if (_context.Song == null)
          {
              return NotFound();
          }
            //var songList = _context.Song.Include(c => c.Category);
            
          // Query:
            // SELECT s.Artist, s.SongName, s.AlbumTitle, s.SongLength, c.CategoryName FROM `song` s
            // LEFT JOIN `category` c on s.CategoryId = c.CategoryId;

            // Working with multiple tables using LINQ Join
            var songList = (from s in _context.Song
                           join c in _context.Category
                           on s.CategoryId equals c.CategoryId
                           select new SongList()
                           {
                               SongId = s.SongId,
                               SongName = s.SongName,
                               Artist = s.Artist,
                               AlbumTitle = s.AlbumTitle,
                               SongLength = s.SongLength,
                               Category = c.CategoryName
                           });


            return await songList.ToListAsync();
        }

        //private int? convertToTime(int? songLength)
        //{
        //    var minutes = (songLength / 60).ToString();
        //    var seconds = (songLength % 60).ToString();
        //    var length = minutes + ":" + seconds;

        //    //return length;
        //    //throw new NotImplementedException();
        //}

        // GET One: api/Song/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
            if (_context.Song == null)
            {
                return NotFound();
            }

            //var song = await _context.Song.FindAsync(id);  // could not add join into this statement
            var song = await _context.Song.Include(c => c.Category).FirstOrDefaultAsync(c => c.SongId == id);


            if (song == null)
            {
                return NotFound();
            }

            //var category = await _context.Category.FindAsync(song.CategoryId);
            


            return song;
        }

        // PUT: api/Song/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, Song song)
        {
            if (id != song.SongId)
            {
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Song
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
          if (_context.Song == null)
          {
              return Problem("Entity set 'SongContext.Song'  is null.");
          }
            _context.Song.Add(song);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSong", new { id = song.SongId }, song);
        }

        // DELETE: api/Song/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            if (_context.Song == null)
            {
                return NotFound();
            }
            var song = await _context.Song.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Song.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(int id)
        {
            return (_context.Song?.Any(e => e.SongId == id)).GetValueOrDefault();
        }
    }
}
