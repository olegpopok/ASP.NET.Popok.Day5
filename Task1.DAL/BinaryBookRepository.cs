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

        public BinaryBookRepository()
        {
            string baseFolder = AppDomain.CurrentDomain.BaseDirectory;
            path = Path.Combine(baseFolder, "books");
        }

        public IEnumerable<Book> LoadAll()
        {
            List<Book> books = new List<Book>();
            try
            {
                using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
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
            catch (FileNotFoundException)
            {
                throw new InvalidOperationException("Repository is not found!");
            }
            catch (IOException)
            {
                throw new InvalidOperationException("Load error!");
            }
            return books.AsQueryable();
        }

        public void SaveAll(IEnumerable<Book> books)
        {
            if (books == null)
            {
                throw new ArgumentNullException();
            }

            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Create(path)))
                {
                    foreach (Book book in books)
                    {
                        writer.Write(book.Title);
                        writer.Write(book.Author);
                        writer.Write(book.Description);
                        writer.Write(book.Pages);
                        writer.Write(book.YearOfPublish);
                    }
                }
            }
            catch (IOException)
            {
                throw new InvalidOperationException("Save error!");
            }
        }
    }
}
