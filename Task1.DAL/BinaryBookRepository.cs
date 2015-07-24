using System;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using Task1.Entities;

namespace Task1.DAL
{
    public class BinaryBookRepository : IRepository<Book> 
    {
        private readonly string path;
        private List<Book> books;

        public BinaryBookRepository()
        {
            string baseFolder = AppDomain.CurrentDomain.BaseDirectory;
            path = Path.Combine(baseFolder, "books");
            books = new List<Book>();
        }

        public void Add(Book book)
        {
            LoadBooksHelper();
            books.Add(book);
            SaveBooksHelper();
        }

        public void Remove(Book book)
        {
            LoadBooksHelper();
            books.Remove(book);
            SaveBooksHelper();
        }

        public IEnumerable<Book> GetAll()
        {
            LoadBooksHelper();
            
            return books.AsEnumerable();
        }

        public void Sort(IComparer<Book> comparer)
        {
            LoadBooksHelper();
            books.Sort(comparer);
            SaveBooksHelper();
        }

        private void LoadBooksHelper()
        {
            books.Clear();
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Open))
                {
                    BinaryReader reader = new BinaryReader(stream);

                    while (stream.Position < stream.Length)
                    {
                        Book book = new Book();
                        book.Title = reader.ReadString();
                        book.Author = reader.ReadString();
                        book.Description = reader.ReadString();
                        book.Pages = reader.ReadInt32();
                        book.YearOfPublish = reader.ReadInt32();
                        books.Add(book);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                throw new Exception("Repository is empty!", e);
            }
            catch (IOException e)
            {
                throw new Exception("Load error!", e);
            }
        }

        private void SaveBooksHelper()
        {
            using (Stream stream = new FileStream(path, FileMode.Create))
            {

                BinaryWriter writer = new BinaryWriter(stream);
                foreach (Book book in books)
                {
                    writer.Write(book.Title);
                    writer.Write(book.Author);
                    writer.Write(book.Description);
                    writer.Write(book.Pages);
                    writer.Write(book.YearOfPublish);
                }
                writer.Flush();
            }
        }
    }
}
