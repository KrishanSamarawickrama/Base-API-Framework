using System;
using System.Threading.Tasks;
using Base.Application.Common;
using Base.Application.Queries;
using Xunit;

namespace BaseFramework.Application.Test
{
    public class PersonTests
    {
        [Fact]
        public async Task ShouldReturnAllPersons()
        {
            PaginationAndSort dto = new()
            {
                Paging = new()
                {
                    Page = 1,
                    RecordsPerPage = 2
                },
                Sorting = new()
                {
                    ColumnName = "FirstName"
                }
            };

            var request = new GetPersonsQuery(dto);
           
        }
    }
}
