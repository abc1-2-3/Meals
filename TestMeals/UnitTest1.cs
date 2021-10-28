using Castle.Core.Configuration;
using Meals.BLL.ImplementBLL;
using Meals.Models.Context;
using Meals.Models.DTO;
using Meals.Repository.ImplementRepository;
using Meals.Repository.InterfaceRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestMeals
{
    public class UnitTest1
    {

        private DBContext GetInMemory()
        {
            DbContextOptions<DBContext> options;
            var builder = new DbContextOptionsBuilder<DBContext>();
            builder.UseInMemoryDatabase("InMemory");
            options = builder.Options;
            DBContext DataContext = new DBContext(options);
            DataContext.Database.EnsureDeleted();
            DataContext.Database.EnsureCreated();
            DataContext.Orders.Add(new Order() { ModifyDate = Convert.ToDateTime("2021-10-18"), OrderId = "1", CustomerId = 1, CreateDate = Convert.ToDateTime("2021-10-18"), OrderPrice = 123, OrderStatus = "成立", OrderSubject = "zxc", TableNumber = 1 });
            DataContext.Boms.Add(new Bom() { ModifyDate = Convert.ToDateTime("2021-10-18"), CreateDate = Convert.ToDateTime("2021-10-18"),AutoId=1,ItemId=1,ItemUsageAmount=10,ProductId=1});

            DataContext.OrderDetails.Add(new OrderDetail() { ModifyDate = Convert.ToDateTime("2021-10-18"), Amount = 1, Status = "成立", CreateDate = Convert.ToDateTime("2021-10-18"), OrderDetailId = 1, OrderId = "1", ProductId = 1 });
            DataContext.Products.Add(new Product() { ModifyDate = Convert.ToDateTime("2021-10-18"), ProductStatus = "可點餐", CreateDate = Convert.ToDateTime("2021-10-18"), ProductId = 1, ProductName = "產品名稱", ProductPicture = "", ProductPrice = 100, ProductType = "甜點" });
            DataContext.Items.Add(new Item() { ModifyDate = Convert.ToDateTime("2021-10-18"), CreateDate = Convert.ToDateTime("2021-10-18"), ItemId = 1, ItemName = "hhh", ItemStock = 10000 });
            DataContext.SaveChanges();
            return DataContext;
        }
        [Fact]
        public void Test1()
        {

            var dbContext = GetInMemory();

            var mockLogService = new Mock<ILogger<SerchBLL>>();

            var testserchOrderBLL = new SerchBLL(mockLogService.Object, dbContext);
            var actual = testserchOrderBLL.SerchOrder(1);

            Assert.True(actual.list.Count > 0);
        }
        [Fact]
        public void Test2()
        {

            var dbContext = GetInMemory();
            

            var mockLogService = new Mock<ILogger<OrderBLL>>();
            //var mockMailService = new Mock<Mail,IConfiguration configuration, ILogger<Mail>>();
            var mockItemService = new Mock<IItemRepository>();
            var mockorderService = new Mock<IOrderRepository>();
            var mockorderdetailService = new OrderDetailRepository(dbContext);

            var testserchOrderBLL = new OrderBLL(null, mockItemService.Object, mockLogService.Object,dbContext, mockorderService.Object, mockorderdetailService);
            var actual = testserchOrderBLL.AdditionOrder(fakeorderdto());

            Assert.True(actual.Result);
        }
        public OrderDTO fakeorderdto()
        {
            var detail = new List<OrderDetailDTO>();
            detail.Add(new OrderDetailDTO()
            {
                Amount=1,
                OrderDetailId=1,
                OrderId="1",
                ProductId=1,
                Status="成立"
            });
            var order = new OrderDTO()
            {
                CustomerId=1,
                OrderDetails=detail,
                OrderId="1",
                OrderPrice=100,
                OrderStatus="成立",
                OrderSubject="abc123",
                TableNumber=1
                
            };
            return order;
        }
    }
}
