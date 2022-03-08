using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using LibApp.Interfaces;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using LibApp.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private IMapper _mapper;
        
        public BooksController(IGenreRepository genreRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var books = _bookRepository.GetBooks()
                .ToList();

            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _bookRepository.GetBooks()
                .SingleOrDefault(b => b.Id == id);

            return View(book);
        }
        
        public IActionResult Edit(int id)
        {
            var book = _bookRepository.Get(id);
            if (book == null) 
            {
                return NotFound();
            }

            var viewModel = new BookFormViewModel
            {
                Book = book,
                Genres = _genreRepository.GetGenres()
            };

            return View("BookForm", viewModel);
        }

        public IActionResult New()
        {
            var viewModel = new BookFormViewModel
            {
                Genres = _genreRepository.GetGenres()
            };

            return View("BookForm", viewModel);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Save(Book book)
        {
            if (book.Id == 0)
            {
                book.DateAdded = DateTime.Now;
                _bookRepository.Add(book);
            }
            else
            {
                var bookInDb = _bookRepository.Get(book.Id);
                bookInDb.Name = book.Name;
                bookInDb.AuthorName = book.AuthorName;
                bookInDb.GenreId = book.GenreId;
                bookInDb.ReleaseDate = book.ReleaseDate;
                bookInDb.DateAdded = book.DateAdded;
                bookInDb.NumberInStock= book.NumberInStock;
            }

            try
            {
                _bookRepository.Save();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Books");
        }
        
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Authorize]
        [Microsoft.AspNetCore.Mvc.Route("api/books")]
        public IList<Book> GetBooks()
        {
            return _bookRepository.GetBooks().ToList();
        }
        
        [Microsoft.AspNetCore.Mvc.HttpDelete]
        [Authorize(Roles = "StoreManager, User")]
        [Microsoft.AspNetCore.Mvc.Route("api/books/{id}")]
        public Book DeleteBook(int id)
        {
            Book book = _bookRepository.Get(id);
            if (book != null)
            {
                _bookRepository.Remove(id);
                _bookRepository.Save();
                return book;
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
        
        [Microsoft.AspNetCore.Mvc.HttpPut]
        [Authorize(Roles = "StoreManager, User")]
        [Microsoft.AspNetCore.Mvc.Route("api/books/{id}")]
        public void Update(int id, BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var book = _bookRepository.Get(id);
            if (book == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            _mapper.Map(bookDto, book);

            _bookRepository.Save();
        }
        
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Authorize(Roles = "StoreManager, User")]
        [Microsoft.AspNetCore.Mvc.Route("api/books")]
        public Book Add(Book bookDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);
            }
            var book = _mapper.Map<Book>(bookDto);
            _bookRepository.Add(book);
            _bookRepository.Save();
            bookDto.Id = book.Id;

            return book;
        }
        

    }
}
