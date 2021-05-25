using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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
                cmd.CommandType = System.Data.CommandType.Text;
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

        //public Book GetBook(int id)
        //{
            
        //}

        
    }
}
