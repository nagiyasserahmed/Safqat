using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Application.Safqat.Queries.GetSafqat
{
    public class GetSafqasFilterDto
    {
        public string? SearchTerm { get; set; } 
        public string? Country { get; set; } 
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? IsNegotiable { get; set; }
        public Guid? CategoryId { get; set; }
        public DateTime? PublishedFrom { get; set; }
        public DateTime? PublishedTo { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
