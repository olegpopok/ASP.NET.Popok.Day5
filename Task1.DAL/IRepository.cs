using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.DAL
{
    public interface IRepository<T>
    {
        IEnumerable<T> LoadAll();
        void SaveAll(IEnumerable<T> items);
    }
}
