using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ADOHelper.Common.DB
{
    public class AdoTransactionScope : IDisposable
    {
        private bool isNotInDistributedTransaction;
        private bool isRootTransaction;

        public AdoTransactionScope()
        {
            isRootTransaction = !Database.IsInAdoTransaction;
            isNotInDistributedTransaction = Transaction.Current == null;

            if (isRootTransaction && isNotInDistributedTransaction)
            {
                Database.BeginTransaction();
            }
        }

        public void Complete()
        {
            if (isRootTransaction && isNotInDistributedTransaction)
            {
                Database.CommitTransaction();
            }
        }

        public void Dispose()
        {
            if (isRootTransaction && isNotInDistributedTransaction)
            {
                Database.DisposeTransaction();
            }
        }
    }
}
