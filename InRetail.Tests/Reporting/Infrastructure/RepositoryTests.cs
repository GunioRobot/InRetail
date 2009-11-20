using System;
using System.Linq;
using InRetail.Configuration;
using InRetail.Reporting.Dto;
using InRetail.Reporting.Infrastructure;
using NUnit.Framework;

namespace Tests.InRetail.Reporting.Infrastructure
{
    [TestFixture]
    public class RepositoryTests
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            new ReportingDatabaseBootStrapper().ReCreateDatabaseSchema();

            string sqliteConnectionString = string.Format("Data Source={0}", dataBaseFile);

            _repository = new SQLiteReportingRepository(sqliteConnectionString, new SqlSelectBuilder(),
                                                        new SqlInsertBuilder(), new SqlUpdateBuilder(),
                                                        new SqlDeleteBuilder());
        }

        #endregion

        private SQLiteReportingRepository _repository;
        private const string dataBaseFile = "InRetailReportingDataBase.db3";

        [Test]
        public void Will_be_able_to_save_and_retrieve_a_category_dto()
        {
            var categoryReport = new CategoryReport(Guid.NewGuid(), Guid.Empty, "Test Category");
            _repository.Save(categoryReport);
            CategoryReport sut = _repository.GetByExample<CategoryReport>(new {Name = "Test Category"}).FirstOrDefault();

            Assert.That(sut.Id, Is.EqualTo(categoryReport.Id));
            Assert.That(sut.Name, Is.EqualTo(categoryReport.Name));
        }
        [Test]
        public void Will_be_able_to_save_and_retrieve_a_category_dto2()
        {
            var root = Guid.NewGuid();
            var categoryReport = new CategoryReport(root, Guid.Empty, "Test Category");
            var cat1 = Guid.NewGuid();
            _repository.Save(new CategoryReport(cat1, root, "Test Category2"));
            _repository.Save(new CategoryReport(Guid.NewGuid(), cat1, "Test Category3"));
            _repository.Save(new CategoryReport(Guid.NewGuid(), root, "Test Category4"));
            _repository.Save(new CategoryReport(Guid.NewGuid(), root, "Test Category5"));
            
            _repository.Save(categoryReport);

            CategoryReport sut = _repository.GetByExample<CategoryReport>(new { Name = "Test Category" }).FirstOrDefault();

            Assert.That(sut.Id, Is.EqualTo(categoryReport.Id));
            Assert.That(sut.Name, Is.EqualTo(categoryReport.Name));
        }
        [Test]
        public void Will_be_able_to_save_and_retrieve_a_product_dto()
        {
            var productReport = new ProductReport(Guid.NewGuid(), "Test Product");
            _repository.Save(productReport);
            ProductReport sut = _repository.GetByExample<ProductReport>(new {Name = "Test Product"}).FirstOrDefault();

            Assert.That(sut.Id, Is.EqualTo(productReport.Id));
            Assert.That(sut.Name, Is.EqualTo(productReport.Name));
        }
    }
}