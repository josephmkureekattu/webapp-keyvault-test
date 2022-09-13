﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AuthorBookMapping
    {

        public int AuthorId { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }
        public Author Author { get; set; }

    }
}
