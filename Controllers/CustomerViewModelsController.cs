using FridgeManagement.Areas.Identity.Data;
using FridgeManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class CustomerViewModelsController : Controller
{
    private readonly UserManager<FridgeManagementUser> _userManager;

    public CustomerViewModelsController(UserManager<FridgeManagementUser> userManager)
    {
        _userManager = userManager;
    }

    // GET: Display all customers
    public async Task<IActionResult> Index()
    {
        var customers = await _userManager.Users
            .Where(u => u.UserRole == "Customer") // Filter users with UserRole "Customer"
            .ToListAsync();
        return View(customers);
    }

    // GET: Create new customer view
    public IActionResult Create()
    {
        return View(new UserViewModel());
    }

    // POST: Create a new customer
    [HttpPost]
    public async Task<IActionResult> Create(UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new FridgeManagementUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserRole = "Customer" // Set UserRole to "Customer"
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

    // GET: Edit customer view
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null || user.UserRole != "Customer") // Ensure the user is a customer
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

    // POST: Edit customer details
    [HttpPost]
    public async Task<IActionResult> Edit(UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null || user.UserRole != "Customer")
            {
                return NotFound();
            }

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserRole = model.UserRole;

            var result = await _userManager.UpdateAsync(user);
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

    // GET: Delete confirmation view
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null || user.UserRole != "Customer")
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

    // POST: Delete customer
    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null || user.UserRole != "Customer")
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
