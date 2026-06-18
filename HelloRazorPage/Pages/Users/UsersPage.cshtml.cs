using HelloRazorPage.DbModels;
using HelloRazorPage.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelloRazorPage.Pages.Users
{
    public class UsersPageModel : PageModel
    {
        private readonly UserService _userService;

        [BindProperty]
        public User CurrentUser { get; set; } = new();

        public List<User> Users { get; private set; } = new();

        public UsersPageModel(UserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
            Users = _userService.GetAll();
        }

        public JsonResult OnGetUser(int id)
        {
            var user = _userService.GetById(id);

            if (user is null)
                return new JsonResult(new { error = "Пользователь не найден" }) { StatusCode = 404 };

            return new JsonResult(user);
        }

        public IActionResult OnPostSave()
        {
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"{state.Key}: {error.ErrorMessage}");
                }
            }
            if (!ModelState.IsValid)
            {
                Users = _userService.GetAll();
                return Page();
            }

            if (CurrentUser.Id == 0)
                _userService.Add(CurrentUser);
            else
                _userService.Update(CurrentUser);

            return RedirectToPage();
        } 

        public JsonResult OnPostDelete(int id)
        {
            bool success = _userService.DeleteById(id);
            return new JsonResult(new { success });
        }
    }
}