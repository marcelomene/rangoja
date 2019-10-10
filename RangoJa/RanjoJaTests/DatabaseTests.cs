using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using RangoJaDatabaseAccess.DbModels;
using RangoJaDatabaseAccess.NHibernate;

namespace RanjoJaTests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void SaveObjectTest()
        {
            User user = new User("Marcelo", "mm@mm.com", "123456");
            new DatabaseOperations().SaveObject(user);
        }

        [TestMethod]
        public void GetObject()
        {
            DatabaseOperations dbOps = new DatabaseOperations();
            User user = dbOps.GetObject(1, typeof(User));
        }
    }
}
