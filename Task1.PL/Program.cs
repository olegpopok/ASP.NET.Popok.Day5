using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Entities;
using Task1.BLL;
namespace Task1.PL
{
    class Program
    {
        static void Main(string[] args)
        {
            BookListService bookService = new BookListService();
            Book b = new Book();
            bookService.Add(b);
            bookService.Add(b);
            bookService.Remove(b);
            Console.ReadKey();
        }
    }
}
