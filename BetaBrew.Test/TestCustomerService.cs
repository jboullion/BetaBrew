using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BetaBrew.Data;
using BetaBrew.Data.Models;
using BetaBrew.Services.Customer;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using System.Linq;
using Moq;

namespace BetaBrew.Test
{
    public class TestCustomerService
    {
        [Fact]
        public void CustomerService_GetsAllCustomers_GivenTheyExist()
        {
            var options = new DbContextOptionsBuilder<BetaBrewDbContext>()
                .UseInMemoryDatabase("gets_all").Options;

            using var context = new BetaBrewDbContext(options);

            var sut = new CustomerService(context);

            sut.CreateCustomer(new Customer { Id = 123123 });
            sut.CreateCustomer(new Customer { Id = 123 });

            var allCustomers = sut.GetAllCustomers();

            allCustomers.Count.Should().Be(2);
        }

        [Fact]
        public void CustomerService_CreatesCustomer_GivenNewCustomerObject()
        {
            var options = new DbContextOptionsBuilder<BetaBrewDbContext>()
                .UseInMemoryDatabase("add_writes_to_database").Options;

            using var context = new BetaBrewDbContext(options);
            var sut = new CustomerService(context);

            sut.CreateCustomer(new Customer { Id = 123123 });

            var allCustomers = sut.GetAllCustomers();

            context.Customers.Single().Id.Should().Be(123123);
        }

        [Fact]
        public void CustomerService_DeleteCustomer_GivenId()
        {
            var options = new DbContextOptionsBuilder<BetaBrewDbContext>()
                .UseInMemoryDatabase("deletes_one").Options;

            using var context = new BetaBrewDbContext(options);
            var sut = new CustomerService(context);

            var customerId = 123123;

            sut.CreateCustomer(new Customer { Id = customerId });
            sut.DeleteCustomer(customerId);

            var allCustomers = sut.GetAllCustomers();

            allCustomers.Count().Should().Be(0);
        }


        [Fact]
        public void CustomerService_OrdersByLastName_WhenGetAllCustomersInvoked()
        {
            // Arrange
            var data = new List<Customer>
            {
                new Customer { Id = 220, LastName = "Zulu"},
                new Customer { Id = 300, LastName = "Alpha"},
                new Customer { Id = 150, LastName = "Xu"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Customer>>();

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.GetEnumerator())
                .Returns(data.GetEnumerator());

            var mockContext = new Mock<BetaBrewDbContext>();

            mockContext.Setup(c => c.Customers)
                .Returns(mockSet.Object);


            // Act
            var sut = new CustomerService(mockContext.Object);
            var customers = sut.GetAllCustomers();


            // Assert
            customers.Count.Should().Be(3);

            customers[0].Id.Should().Be(300);
            customers[1].Id.Should().Be(150);
            customers[2].Id.Should().Be(220);

        }
    }

}
