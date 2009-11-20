using InRetail.Reporting.Infrastructure;
using NUnit.Framework;

namespace Tests.InRetail.Reporting.Infrastructure
{
    [TestFixture]
    public class SqlInsertBuilderTest
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            _sqlInsertBuilder = new SqlInsertBuilder();
        }

        #endregion

        private SqlInsertBuilder _sqlInsertBuilder;

        [Test]
        public void
            When_calling_CreateSqlSelectStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_select_with_where_clause_statement_case_1
            ()
        {
            Assert.That(_sqlInsertBuilder.CreateSqlInsertStatementFromDto<TestDtoCase1>(),
                        Is.EqualTo(
                            "INSERT INTO TestDtoCase1 (Column1,Column2,Column3) VALUES (@column1,@column2,@column3);"));
        }

        [Test]
        public void
            When_calling_CreateSqlSelectStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_select_with_where_clause_statement_case_4
            ()
        {
            Assert.That(_sqlInsertBuilder.CreateSqlInsertStatementFromDto<TestDtoCase4>(),
                        Is.EqualTo("INSERT INTO TestDtoCase4 (Column1,Column3) VALUES (@column1,@column3);"));
        }
    }
}