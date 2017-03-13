using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeTrello.DAL;

namespace FakeTrello.Tests.DAL
{
    [TestClass]
    public class FakeTrelloRepoTests
    {
        [TestMethod]
        public void EnsureICanCreateInstanceofRepo()
        {
            FakeTrelloRepository repo = new FakeTrelloRepository();

            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void EnsureICanInjectContextInstance()
        {
            FakeTrelloContext context = new FakeTrelloContext();
            FakeTrelloRepository repo = new FakeTrelloRepository(context);

            Assert.IsNotNull(repo.Context);
        }

        [TestMethod]
        public void EnsureIHaveNotNullContext()
        {
            FakeTrelloContext context = new FakeTrelloContext();
            FakeTrelloRepository repo = new FakeTrelloRepository(context);

            Assert.IsNotNull(repo.Context);
        }
    }
}
