using System;

namespace Domain.Interfaces.UnitOfWork
{
    public partial interface IUnitOfWorkManager : IDisposable
    {
        IUnitOfWork NewUnitOfWork();
    }
}
