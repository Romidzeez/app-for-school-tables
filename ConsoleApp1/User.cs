using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1;

public partial class User
{
    public string? Name { get; }
    [Range(0, 100, ErrorMessage = "Age must be between 0 and 100.")]
    public int Age { get; set; }
    public int Id { get; set; }
    public string? FirstName {  get; set; }
    public string? LastName { get; set; }
}
