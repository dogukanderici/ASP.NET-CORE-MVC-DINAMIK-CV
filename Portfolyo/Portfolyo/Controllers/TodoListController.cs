using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.Controllers
{
    public class TodoListController : Controller
    {
        private ITodoListService _todoListService;

        public TodoListController(ITodoListService todoListService)
        {
            _todoListService = todoListService;
        }

        [HttpPost]
        public IActionResult AddTodo(TodoList todoList)
        {
            var result = _todoListService.TAdd(todoList);

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
