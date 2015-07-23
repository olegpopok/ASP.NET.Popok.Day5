using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Entities
{
    public class Book : IEquatable<Book>, IComparable<Book>
    {
        private int yearOfPublish;
        private int pages;

        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int YearOfPublish 
        {
            get
            {
                return yearOfPublish;
            }
            set
            {
                if ((value < 868) && (value > DateTime.Now.Year))
                {
                    throw new ArgumentException("Year of publich cannot be less than 868 or cannot be more than current year");
                }

                yearOfPublish = value;
            }
        }
        public int Pages 
        {
            get 
            {
                return pages;
            }
            set 
            {
                if (value < 10)
                {
                    throw new ArgumentException("In book cannot be less than ten pages");
                }
                pages = value;
            }
        }

        public Book() :this("author", "title", "description", 10, 868)
        { }

        public Book(string title, string author, string description, int pages, int yearOfPublish)
        {
            Title = title;
            Author = author;
            Description = description;
            Pages = pages;
            YearOfPublish = yearOfPublish;
        }

        public bool Equals(Book other)
        {
            if (other == null)
            {
                return false;
            }

            if (this == other)
            {
                return true;
            }

            if ((String.Compare(this.Author,other.Author, false) == 0) && (String.Compare(this.Title,other.Title, false) == 0) && 
                (this.YearOfPublish == other.YearOfPublish) && (this.Pages == other.Pages))
            {
                return true;
            }

            return false;
        }

        public int CompareTo(Book other)
        {
            if (other == null)
            {
                return 1;
            }

            if (this.Equals(other))
            {
                return 0;
            }

            if (String.Compare(this.Title, other.Title, false) > 0)
            {
                return 1;
            }

            return -1;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (this == obj)
            {
                return true;
            }

            Book book = obj as Book;
            if (book == null)
            {
                return false;
            }

            return  this.Equals(book);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                uint sumGhcOfProperties = (uint)Author.GetHashCode()+ (uint)Title.GetHashCode() + 
                    (uint)YearOfPublish.GetTypeCode() + (uint)Pages.GetTypeCode();
                return (int)(sumGhcOfProperties % (uint)2971215073);
            }
        }

        public override string ToString()
        {
            return String.Format("Title: {0}. Author: {1}. Description: {2}. Year of publish: {3}. Pages: {4}.",
                Title, Author, Description, YearOfPublish, Pages);
        }
    }
}
