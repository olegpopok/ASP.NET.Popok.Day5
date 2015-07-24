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
        private List<Book> books;

        public BookListService()
        {
            repository = new BinaryBookRepository();
            logger = LogManager.GetCurrentClassLogger();
            books = new List<Book>();
        }

        public void Add(Book book)
        {
            try
            {
                LoadBooksHelper();
                if (IsInRepository(book))
                {
                    throw new InvalidOperationException("This book is in repository!");
                }
                books.Add(book);
                SaveBooksHelper();
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                logger.Error(e.StackTrace);
            }
        }

        public void Remove(Book book)
        {
            try
            {
                LoadBooksHelper();
                if (!IsInRepository(book))
                {
                    throw new InvalidOperationException("This book isn't in repository!");
                }
                books.Remove(book);
                SaveBooksHelper();
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                logger.Error(e.StackTrace);
            }
        }

        public Book FindByTag(Func<Book,bool> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("tag is null!");
            }

            Book result = null;
            try
            {
                LoadBooksHelper();
                result = books.First(func);
            }
            catch(Exception e)
            {  
                logger.Info(e.Message);
                logger.Error(e.StackTrace);
            }
            return result;
        }

        public void SortBooksByTag(IComparer<Book> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }

            try
            {
                LoadBooksHelper();
                books.Sort(comparer);
                SaveBooksHelper();
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                logger.Error(e.StackTrace);
            }
        }

        public void SortBooksByTag()
        {
            this.SortBooksByTag(Comparer<Book>.Default);
        }

        private bool IsInRepository(Book book)
        {
            Book temp = books.FirstOrDefault(x => x.Equals(book));
            if (temp != null)
            {
                return true;
            }
            return false;
        }

        private void LoadBooksHelper()
        {
            books = repository.LoadAll().ToList();
        }

        private void SaveBooksHelper()
        {
            repository.SaveAll(books);
        }
    }
}
