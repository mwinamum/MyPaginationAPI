using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPaginationAPI
{
    /// <summary>
    /// Provides pagination functionality for a collection of items.
    /// </summary>
    /// <typeparam name="T">The type of items to paginate.</typeparam>
    public class Paginator<T>
    {
        private readonly IReadOnlyCollection<T> _source;
        public int PageNumber { get; }
        public int PageSize { get; }

        public Paginator(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            _source = source as IReadOnlyCollection<T> ?? source.ToList();
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 10 : pageSize;
        }

        public PagedResult<T> GetPagedResult()
        {
            var totalItems = _source.Count;
            var totalPages = totalItems == 0 ? 1 : (int)Math.Ceiling(totalItems / (double)PageSize);
            var pageNumber = Math.Min(PageNumber, totalPages);

            var items = _source
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return new PagedResult<T>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = PageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }
    }

    /// <summary>
    /// Represents a paged result.
    /// </summary>
    /// <typeparam name="T">The type of items in the result.</typeparam>
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }

}
