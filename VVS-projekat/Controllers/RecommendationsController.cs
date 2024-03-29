﻿using System;
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
            catch (ArgumentException ex) //Metoda RecommendationsController.Index() hvata ArgumentException, dok GetAllBooksAsync() baca ArgumentNullException.
            {               
                //Ako nema knjiga u sistemu napiši na ekran
                ViewBag.EmptyList = "Nema knjiga u sistemu!";
            }

            return View(books);
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Book.ToListAsync();
        }
        //Dodati komentare u kod radi bolje čitljivosti i održavanja koda.
        
        
        //Vraća listu najboljih knjiga u sistemu
        public List<Book> GetBestBooks(List<Book> allBooks)
        {
            if (allBooks.Count == 0) throw new ArgumentException("Prazna lista knjiga");
            
            // Izračunaj prosjek ratinga za svaku knjigu
            var booksWithAverageRatings = CalculateAverageRatings(allBooks);
            
            // Sortiraj knjige po prosječnom ratingu
            var sortedBooks = booksWithAverageRatings.OrderByDescending(b => b.AverageRating)
                    .ToList();

            // Izdvoji najbolje knjige
            var topCount = (int)(sortedBooks.Count*0.3); //U metodi GetBestBooks, potrebno je pomnožiti sortedBooks.Count sa 0.3 kako bi se dobile samo najbolje 30% knjiga.
            var topBooks = sortedBooks.Take(topCount).ToList();

            //Dio koda treba izdvojiti iz metode GetBestBooks i staviti u zasebnu metodu GetBooksFromBooksWithRating.
            return GetBooksFromBooksWithRating(topBooks);
        }
        //Dodati komentare u kod radi bolje čitljivosti i održavanja koda.

        //Računa prosjeke ratinga za sve knjige i vraća listu objekata BookWithAverageRating
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

        //Pretvara listu objekata tipa BookWithAverageRating u listu Knjiga
        private List<Book> GetBooksFromBooksWithRating(List<BookWithAverageRating> booksWithRating)
        {
            var books = new List<Book>();
            foreach (var bookWithRating in booksWithRating)
            {
                books.Add(bookWithRating.Book);

            }
            return books;
        }

        //Objekat koji sadrži knjige, i odgovarajući prosječan rating za svaku
        public class BookWithAverageRating
        {
            public Book Book { get; set; }
            public double AverageRating { get; set; }


        }


    }
}