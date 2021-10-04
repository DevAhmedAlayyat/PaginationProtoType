using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginationProtoType.Dtos
{
    public class Paging<T>
    {
        public Paging(int currentPage, int totalPages, int pageSize, int totalCount, IEnumerable<T> data)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
        }

        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public IEnumerable<T> Data { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
