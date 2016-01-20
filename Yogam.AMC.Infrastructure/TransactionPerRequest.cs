using System.Data;
using System.Data.Entity;
using System.Web;
using Yogam.AMC.Data.Models;
using Yogam.AMC.Infrastructure.Tasks;

namespace Yogam.AMC.Infrastructure
{
    public class TransactionPerRequest : IRunOnEachRequest, IRunOnError, IRunAfterEachRequest
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpContextBase _httpContext;

        public TransactionPerRequest(ApplicationDbContext context, HttpContextBase httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        void IRunOnEachRequest.Execute()
        {
            _httpContext.Items["_Transaction"] = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        void IRunAfterEachRequest.Execute()
        {
            var transaction = (DbContextTransaction)_httpContext.Items["_Transaction"];

            if (_httpContext.Items["_Error"] != null)
            {
                transaction.Rollback();
            }
            else
            {
                transaction.Commit();
            }
        }

        void IRunOnError.Execute()
        {
            _httpContext.Items["_Error"] = true;
        }
    }
}