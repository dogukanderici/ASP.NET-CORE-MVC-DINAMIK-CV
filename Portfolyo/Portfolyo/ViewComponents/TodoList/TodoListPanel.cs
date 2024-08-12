using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Portfolyo.ViewComponents.TodoList
{
    public class TodoListPanel : ViewComponent
    {
        private ITodoListService _todoListService;

        public TodoListPanel(ITodoListService todoListService)
        {
            _todoListService = todoListService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _todoListService.GetLast5Todo();

            if (!result.Success)
            {
                return View(); // Hata View'i Yazılabilir.
            }

            return View(result.Data);
        }
    }
}
