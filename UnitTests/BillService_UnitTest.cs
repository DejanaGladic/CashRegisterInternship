using CashRegister.Application.ServiceInterfaces;
using CashRegister.Application.Services;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using Moq;

namespace UnitTests
{
    [TestFixture]
    public class BillService_UnitTest
    {
        Mock<IUnitOfWork> unitOfWorkMock;
        IBillService iBillService;
        BillService billService;
        Mock<ICalculator> calculator;

        [SetUp]
        public void SetUp()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            iBillService = new BillService();
            billService = (BillService)iBillService;
            billService.SetUnitOfWork(unitOfWorkMock.Object);
            calculator = new Mock<ICalculator>();
            billService.SetCalculator(calculator.Object);
        }

        [Test]
        public void CreateBill_BillNotCreated_BillWithThatBillNumberAlreadyExists()
        {
            Bill bill = new Bill("260-0056010016113-79", "card", 0, "4003600000000014");

            //fejkujem izvrsavanje metode
            unitOfWorkMock.Setup(u => u.BillRepository.GetByStringId(bill.BillNumber)).Returns(bill);
            //koja ce se ovde navodno pozvati
            var trueResult = billService.CreateBill(bill).Result;
            //proveravam da li mi se add izvrsio, ne treba jer je rezultat getbyid neki bill te ne vrsim dodavanje
            //ne proveravam true/false ovde
            unitOfWorkMock.Verify(u => u.BillRepository.Add(It.IsAny<Bill>()), Times.Never);
        }

        [Test]
        public void CreateBill_BillNotCreated_SaveMethodReturnsFalse()
        {
            Bill bill = new Bill("260-0056010016113-79", "card", 0, "4003600000000014");
            Bill feakedReturnedResult = null;

            unitOfWorkMock.Setup(u => u.BillRepository.GetByStringId(bill.BillNumber)).Returns(feakedReturnedResult);
            unitOfWorkMock.Setup(u => u.BillRepository.Add(It.IsAny<Bill>())).Returns(Task.FromResult(true));
            unitOfWorkMock.Setup(u => u.Save()).Returns(0);
            var trueResult = billService.CreateBill(bill).Result;

            //pozvana je 1 u pravom pozivu, ali je false zbog unit of work save() metode
            unitOfWorkMock.Verify(u => u.BillRepository.Add(It.IsAny<Bill>()), Times.Once);
            Assert.False(trueResult);
        }

        [Test]
        public void CreateBill_BillCreated_SaveMethodReturnsTrue()
        {
            Bill bill = new Bill("260-0056010016113-79", "card", 0, "4003600000000014");
            Bill feakedReturnedResult = null;

            //mokujem i get i create!! jer takav treba da mi bude sled izvrsavanja
            unitOfWorkMock.Setup(u => u.BillRepository.GetByStringId(bill.BillNumber)).Returns(feakedReturnedResult);
            unitOfWorkMock.Setup(u => u.BillRepository.Add(It.IsAny<Bill>())).Returns(Task.FromResult(true));
            unitOfWorkMock.Setup(u => u.Save()).Returns(5);
            var trueResult = billService.CreateBill(bill).Result;

            //pozvana je 1 u pravom pozivu
            unitOfWorkMock.Verify(u => u.BillRepository.Add(It.IsAny<Bill>()), Times.Once);
            Assert.True(trueResult);
        }

        [Test]
        public void UpdateBill_BillNotUpdated_PassedNullAsBill()
        {
            Bill bill = null;

            var trueResult = billService.UpdateBill(bill);

            Assert.False(trueResult);
        }

        [Test]
        public void UpdateBill_BillNotUpdated_BillWithThatBillNumberNotExists()
        {
            Bill bill = new Bill("260-0056010016113-79", "card", 0, "4003600000000014");
            Bill feakedReturnedResult = null;

            unitOfWorkMock.Setup(u => u.BillRepository.GetByStringId(bill.BillNumber)).Returns(feakedReturnedResult);

            var trueResult = billService.UpdateBill(bill);

            Assert.False(trueResult);
        }

