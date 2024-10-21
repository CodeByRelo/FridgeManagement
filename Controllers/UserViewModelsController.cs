using FridgeManagement.Areas.Identity.Data;
using FridgeManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class UserViewModelsController : Controller
{
    private readonly UserManager<FridgeManagementUser> _userManager;

    public UserViewModelsController(UserManager<FridgeManagementUser> userManager)
    {
        _userManager = userManager;
    }

    // GET: Display all users
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users
            .Where(u => u.UserRole != "Customer") // Filter out customers
            .ToListAsync();
        return View(users);
    }

    // GET: Create new user view
    public IActionResult Create()
    {
        return View(new UserViewModel());
    }

    // POST: Create a new user
    [HttpPost]
    public async Task<IActionResult> Create(UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Check if the email already exists
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email is already in use.");
                return View(model);
            }

            var user = new FridgeManagementUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserRole = model.UserRole
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return View(model);
    }


    // GET: Edit user view
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var model = new UserViewModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserRole = user.UserRole
        };

        return View(model);
    }

    // POST: Edit user details
    [HttpPost]
    public async Task<IActionResult> Edit(UserViewModel model)
    {
        // Validate the incoming model
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound(); // Return 404 if the user is not found
            }

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserRole = model.UserRole;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index"); // Redirect on success
            }

            // If there are errors, add them to the model state
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        else
        {
            // Log the model state errors
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                // You can log to console, file, or any logging framework you use
                Console.WriteLine(error.ErrorMessage); // Or use a logging library
            }
        }

        // If we get here, something failed; redisplay the form with the current model
        return View(model);
    }



    // GET: Delete confirmation view
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var model = new UserViewModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserRole = user.UserRole
        };

        return View(model);  // Confirm deletion
    }

    // POST: Delete user
    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return View("Index");
    }
}
