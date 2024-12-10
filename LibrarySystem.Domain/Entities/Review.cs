using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment {  get; set; }=string.Empty;
        public int Rating {  get; set; }
        public string UserId { get; set; } = string.Empty;
        public int BookId {  get; set; }

        public ApplicationUser User { get; set; } = default!;
        public Book Book { get; set; }=default!;
    }
}
