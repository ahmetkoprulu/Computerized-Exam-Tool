﻿using Cet.Core.DataAccess.EntityFramework;
using Cet.DataAccess.Abstract;
using Cet.DataAccess.EntityFramework.Concrete;
using Cet.Entities.Concrete;

namespace Cet.DataAccess.Concrete.EntityFramework
{
    public class AnswerRepository
        : Repository<Answer, ApplicationDbContext>, IAnswerRepository
    {
        
    }
}