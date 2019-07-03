using BooksApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BooksApi.Services
{
    public class BookService
    {
        // create a private field representing list of books.
        private readonly IMongoCollection<Book> _books;

        // Constructor.
        // An IBookstoreDatabaseSettings instance is retrieved from DI via constructor injection.
        public BookService(IBookstoreDatabaseSettings settings)
        {
            // inflate to object connection settings.
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);

        }

        // getter all  books.
        public List<Book> Get() => _books.Find(book => true).ToList();

        // Get a particular book
        public Book Get(string id ) => _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        // create a new book
        public  Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        //Replace
        public void Update(string id, Book bookIn ) => _books.ReplaceOne(book => book.Id ==id, bookIn);

        //Remove 
        public void Remove(Book bookIn) => _books.DeleteOne(book => book.Id == bookIn.Id);

        //Remove by string id.
        public void Remove(string id) =>  _books.DeleteOne(book => book.Id == id);

    }


}
