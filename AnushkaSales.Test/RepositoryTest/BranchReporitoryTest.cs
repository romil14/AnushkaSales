using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnushkaSales.Model.Repositorys;
using AnushkaSales.Model;
using AnushkaSales.Model.Infrastructure;
using AnushkaSales.Model.Models;

namespace AnushkaSales.Test.RepositoryTest
{
    [TestClass]
    public class BranchReporitoryTest
    {
        BranchRepository repo;

        [TestInitialize]
        public void TestSetup()
        {
            AppDbContext dbContext = new AppDbContext();
            //ApplicationDbInitialize db = new ApplicationDbInitialize();
            //System.Data.Entity.Database.SetInitializer(db);
            repo = new BranchRepository(dbContext);
        }

        

        [TestMethod]
        public void IsRepositoryAddsBranch()
        {
            Branch branchToInsert = new Branch { Id = 1, BranchName = "Anushka Sales", BranchAddress = "Kamptee Road, Indora Square", BranchCity = "Nagpur", BranchPinCode = 440014 };
            repo.Add(branchToInsert);            
            var result = repo.FindById(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IsRepositoryInitalizeWithValidNumberOfData()
        {
            //var result = repo.FindById(1);
            //Assert.IsNull(result);

        }
    }
}
