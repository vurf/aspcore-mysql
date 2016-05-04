using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WebApplication3.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication3.Controllers
{
    public class PostController : Controller
    {
        readonly DataContext _context;
        public PostController(DataContext context)
        {
            _context = context;
        }

        
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_context.Posts);
        }

        public IActionResult Details(int id = 0)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(PostEntity entity)
        {
            if (ModelState.IsValid)
            {
                _context.Posts.Add(entity);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public IActionResult Edit(int id = 0)
        {
            var entity = _context.Posts.Find(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(PostEntity entity)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public IActionResult Delete(int id = 0)
        {
            var entity = _context.Posts.Find(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id = 0)
        {
            var entity = _context.Posts.Find(id);
            _context.Posts.Remove(entity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
