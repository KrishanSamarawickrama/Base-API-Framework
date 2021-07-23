using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Common
{
    public class PaginationAndSort
    {
        public Pagination Paging { get; set; }
        public SortInfo Sorting { get; set; }
    }

    public class Pagination
    {
        public int Page { get; set; } = 1;

        private int recordsPerPage = 10;
        private readonly int maxRecordsPerPage = 50;

        public int RecordsPerPage
        {
            get
            {
                return recordsPerPage;
            }

            set
            {
                recordsPerPage = (value > maxRecordsPerPage) ? maxRecordsPerPage : value;
            }
        }
    }

    public class SortInfo
    {
        public string ColumnName { get; set; } = "Id";
        public SortOrder SortOrder { get; set; } = SortOrder.Desc;
    }
}
