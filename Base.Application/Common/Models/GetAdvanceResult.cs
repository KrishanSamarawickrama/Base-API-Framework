using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Domain.Common;

namespace Base.Application.Common
{
    public class GetAdvanceResult<TDocument> where TDocument : IDocument
    {
        public List<TDocument> DataCollection { get; set; }
        public PaginationResult Pagination { get; set; }

    }

    public class PaginationResult
    {
        public long RecordCount { get; set; }
        public int PageNo { get; set; }
        public int RecordsPerPage { get; set; }
        public long NoOfPages { get; set; }
    }
}
