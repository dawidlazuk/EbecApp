using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace EbecShop.DataAccess
{
    static class ConfigurationReader
    {
        internal static string ConnectionString => @"Server=.\SQLEXPRESS;Database=EbecShopDB;Trusted_Connection=True;";
    }
}
