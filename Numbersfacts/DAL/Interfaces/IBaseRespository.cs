﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numbersfacts.DAL.Interfaces
{
    public interface IBaseRespository<T>
    {
        Task<bool> Create(T entity);

        Task<T> Get(string year);

        Task<List<T>> Select();

        Task<bool> Delete(T entity);
        Task<bool> Update(T entity);
        Task<T> GetID(int id);
    }
}
