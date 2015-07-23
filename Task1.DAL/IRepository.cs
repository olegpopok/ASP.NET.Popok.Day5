using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.DAL
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        void Add(T item);
        void Remove(T item);
        void Sort(IComparer<T> comparer);
    }
}