        [Test]
        public void UpdateBill_BillNotUpdated_SaveMethodReturnsFalse()
        {
            Bill billsUpdateValues = new Bill("260-0056010016113-79", "cash", 0, "4003600000000014");
            Bill returnedBill = new Bill("260-0056010016113-79", "card", 0, "4003600000000014");
 
            unitOfWorkMock.Setup(u => u.BillRepository.GetByStringId(billsUpdateValues.BillNumber)).Returns(returnedBill);
            unitOfWorkMock.Setup(u => u.BillRepository.Update(returnedBill)).Verifiable();
            unitOfWorkMock.Setup(u => u.Save()).Returns(0);

            var trueResult = billService.UpdateBill(billsUpdateValues);

            unitOfWorkMock.Verify(u => u.BillRepository.Update(returnedBill), Times.Once);
            Assert.False(trueResult);
        }

        [Test]
        public void DeleteBill_BillNotDeleted_PassedNullAsBill()
        {
            string billNum = null;

            var trueResult = billService.DeleteBill(billNum);

            Assert.False(trueResult);
        }

        [Test]
        public void DeleteBill_BillNotDeleted_BillWithThatBillNumberNotExists()
        {
            Bill bill = new Bill("260-0056010016113-79", "card", 0, "4003600000000014");
            Bill feakedReturnedResult = null;

            unitOfWorkMock.Setup(u => u.BillRepository.GetByStringId(bill.BillNumber)).Returns(feakedReturnedResult);

            var trueResult = billService.DeleteBill(bill.BillNumber);

            Assert.False(trueResult);
        }

        [Test]
        public void DeleteBill_BillNotDeleted_SaveMethodReturnsFalse()
        {
            Bill returnedBill = new Bill("260-0056010016113-79", "card", 0, "4003600000000014");

            unitOfWorkMock.Setup(u => u.BillRepository.GetByStringId(returnedBill.BillNumber)).Returns(returnedBill);
            unitOfWorkMock.Setup(u => u.BillRepository.Delete(returnedBill)).Verifiable();
            unitOfWorkMock.Setup(u => u.Save()).Returns(0);

            var trueResult = billService.DeleteBill(returnedBill.BillNumber);

            unitOfWorkMock.Verify(u => u.BillRepository.Delete(returnedBill), Times.Once);
            Assert.False(trueResult);
        }

        [Test]
        public void DeleteBill_BillNotDeleted_SaveMethodReturnsTrue()
        {
            Bill returnedBill = new Bill("260-0056010016113-79", "card", 0, "4003600000000014");

            unitOfWorkMock.Setup(u => u.BillRepository.GetByStringId(returnedBill.BillNumber)).Returns(returnedBill);
            unitOfWorkMock.Setup(u => u.BillRepository.Delete(returnedBill)).Verifiable();
            unitOfWorkMock.Setup(u => u.Save()).Returns(5);

            var trueResult = billService.DeleteBill(returnedBill.BillNumber);

            unitOfWorkMock.Verify(u => u.BillRepository.Delete(returnedBill), Times.Once);
            Assert.True(trueResult);
        }

        [Test]
        public void GetBillById_ReturnedBill_NotNull()
        {
            Bill bill = new Bill("105-0081231231231-73", "card", 0, "4003600000000014");

            unitOfWorkMock.Setup(u => u.BillRepository.GetByStringId(bill.BillNumber)).Returns(bill);
            var trueResult = billService.GetBillById(bill.BillNumber);

            Assert.That(bill, Is.EqualTo(trueResult));
        }

        [Test]
        public void GetBillById_ReturnBill_Null()
        {
            Bill bill = null;

            unitOfWorkMock.Setup(u => u.BillRepository.GetByStringId("105-0081231231231-73")).Returns(bill);
            var trueResult = billService.GetBillById("105-0081231231231-73");

            Assert.That(bill, Is.EqualTo(trueResult));
        }

