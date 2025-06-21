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
        private readonly IEnumerable<T> _source;
        private readonly int _pageNumber;
        private readonly int _pageSize;

        public Paginator(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _pageNumber = pageNumber < 1 ? 1 : pageNumber;
            _pageSize = pageSize < 1 ? 10 : pageSize;
        }

        public PagedResult<T> GetPagedResult()
        {
            var totalItems = _source.Count();
            var items = _source
                .Skip((_pageNumber - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            return new PagedResult<T>
            {
                Items = items,
                PageNumber = _pageNumber,
                PageSize = _pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)_pageSize)
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
