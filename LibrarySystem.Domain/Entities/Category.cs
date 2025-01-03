﻿namespace LibrarySystem.Domain.Entities;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }=string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public ICollection<Book> ?Books { get; set; } 
}