        [Test]
        public void GetAllBills_ReturnBills_EmptyList()
        {
           List<Bill> bills = new List<Bill>();

            unitOfWorkMock.Setup(u => u.BillRepository.GetAll()).Returns(Task.FromResult(bills));
            var trueResult = billService.GetAllBills().Result;

            Assert.That(trueResult.Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetAllBills_ReturnBills_NotEmptyList()
        {
            List<Bill> bills = new List<Bill>() { new Bill("105-0081231231231-73", "cash", 1000, "4003600000000014") };

            unitOfWorkMock.Setup(u => u.BillRepository.GetAll()).Returns(Task.FromResult(bills));
            var trueResult = billService.GetAllBills().Result;

            Assert.That(trueResult.Count(), Is.EqualTo(bills.Count()));
        }

        [Test]
        public void IfBillByIdExists_BillNumNull_ReturnFalse()
        {
            string billNum = null;
            var trueResult = billService.IfBillByIdExists(billNum);

            Assert.False(trueResult);
        }

        [Test]
        public void IfBillByIdExists_BillNotExists_ReturnFalse()
        {
            string billNum = "105-0081231231231-73";
            unitOfWorkMock.Setup(u => u.BillRepository.IfExistsByStringId(billNum)).Returns(false);

            var trueResult = billService.IfBillByIdExists(billNum);

            Assert.False(trueResult);
        }

        [Test]
        public void IfBillByIdExists_BillExists_ReturnTrue()
        {
            string billNum = "105-0081231231231-73";
            unitOfWorkMock.Setup(u => u.BillRepository.IfExistsByStringId(billNum)).Returns(true);

            var trueResult = billService.IfBillByIdExists(billNum);

            Assert.True(trueResult);
        }

        [Test]
        public void GetBillExchangeRate_BillNotExists_ReturnFalse()
        {
            Bill bill = new Bill("105-0081231231231-73", "card", 50000, "4003600000000014");
            Bill returnedBill = null;

            unitOfWorkMock.Setup(u => u.BillRepository.GetByStringId(bill.BillNumber)).Returns(returnedBill);

            var returnedResult = billService.GetBillExchangeRate(bill.BillNumber, "RSD");

            Assert.Null(returnedResult);
        }

        [Test]
        public void GetBillExchangeRate_BillExists_ReturnBillWithRSDTotalPrice()
        {
            Bill bill = new Bill("105-0081231231231-73", "card", 50000, "4003600000000014");

            unitOfWorkMock.Setup(u => u.BillRepository.GetByStringId(bill.BillNumber)).Returns(bill);
            calculator.Setup(c => c.moneyConversion(bill.TotalPrice, "RSD")).Returns(50000);

            var returnedBill = billService.GetBillExchangeRate(bill.BillNumber, "RSD");

            Assert.That(bill, Is.EqualTo(returnedBill));
        }

        [Test]
        public void GetBillExchangeRate_BillExists_ReturnBillWithUSDTotalPrice()
        {
            Bill bill = new Bill("105-0081231231231-73", "card", 107000, "4003600000000014");

            unitOfWorkMock.Setup(u => u.BillRepository.GetByStringId(bill.BillNumber)).Returns(bill);
            calculator.Setup(c => c.moneyConversion(bill.TotalPrice, "USD")).Returns(1000);

            var returnedBill = billService.GetBillExchangeRate(bill.BillNumber, "USD");

            Assert.That(bill, Is.EqualTo(returnedBill));
        }

        [Test]
        public void GetBillExchangeRate_BillExists_ReturnBillWithEURotalPrice()
        {
            Bill bill = new Bill("105-0081231231231-73", "card", 118000, "4003600000000014");

            unitOfWorkMock.Setup(u => u.BillRepository.GetByStringId(bill.BillNumber)).Returns(bill);
            calculator.Setup(c => c.moneyConversion(bill.TotalPrice, "EUR")).Returns(1000);

            var returnedBill = billService.GetBillExchangeRate(bill.BillNumber, "EUR");

            Assert.That(bill, Is.EqualTo(returnedBill));
        }



    }
}