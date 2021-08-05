using System.Linq;
using System.Threading;
using Application.Common.Interfaces;
using Application.Items.Commands.CreateItem;
using Application.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Items.Commands
{
    public class CreateItemCommandHandlerTests : CommandTestBase
    {
        private readonly DbContextOptions<WegisterDbContext> options = new DbContextOptionsBuilder<WegisterDbContext>()
            .UseSqlite("Data Source = WegisterItemCommandDb.db")
            .Options;

        private readonly IWegisterDbContext _context;
        private readonly CreateItemCommandHandler _sut;

        public CreateItemCommandHandlerTests()
        {
            _context = WegisterTestContextFactory.CreateWorkHourDb(options, UserService, MachineDateTime);

            _sut = new CreateItemCommandHandler(_context, Mediator.Object, ItemFactory);
        }

        [Fact]
        public void Handle_GivenValidRequest_ShouldRaiseItemCreatedNotification()
        {
            // Arrange
            _context.Items.ToList().Count.ShouldBe(3);
            var itemCommand = new CreateItemCommand("Utrawide monitor", 1099m,"Piece per");

            // Act
            var result = _sut.Handle(itemCommand, CancellationToken.None);

            // Assert
            Mediator.Verify(m => m.Publish(It.IsAny<ItemCreated>(), It.IsAny<CancellationToken>()), Times.Once);
            _context.Items.ToList().Count.ShouldBe(4);
            _context.Items
                .Any(i =>
                    i.Name == itemCommand.Name &&
                    i.Unit == itemCommand.Unit &&
                    i.Price == itemCommand.Price &&
                    i.CompanyId == UserService.CompanyId)
                .ShouldBe(true);
        }
    }
}