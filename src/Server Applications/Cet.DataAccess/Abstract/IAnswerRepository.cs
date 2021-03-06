﻿using Cet.Core.DataAccess;
using Cet.Entities.Concrete;

namespace Cet.DataAccess.Abstract
{
    public interface IAnswerRepository 
        : IRepository<Answer>
    {
        void AddOrUpdate(Answer entity);
    }
}