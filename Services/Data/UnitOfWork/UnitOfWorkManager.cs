using System.Data.Entity;
using Config;
using Domain.Interfaces;
using Domain.Interfaces.UnitOfWork;
using Services.Data.Context;
using Services.Migrations;

namespace Services.Data.UnitOfWork
{
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private bool _isDisposed;
        private readonly DatabaseContext _context;

        public UnitOfWorkManager(IDatabaseContext context)
        {
            //http://www.entityframeworktutorial.net/code-first/automated-migration-in-code-first.aspx
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Configuration>(AppConstants.DatabaseContext));
            _context = context as DatabaseContext;
        }

        /// <summary>
        /// Provides an instance of a unit of work. This wrapping in the manager
        /// class helps keep concerns separated
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork NewUnitOfWork()
        {
            return new UnitOfWork(_context);
        }

        /// <inheritdoc />
        /// <summary>
        /// Make sure there are no open sessions.
        /// In the web app this will be called when the injected UnitOfWork manager
        /// is disposed at the end of a request.
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _context.Dispose();
                _isDisposed = true;
            }
        }
    }
}
