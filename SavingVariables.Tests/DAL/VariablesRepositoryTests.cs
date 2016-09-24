using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SavingVariables.DAL;
using Moq;
using System.Data.Entity;
using System.Collections.Generic;
using SavingVariables.Models;
using System.Linq;

namespace SavingVariables.Tests.DAL
{
    [TestClass]
    public class VariablesRepositoryTests
    {
   
        [TestMethod]
        public void EnsureRepoCanCreateAnInstance()
        {
            VariablesRepository repo = new VariablesRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void EnsureContextIsNotNull()
        {
            VariablesRepository repo = new VariablesRepository();
            Assert.IsNotNull(repo.Context);
        }

        [TestMethod]
        public void EnsureRepoHasNoVariables()
        {
            Mock<VariablesContext> mock_context = new Mock<VariablesContext>();
            Mock<DbSet<Variable>> mock_variable_table = new Mock<DbSet<Variable>>();
            List<Variable> variable_datastore = new List<Variable>(); //Fake Database

            var queryable_list = variable_datastore.AsQueryable();

            // Lie to LINQ make it think that our new Queryable List is a Database table.
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.Provider).Returns(queryable_list.Provider);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.Expression).Returns(queryable_list.Expression);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_list.GetEnumerator());

            VariablesRepository repo = new VariablesRepository();
         
            List<Variable> found_variables = repo.GetVariables();           
        }
    }
}
