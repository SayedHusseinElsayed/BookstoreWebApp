using Bookstore.Models;
using Bookstore.Models.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IBookstoreRepo<Author> authorRepo;
        public AuthorsController(IBookstoreRepo<Author> authorRepo)
        {
            this.authorRepo = authorRepo;
        }
        // GET: AuthorsController
        public ActionResult Index()
        {
            var authors = authorRepo.list();
            return View(authors);
        }

        // GET: AuthorsController/Details/5
        public ActionResult Details(int id)
        {
            var author = authorRepo.Find(id);
            return View(author);
        }

        // GET: AuthorsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    authorRepo.Add(author);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError("", "You have to fill all required fields and check the validation");
            return View();
        }

        // GET: AuthorsController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = authorRepo.Find(id);
            return View(author);
        }

        // POST: AuthorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            try
            {
                authorRepo.Update(id, author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorsController/Delete/5
        public ActionResult Delete(int id)
        {
            var author = authorRepo.Find(id);
            return View(author);
        }

        // POST: AuthorsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                authorRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
