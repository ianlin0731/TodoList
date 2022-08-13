using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListAsync.Models;

namespace ToDoListAsync.Controllers
{
    public class TodoController : Controller
    {
        private readonly ToDoListContext _context;

        public TodoController(ToDoListContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var item = from i in _context.ToDoListTables 
                                select i;

            if(item == null)
            {
                return NotFound();
            }
            else
            {
                return View(await item.ToListAsync());
            }   
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoListTable _toDoListTable)
        {
            if ((_toDoListTable != null) && (ModelState.IsValid))
            {
                _context.ToDoListTables.Add(_toDoListTable);
                await _context.SaveChangesAsync();

                TempData["Success"] = "新增成功";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "新增失敗";
            }
            return View();
        }

        public IActionResult Edit(int? id)
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
        public async Task<IActionResult> Edit(ToDoListTable toDoListTable)
        {
            if (ModelState.IsValid)
            {
                _context.ToDoListTables.Update(toDoListTable);
                await _context.SaveChangesAsync();

                TempData["Success"] = "修改成功";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "修改失敗";
            }
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            ToDoListTable item = await _context.ToDoListTables.FindAsync(id);

            if (item == null)
            {
                TempData["Error"] = "此筆資料不存在";
            }
            else
            {
                _context.ToDoListTables.Remove(item);
                await _context.SaveChangesAsync();

                TempData["Success"] = "刪除成功";
            }
            return RedirectToAction("Index");
        }

    }
}
