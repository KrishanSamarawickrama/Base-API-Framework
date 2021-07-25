using System;
using Base.API.Controllers;
using Base.Application.Common;
using Xunit;

namespace CoreApiTest
{
    public class PersonControllerTest
    {
        [Fact]
        public void ShouldReturnAllPersons()
        {
            //Arrange
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

            var controller = new PersonController();

            //Act

            //Assert

        }
    }
}
