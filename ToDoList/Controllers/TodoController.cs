using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly  ToDoListContext _context;
        public TodoController(ToDoListContext context)
        { 
            _context = context;    
        }
        public IActionResult Index()
        {
            IQueryable<ToDoListTable> ListAll = from i in _context.ToDoListTables orderby i.Id
                                                  select i;

            return View(ListAll);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ToDoListTable _toDoListTable)
        {
            if ( (_toDoListTable != null) && (ModelState.IsValid))
            {
                _context.ToDoListTables.Add(_toDoListTable);
                _context.SaveChanges();

                TempData["Success"] = "新增成功";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "新增失敗";
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)System.Net.HttpStatusCode.BadRequest);
            }

            ToDoListTable item = _context.ToDoListTables.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return View(item);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ToDoListTable toDoListTable)
        {
            if (ModelState.IsValid)
            {
                _context.ToDoListTables.Update(toDoListTable);
                _context.SaveChanges();

                TempData["Success"] = "修改成功";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "修改失敗";
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            ToDoListTable item = _context.ToDoListTables.Find(id);

            if (item == null)
            {
                TempData["Error"] = "此筆資料不存在";
            }
            else
            {
                _context.ToDoListTables.Remove(item);
                _context.SaveChanges();

                TempData["Success"] = "刪除成功";
            }
            return RedirectToAction("Index");
        }


    }
}
