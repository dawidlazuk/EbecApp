using System;
using System.Collections.Generic;
using System.Text;

namespace EbecShop.DataAccess
{
    interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
