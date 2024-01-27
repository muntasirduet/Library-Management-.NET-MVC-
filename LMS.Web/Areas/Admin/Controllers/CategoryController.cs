using LMS.Core;
using LMS.Service;
using LMS.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Web.Areas.Admin.Controllers;
[Area("Admin")]
public class CategoryController:Controller
{
    private readonly CategoryService _service;
    public CategoryController(CategoryService service)
    {
        _service = service;
    }
    public IActionResult Index()
    {
        List<Category?> categories = _service.GetAllDocs().ToList();
        return View(categories);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _service.AddDoc(category);
            TempData["Success"] = "Created Successfully";
            return RedirectToAction("Index", "Category");
        }

        return View();
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? entity = _service.GetDocById(id);
        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    [HttpPost]
    public IActionResult Update(Category category)
    {
        if (ModelState.IsValid)
        {
            _service.UpdateDoc(category);
            TempData["success"] = "Edited SuccessFully";
            return RedirectToAction("Index", "Category");
        }

        return View("Edit");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Category? entity = _service.GetDocById(id);
        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int id)
    {
        Category? obj = _service.GetDocById(id);
        if (obj == null)
        {
            return NotFound();
        }

        _service.DeleteDoc(obj);
        TempData["success"] = "Deleted Success Fully";
        return RedirectToAction("Index", "Category");

    }



}