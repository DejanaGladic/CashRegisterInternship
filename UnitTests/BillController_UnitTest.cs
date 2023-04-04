using AutoMapper;
using CashRegister.API.Controllers;
using CashRegister.Domain.Commands;
using CashRegister.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using System.Net;

namespace UnitTests
{
    [TestFixture]
    public class BillController_UnitTest
    {
        Mock<IMediator> _mediatorMock;
        BillController _billController;

        [SetUp]
        public void SetUp()
        {
            _mediatorMock = new Mock<IMediator>();
            _billController = new BillController(_mediatorMock.Object);
        }

        [Test]
        public async Task CreateBill_BillCreated_Status201()
        {
            BillPostPutDTO billPostPutDTO = new BillPostPutDTO("260-0056010016113-79", "card", "371449635398431");
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBillCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var trueResult = await _billController.CreateBill(billPostPutDTO);

            var statusCodeCreated = trueResult as CreatedResult;
            Assert.That(statusCodeCreated?.Value, Is.EqualTo("Bill has been created"));

        }




    }
}