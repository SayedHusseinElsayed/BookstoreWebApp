using Bookstore.Models.Repo;
using Bookstore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using Microsoft.AspNetCore.Http.HttpResults;


namespace Bookstore.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookstoreRepo<Book> bookRepo;
        private readonly IBookstoreRepo<Author> authorRepo;
        [System.Obsolete]
        private readonly IHostingEnvironment hosting;

        [System.Obsolete]
        public BooksController(IBookstoreRepo<Book> bookRepo, IBookstoreRepo<Author> authorRepo, IHostingEnvironment hosting)
        {
            this.bookRepo = bookRepo;
            this.authorRepo = authorRepo;
            this.hosting = hosting;
        }
        // GET: BooksController
        public ActionResult Index()
        {
            var books = bookRepo.list();
            var authors = authorRepo.list();
            return View(books);
        }

        // GET: BooksController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepo.Find(id);
            return View(book);
        }

        // GET: BooksController/Create
        public ActionResult Create()
        {

            var model = new BookAuthorViewModel
            {
                //Authors = authorRepo.list().ToList()  replaced with below creatde method
                Authors = FillSelectList()

            };

            return View(model);
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [System.Obsolete]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = string.Empty;
                    if (model.File != null)
                    {
                        string upload = Path.Combine(hosting.WebRootPath, "Uploads");
                        fileName = model.File.FileName;
                        if (fileName==null)
                        {
                            fileName = "default.png";
                        }
                        string fullPath = Path.Combine(upload, fileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }

                    if (model.AuthorId == -1)
                    {

                        ViewBag.Message = "Please select an author from the list!";
                        var vmodel = new BookAuthorViewModel
                        {
                            //Authors = authorRepo.list().ToList()  replaced with below creatde method
                            Authors = FillSelectList()

                        };
                        return View(vmodel);
                    }

                    Book book = new Book
                    {
                        Id = model.BookID,
                        Title = model.Title,
                        Description = model.Desciption,
                        Author = authorRepo.Find(model.AuthorId),
                        imageUrl = fileName,
                    };
                    bookRepo.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            var bmodel = new BookAuthorViewModel
            {
                //Authors = authorRepo.list().ToList()  replaced with below creatde method
                Authors = FillSelectList()

            };

            ModelState.AddModelError("", "You have to fill all required fields and check the validation");
            return View(bmodel);

        }

        // GET: BooksController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepo.Find(id);

            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;

            var viewModel = new BookAuthorViewModel
            {
                BookID = book.Id,
                Title = book.Title,
                Desciption = book.Description,
                AuthorId = authorId,
                Authors = authorRepo.list().ToList(),
                ImageUrl = book.imageUrl

            };
            
            return View(viewModel);
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [System.Obsolete]
        public ActionResult Edit(BookAuthorViewModel model)
        {

            string fileName = string.Empty;
            if (model.File != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                fileName = model.File.FileName;
                string fullPath = Path.Combine(uploads, fileName);

                string oldFileName = model.ImageUrl;
                if (oldFileName==null)
                {
                    oldFileName = "default.png";
                }
                string fullOldPath = Path.Combine(uploads, oldFileName);

                if (fullPath != fullOldPath)
                {
                    System.IO.File.Delete(fullOldPath);
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

            }
            var author = authorRepo.Find(model.AuthorId);
            Book book = new Book
            {

                Id = model.BookID,
                Title = model.Title,
                Description = model.Desciption,
                Author = author,
                imageUrl = fileName
            };

            bookRepo.Update(model.BookID, book);

            return RedirectToAction("Index");

        }
          
  

        // GET: BooksController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepo.Find(id);
            return View(book);
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Search(string term)
        {
            if (term !=null)
            {
                var result = bookRepo.Search(term);
                return View("Index", result);
            }
            return RedirectToAction(nameof(Index));
        }

        List<Author> FillSelectList()
        { 
           var authors = authorRepo.list().ToList();
           authors.Insert(0, new Author { Id = -1, Fullname = "----Please select an Author----" });
           return authors;

        }

        [Obsolete]
        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));

                return file.FileName;
            }

            return null;
        }

        [Obsolete]
        string UploadFile(IFormFile file, string imageUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");

                string newPath = Path.Combine(uploads, file.FileName);
                string oldPath = Path.Combine(uploads, imageUrl);

                if (oldPath != newPath)
                {
                    System.IO.File.Delete(oldPath);
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }

                return file.FileName;
            }

            return imageUrl;
        }

        
    }
}
