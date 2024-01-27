using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LMS.Core;

public class Book:BaseEntity
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Publication { get; set; }

    public int  CategoryId { get; set; }
    
    [ValidateNever]
    public Category Category { get; set; }
    
    public string? ImageUrl { get; set; }
}