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

        public async Task<IActionResult> Index()
        {
            var allBooks = await GetAllBooksAsync();
            var books = new List<Book>();
            try
            {
                books = GetBestBooks(allBooks);
            }
            catch (ArgumentException ex)
            {
                ViewBag.EmptyList = "Nema knjiga u sistemu!";
            }

            return View(books);
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Book.ToListAsync();
        }

        public List<Book> GetBestBooks(List<Book> allBooks)
        {
            if (allBooks.Count == 0) throw new ArgumentNullException("Prazna lista knjiga");

            var booksWithAverageRatings = CalculateAverageRatings(allBooks);

            var sortedBooks = booksWithAverageRatings.OrderByDescending(b => b.AverageRating)
                    .ToList();


            var topCount = (int)(sortedBooks.Count);
            var topBooks = sortedBooks.Take(topCount).ToList();

            var books = new List<Book>();
            foreach (var bookWithRating in topBooks)
            {
                books.Add(bookWithRating.Book);

            }

            return books;
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



        //Objekat koji sadrži knjige, i odgovarajući prosječan rating za svaku
        public class BookWithAverageRating
        {
            public Book Book { get; set; }
            public double AverageRating { get; set; }


        }


    }
}