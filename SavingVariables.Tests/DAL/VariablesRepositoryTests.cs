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

            //Hey Context, return the mock_variable_table when someone calls the VariablesContext.Variables getter
            mock_context.Setup(c => c.Variables).Returns(mock_variable_table.Object);

            //Now we have our repo use the mock context for all of it's operations
            //mock_context.Object gets the VariableContext instance contained in the mock
            VariablesRepository repo = new VariablesRepository(mock_context.Object);
         
            List<Variable> found_variables = repo.GetVariables();

            //Assert
            int expected_variable_count = 0;
            int actual_variable_count = found_variables.Count;
            Assert.AreEqual(expected_variable_count, actual_variable_count);
        }
    }
}
