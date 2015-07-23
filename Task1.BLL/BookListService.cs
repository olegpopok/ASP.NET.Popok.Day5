using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.DAL;
using Task1.Entities;
using NLog;

namespace Task1.BLL
{
    public class BookListService
    {
        private readonly Logger logger;

        private IRepository<Book> repository;

        public BookListService()
        {
            repository = new BinaryBookRepository();
            logger = LogManager.GetCurrentClassLogger();
        }

        public void Add(Book book)
        {
            try
            {
                if (IsInRepository(book))
                {
                    throw new InvalidOperationException("This book is in repository!");
                }
                repository.Add(book);
            }
            catch (InvalidOperationException e)
            {
                logger.Info(e.Message);
                logger.Error(e.StackTrace);
            }
        }

        public void Remove(Book book)
        {
            try
            {
                if (IsInRepository(book))
                {
                    repository.Remove(book);
                }
                throw new InvalidOperationException("This book isn't in repository!");
            }
            catch (InvalidOperationException e)
            {
                logger.Info(e.Message);
                logger.Error(e.StackTrace);
            }
        }

        public Book FindByTag(Func<Book,bool> func)
        {
            return repository.GetAll().First(func);
        }

        public void SortBooksByTag(IComparer<Book> comparer = null)
        {
            repository.Sort(comparer);
        }

        private bool IsInRepository(Book book)
        {
            Book temp = repository.GetAll().FirstOrDefault(x => x.Title == book.Title);
            if (temp != null)
            {
                return book.Equals(temp);
            }
            return false;
        }
    }
}
