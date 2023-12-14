using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VVS_projekat.Data;
using VVS_projekat.Models;

namespace VVS_projekat.Controllers
{
    public class RecommendationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecommendationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recommendations

        public async Task<IActionResult> Index()
        {
            var allBooks = await GetAllBooksAsync();

            var books = GetBestBooks(allBooks);

            // Return the top 30% books as recommendations
            return View(books);
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Book.ToListAsync();
        }

        public List<BookWithAverageRating> CalculateAverageRatings(List<Book> books)
        {
            var booksWithAverageRatings = new List<BookWithAverageRating>();

            foreach (var book in books)
            {
                var ratings = _context.Rating.Include(r => r.Book);
                double avgRating = ratings.Average(r => r.RatingValue);
                
                booksWithAverageRatings.Add(new BookWithAverageRating
                {
                    Book = book,
                    AverageRating = avgRating,
                    
                });
            }

            return booksWithAverageRatings;
        }

        private List<Book> GetBestBooks(List<Book> allBooks)
        {
            // Step 1: Calculate Average Ratings for Each Book
            var booksWithAverageRatings = CalculateAverageRatings(allBooks);

            // Step 2: Sort Books by Average Rating
            var sortedBooks = booksWithAverageRatings.OrderByDescending(b => b.AverageRating)
                    .ToList();


            // Step 3: Select the Top 30%
            var topCount = (int)(sortedBooks.Count * 0.3);
            var topBooks = sortedBooks.Take(topCount).ToList();

            return GetBooksFromBooksWithRating(topBooks);
        }

        private List<Book> GetBooksFromBooksWithRating(List<BookWithAverageRating> booksWithRating)
        {
            var books = new List<Book>();
            foreach(var bookWithRating in booksWithRating)
            {
                books.Add(bookWithRating.Book);

            }
            return books;
        }

 
    

    public class BookWithAverageRating
    {
        public Book Book { get; set; }
        public double AverageRating { get; set; }
        public string? Comment { get; set; }

        }


    }
}
