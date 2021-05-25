using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Core.DTOs
{
    public class PublisherWithBookCnt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BookCnt { get; set; }
    }
}
