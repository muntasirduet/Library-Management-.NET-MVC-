using LMS.Core;
using LMS.Service.Service;
using LMS.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace LMS.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class BookController:Controller
{
    private readonly BookService _service;
    private readonly CategoryService _categoryService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public BookController(BookService service,CategoryService categoryService,IWebHostEnvironment webHostEnvironment)
    {
        _service = service;
        _categoryService = categoryService;
        _webHostEnvironment = webHostEnvironment;
    }
    public IActionResult Index()
    {
        var product = _service.GetAllDocs().ToList();
        
        return View(product);
    }
    
    public IActionResult Create()
    {
        //ViewBag.CategoryList = CategoryList;
        BookVM bookVm = new()
        {
            CategoryList = _categoryService.GetAllDocs().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
            Book = new Book()
        };
        return View(bookVm);
    }
    [HttpPost]
    public IActionResult Create(BookVM bookVm,IFormFile? file)
    {
        Console.WriteLine(file.FileName);
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string bookPath = Path.Combine(wwwRootPath, @"Images/Book");
                using (var fileStream = new FileStream(Path.Combine(bookPath,fileName),FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                bookVm.Book.ImageUrl = @"/Images/Book/" + fileName;
            }
            _service.AddDoc(bookVm.Book);
            TempData["Success"] = "Created Successfully";
            return RedirectToAction("Index", "Book");
        }
        else
        {
            bookVm.CategoryList = _categoryService.GetAllDocs().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(bookVm);

        }  
    }
    
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        BookVM bookVm = new()
        {
            CategoryList = _categoryService.GetAllDocs().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
            Book = new Book()
        };
        bookVm.Book  = _service.GetDocById(id);
       
        
   
        return View(bookVm);
    }
    
    [HttpPost]
    public IActionResult Edit(BookVM bookVm,IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string bookPath = Path.Combine(wwwRootPath, @"Images/Book");

                if (!string.IsNullOrEmpty(bookVm.Book.ImageUrl))
                {
                    var oldPath = Path.Combine(wwwRootPath, bookVm.Book.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(bookPath,fileName),FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                bookVm.Book.ImageUrl = @"/Images/Book/" + fileName;
            }
            _service.UpdateDoc(bookVm.Book);
            TempData["success"] = "Edited SuccessFully";
            return RedirectToAction("Index", "Category");
        }
        else
        {
            bookVm.CategoryList = _categoryService.GetAllDocs().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(bookVm);
        }

        
    }

}