using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthCarolinaTaxRecoveryCalculator;
using NorthCarolinaTaxRecoveryCalculator.Controllers;
using NorthCarolinaTaxRecoveryCalculator.Security;
using FakeItEasy;
using NorthCarolinaTaxRecoveryCalculator.ViewModels.Project;
using NorthCarolinaTaxRecoveryCalculator.Models.Service;
using NorthCarolinaTaxRecoveryCalculator.Models;
using NorthCarolinaTaxRecoveryCalculator.Models.Data;
using NorthCarolinaTaxRecoveryCalculator.Tests.Models.Mocks;
using NorthCarolinaTaxRecoveryCalculator.ViewModels.PaymentVoucher;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Controllers
{ 
    [TestClass]
    public class PaymentVoucherControllerTest
    {
        private IUserRepository validUser;
        private IUserRepository invalidUser;
        private IProjectRepository projectRepository;
        private IPaymentVoucherRepository paymentVoucherRepository;

        private PaymentVoucherController controller;

        public PaymentVoucherControllerTest() 
        {
            validUser = A.Fake<IUserRepository>();
            A.CallTo(() => validUser.CurrentUserId).Returns(1);
            A.CallTo(() => validUser.CurrentUserName).Returns("Valid User");

            invalidUser = A.Fake<IUserRepository>();
            A.CallTo(() => invalidUser.CurrentUserId).Returns(2);
            A.CallTo(() => invalidUser.CurrentUserName).Returns("Invalid User");

            projectRepository = new MockProjectRepository();
            paymentVoucherRepository = new MockPaymentVoucherRepository();
            controller = new PaymentVoucherController(projectRepository, paymentVoucherRepository, validUser);

            //Create some test data
            //User 1 creates a project
            var firstProject = new Project();
            firstProject.Name = "Owned Project";
            firstProject.OwnerID = validUser.CurrentUserId;
            projectRepository.Create(firstProject);

            //User 2 creates a project
            var secondProject = new Project();
            secondProject.Name = "Shared project";
            secondProject.OwnerID = invalidUser.CurrentUserId;
            projectRepository.Create(secondProject);

            //User 2 invites user 1 to the project
            var acl = projectRepository.CreateCollaboration(secondProject.ID, "test@test.com");

            //User 1 accepts
            projectRepository.AcceptCollaboration(acl.ID, validUser.CurrentUserId);
        }

        [TestMethod]
        public void PaymentVoucherController_Index_ShouldReturnAllVouchersForAProject()
        {
            var project = new Project();
            projectRepository.Create(project);

            int voucherCount = 13;
            for (int i = 0; i < voucherCount; i++)
            {
                var voucher = new PaymentVoucher();
                voucher.ProjectID = project.ID;
                paymentVoucherRepository.Create(voucher);
            }

            var result = controller.Index(project.ID) as ViewResult;
            var model = result.Model as PaymentVouchersViewModel;

            Assert.AreEqual(voucherCount, model.Vouchers.Count());
        }

        [TestMethod]
        public void PaymentVoucherController_Index_ShouldReturnAValidProject()
        {
            var project = new Project();
            projectRepository.Create(project);
            
            var result = controller.Index(project.ID) as ViewResult;
            var model = result.Model as PaymentVouchersViewModel;

            Assert.IsNotNull(model.Project);
        }

        [TestMethod]
        public void PaymentVoucherController_Create_ShouldPopulateThePreparedByFieldWithTheCurrentUserName()
        {
            var project = new Project();
            projectRepository.Create(project);

            var result = controller.Create(project.ID) as ViewResult;
            var model = result.Model as PaymentVoucher;

            Assert.IsNotNull(model.PreparedBy);
            Assert.AreEqual("Valid User", model.PreparedBy);
        }

        //TODO: Test the rest of the actions
        

    }
}
