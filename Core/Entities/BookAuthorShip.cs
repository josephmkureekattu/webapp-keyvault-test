using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BookAuthorShip
    {
        public int BookId { get; set; }

        public int AuthorId { get; set; }

        public int AuthorshipRoleId { get; set; }

        public Book book { get; set; }
        public Author author { get; set; }

        public AuthorshipRole authorshipRole { get; set; }
    }
}
