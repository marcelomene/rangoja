using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangoJaDatabaseAccess.NHibernate
{
    interface IDatabaseOperations
    {
        void Save<T>(T obj);
    }
}
