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
        public void SaveTest()
        {
            User user = new User();
            user.Name = "Roger Leite";
            user.Address = "Avenida D, 234";
            user.SocialId = "222.222.222-22";

            new DatabaseOperations().Save(user);
        }
    }
}
