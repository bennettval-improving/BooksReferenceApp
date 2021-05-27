using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Books.ViewModels.Books;
using Microsoft.Extensions.Configuration;

namespace Books.Models
{
    public class BookData
    {
        private readonly IConfiguration _configuration;

        public BookData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Book> GetBookData()
        {
            var books = new List<Book>();

            using (var conn = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                conn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Books";

                var reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    var title = reader["Title"].ToString();
                    var id = Convert.ToInt32(reader["BookId"]);
                    books.Add(new Book
                    {
                        BookId = id,
                        Title = title
                    });
                }
            }

            return books;
        }

        public BookViewModel GetBook(int id)
        {
            var book = new BookViewModel();

            using (var conn = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                conn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Books WHERE BookId = @id";
                cmd.Parameters.Add(new SqlParameter
                {
                    Value = id,
                    ParameterName = "@id",
                    SqlDbType = SqlDbType.Int
                });

                var reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    var title = reader["Title"].ToString();
                    book.BookId = id;
                    book.Title = title;
                }
            }

            return book;
        }

        public void CreateBook(Book book)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                conn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into Books (Title) Values (@title)";
                cmd.Parameters.Add(new SqlParameter
                {
                    Value = book.Title,
                    ParameterName = "@title",
                    SqlDbType = SqlDbType.VarChar
                });

                cmd.ExecuteNonQuery();
            }
        }
    }
}
