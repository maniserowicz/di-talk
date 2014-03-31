using System;
using System.Collections.Generic;
using System.Data;

namespace Procent.DependencyInjection.app
{
    public class OperationContext : IDisposable
    {
        public IDbTransaction Transaction { private get; set; }

        readonly List<Exception> _errors = new List<Exception>();
        public void RegisterError(Exception error)
        {
            _errors.Add(error);
        }

        public void Dispose()
        {
            if (_errors.Count == 0)
            {
                Transaction.Commit();
            }
            else
            {
                Transaction.Rollback();
            }
        }
    }
}