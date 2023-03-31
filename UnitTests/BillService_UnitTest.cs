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

        [SetUp]
        public void SetUp()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            iBillService = new BillService();
            billService = (BillService)iBillService;
            billService.SetUnitOfWork(unitOfWorkMock.Object);
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

        //ne kreira bill jer vec postoji u bazi
        [Test]
        public void CreateBill_BillNotCreated_BillWithThatBillNumberAlreadyExists()
        {
            Bill bill = new Bill("260-0056010016113-79", "card", 0, "4003600000000014");

            //fejkujem izvrsavanje metode
            unitOfWorkMock.Setup(u => u.BillRepository.GetByStringId(bill.BillNumber)).Returns(bill);
            //koja ce se ovde navodno pozvati
            var trueResult = billService.CreateBill(bill).Result;
            //proveravam da li mi se add izvrsio, ne treba jer je rezultat getbyid neki bill te ne vrsim dodavanje
            //, ne proveravam true/false ovde
            unitOfWorkMock.Verify(u => u.BillRepository.Add(It.IsAny<Bill>()), Times.Never);
        }

        //ne kreira bill jer save vraca false
        [Test]
        public void CreateBill_BillCreated_False()
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

        //kreira bill
        [Test]
        public void CreateBill_BillCreated_True()
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
       
    }
}