using System;
using System.Collections.Generic;
using System.Text;
using Cet.Core.DataAccess;
using Cet.Core.Entities;

namespace Cet.Core.BusinessLogic
{
    public interface IRegistrableService<TRegistrable> 
        where TRegistrable : class, IRegistrable, new()
    {
        TRegistrable Register(TRegistrable user, string password);
        TRegistrable Login(string userName, string password);
        bool IsUserExist(string userName);
    }
}