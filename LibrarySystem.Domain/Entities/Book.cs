﻿namespace LibrarySystem.Domain.Entities;
public class Book
{
    public int Id { get; set; }


    // for book as a document 
    public string Title { get; set; }=string.Empty;
    public string FileContentType {  get; set; }=string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public string FilePath {  get; set; }=string.Empty;

    // for book as a image 
    public string ImageName { get; set; } = string.Empty;
    public string ImageContentType { get; set; } = string.Empty;
    public string ImageExtension { get; set; } = string.Empty;
    public string ImagePath { get; set; }= string.Empty;

    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal PriceForBuy { get; set; }
    public decimal PriceForBorrow { get; set; }
    public DateTime PublishedDate { get; set; }=DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public int CategoryId { get; set; }
    public int AuthorId { get; set; }


    // Navigation properties
    public Category Category { get; set; } = default!;
    public Author Author { get; set; } = default!;
    public ICollection<Review> ?Reviews { get; set; }
    public ICollection<BorrowedBook> ?BorrowedBooks { get; set; }
}
