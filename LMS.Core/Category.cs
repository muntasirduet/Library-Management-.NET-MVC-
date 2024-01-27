using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LMS.Core;

public class Category:BaseEntity
{
    [DisplayName("Category Name")]
    public string Name { get; set; }
    [ValidateNever]
    public List<Book> Book { get; set; }

}