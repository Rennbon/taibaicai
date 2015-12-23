using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace ADOHelper.Common.DB
{
    internal class AdoTransaction
    {
        private DbConnection con;
        private DbTransaction tx;

        internal Database Database { get; set; }
        internal DbConnection DbConnection
        {
            get { return con; }
        }
        internal DbTransaction DbTransaction
        {
            get { return tx; }
            set
            {
                tx = value;
                con = tx.Connection;
            }
        }
    }
}
