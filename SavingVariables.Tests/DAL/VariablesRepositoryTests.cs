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
    public class VariablesRepositoryTests //objects for tests to utilize
    {
        Mock<VariablesContext> mock_context = new Mock<VariablesContext>();
        Mock<DbSet<Variable>> mock_variable_table = new Mock<DbSet<Variable>>();
        List<Variable> variable_datastore = new List<Variable>(); //Fake Database

        public void ConnectMocksToDatastore() //method to contain the setups for Moq to use
        {
            var queryable_list = variable_datastore.AsQueryable();

            // Tricks LINQ into thinking that our new Queryable List is a Database table.
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.Provider).Returns(queryable_list.Provider);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.Expression).Returns(queryable_list.Expression);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_list.GetEnumerator());

            //Context returns the mock_variable_table(queryable list) when someone calls the VariablesContext.Variables getter
            mock_context.Setup(c => c.Variables).Returns(mock_variable_table.Object);

            // It.IsAny<Variable>()
            mock_variable_table.Setup(v => v.Add(It.IsAny<Variable>())).Callback( /*Capture the variable sent */ (Variable my_var) => /* Add it to a list */ variable_datastore.Add(my_var));
        }

        [TestInitialize] //method runs before every test, eliminates cluttered/repetitive code
        public void Initialize()
        {
            //creates mock context 
            mock_context = new Mock<VariablesContext>();
            mock_variable_table = new Mock<DbSet<Variable>>();
            variable_datastore = new List<Variable>(); //fake
        }

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
            //Arrange
            ConnectMocksToDatastore();
            VariablesRepository repo = new VariablesRepository(mock_context.Object);

            //Act
            List<Variable> found_variables = repo.GetVariables();

            //Assert
            int expected_variable_count = 0;
            int actual_variable_count = found_variables.Count;
            Assert.AreEqual(expected_variable_count, actual_variable_count);
        }

        [TestMethod]
        public void EnsureCanAddVariableToRepo()
        {

            //Arrange
            VariablesRepository repo = new VariablesRepository(mock_context.Object);
            ConnectMocksToDatastore();
            Variable my_variable = new Variable { Name = "a", Value = 3 };

            //Act
            repo.AddVariable(my_variable);
            

            //Assert
            int actual_variable_count = repo.GetVariables().Count;
            int expected_variable_count = 1;

            Assert.AreEqual(expected_variable_count, actual_variable_count);
        }

    }
}
