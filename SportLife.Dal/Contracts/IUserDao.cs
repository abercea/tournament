﻿using SportLife.Dal.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Dal.Contracts
{
    public interface IUserDao : IDao<Student>
    {
        void addFirst();
    }
}
