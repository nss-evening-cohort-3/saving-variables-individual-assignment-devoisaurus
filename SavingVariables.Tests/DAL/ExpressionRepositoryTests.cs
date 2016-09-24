using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SavingVariables.DAL;
using System.Collections.Generic;
using SavingVariables.Models;

namespace SavingVariables.Tests.DAL
{
    [TestClass]
    public class ExpressionRepositoryTests
    {
        [TestMethod]
        public void EnsureRepoCanCreateAnInstance()
        {
            ExpressionRepository repo = new ExpressionRepository();
            Assert.IsNotNull(repo);
        }
    }
}
