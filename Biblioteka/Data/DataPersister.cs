using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Configuration;
using Biblioteka.Models;
using Biblioteka.HelperClasses;
using System.Xml;

namespace Biblioteka.Data
{
    static public class DataPersister
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;
        public static List<string> takenBooksSerials = GetTakenBooksSerials();
        public static List<string> archivedBookSerials = GetArchivedBooksSerials();

        #region Readers
        static public Reader ParseReader(OleDbDataReader dbReader)
        {
            string egn = dbReader["EGN"].ToString();
            string name = dbReader["ReaderName"].ToString();
            string serialNumber = dbReader["SerialNumber"].ToString();
            string address = dbReader["Address"].ToString();
            int classNo = Int32.Parse(dbReader["ClassNo"].ToString());
            int paralIndex = Int32.Parse(dbReader["Paralelka"].ToString());
            DateTime date = DateTime.Parse(dbReader["DateOfCreation"].ToString());
            Reader newReader = new Reader(egn, name, address, serialNumber, classNo, paralIndex, date);
            return newReader;
        }
        static public bool DatabaseContainsEGN(string egn)
        {
            string query = "SELECT * FROM Readers WHERE EGN = @egn";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@egn", egn);
                    con.Open();
                    var reader = command.ExecuteReader();
                    if (!reader.HasRows) return false;
                    else return true;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        static public List<Reader> GetAllReaders()
        {
            string query = "SELECT * FROM Readers ORDER BY ClassNo";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<Reader> readers = new List<Reader>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Reader newReader = ParseReader(reader);
                            readers.Add(newReader);
                        }
                    }
                    return readers;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        static public Reader GetReaderByEGN(string egn)
        {
            string query = "SELECT * FROM Readers WHERE EGN = @egn";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, con);
                command.Parameters.AddWithValue("@egn", egn);
                con.Open();
                var dbReader = command.ExecuteReader();
                if (dbReader.HasRows)
                {
                    while (dbReader.Read())
                    {
                        Reader r = ParseReader(dbReader);
                        return r;
                    }
                }
                return null;
            }
        }
        static public List<Reader> GetReaderByClass(int classNo, int paralelka)
        {
            string query = "SELECT * FROM Readers WHERE ClassNo = @classNo AND Paralelka = @paralelka";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@classNo", classNo);
                    command.Parameters.AddWithValue("@paralelka", paralelka);
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<Reader> students = new List<Reader>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Reader stud = ParseReader(reader);
                            students.Add(stud);
                        }
                    }
                    return students;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        #endregion

        #region Books
        static public List<Book> GetAllBooks()
        {
            string query = "SELECT * FROM Books ORDER BY SerialNumber";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<Book> books = new List<Book>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string serial = reader["SerialNumber"].ToString();
                            string author = reader["Author"].ToString();
                            string title = reader["Title"].ToString();
                            string price = reader["Price"].ToString();
                            string year = reader["PublishedYear"].ToString();
                            int genre = Int32.Parse(reader["Genre"].ToString());
                            Book book = new Book(serial, author, title, price, year, genre);
                            books.Add(book);
                        }
                    }
                    return books;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        static public List<Book> GetTop100Books()
        {
            string query = "SELECT TOP 100 * FROM Books ORDER BY SerialNumber";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<Book> books = new List<Book>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string serial = reader["SerialNumber"].ToString();
                            string author = reader["Author"].ToString();
                            string title = reader["Title"].ToString();
                            string price = reader["Price"].ToString();
                            string year = reader["PublishedYear"].ToString();
                            int genre = Int32.Parse(reader["Genre"].ToString());
                            Book book = new Book(serial, author, title, price, year, genre);
                            books.Add(book);
                        }
                    }
                    return books;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        static public bool DatabaseContainsSerialNumber(string number, string tableName)
        {
            string query = "SELECT * FROM Readers WHERE SerialNumber = @serialNumber";
            string query1 = "SELECT * FROM Books WHERE SerialNumber = @serialNumber";
            string finalQuery = "";
            if (tableName.Equals("Readers"))
            {
                finalQuery = query;
            }
            else if (tableName.Equals("Books"))
            {
                finalQuery = query1;
            }

            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(finalQuery, con);
                    command.Parameters.AddWithValue("@serialNumber", number);
                    con.Open();
                    var reader = command.ExecuteReader();
                    if (!reader.HasRows) return false;
                    else return true;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        static public bool BookIsTaken(string serial)
        {
            string query = "SELECT * FROM TakenBooks WHERE BookSerialNumber = @serialNumber";

            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@serialNumber", Int32.Parse(serial));
                    con.Open();
                    var reader = command.ExecuteReader();
                    if (!reader.HasRows) return false;
                    else return true;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        static public Book GetBookBySerialNumber(string serial)
        {
            string query = "SELECT * FROM Books WHERE SerialNumber = @serial";
            try
            {
                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@serial", Int32.Parse(serial));
                    con.Open();
                    var dbReader = command.ExecuteReader();
                    if (dbReader.HasRows)
                    {
                        while (dbReader.Read())
                        {
                            string author = dbReader["Author"].ToString();
                            string title = dbReader["Title"].ToString();
                            string price = dbReader["Price"].ToString();
                            string year = dbReader["PublishedYear"].ToString();
                            int genre = Int32.Parse(dbReader["Genre"].ToString());
                            Book b = new Book(serial, author, title, price, year, genre);
                            return b;
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException();
            }
        }
        static public List<Book> GetBookByGenre(int genre)
        {
            string query = "SELECT * FROM Books WHERE Genre = @genre";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@genre", genre);
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<Book> books = new List<Book>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string serial = reader["SerialNumber"].ToString();
                            string author = reader["Author"].ToString();
                            string title = reader["Title"].ToString();
                            string price = reader["Price"].ToString();
                            string year = reader["PublishedYear"].ToString();
                            Book book = new Book(serial, author, title, price, year, genre);
                            books.Add(book);
                        }
                    }
                    return books;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        #endregion

        #region Taken And Returned Books
        static public Reader GetTakenBookReaderInfoBySerialNumber(string serial)
        {
            string query = "SELECT * FROM TakenBooks t INNER JOIN Readers r ON t.ReaderEGN = r.EGN WHERE BookSerialNumber = @serial";
            try
            {
                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@serial", Int32.Parse(serial));
                    con.Open();
                    var dbReader = command.ExecuteReader();
                    if (dbReader.HasRows)
                    {
                        while (dbReader.Read())
                        {
                            Reader r = ParseReader(dbReader);
                            return r;
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException();
            }
        }
        public static List<TakenBookRecord> GetAllTakenBooks()
        {
            string query = "SELECT * FROM TakenBooks t INNER JOIN Books b ON t.BookSerialNumber = b.SerialNumber";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<TakenBookRecord> books = new List<TakenBookRecord>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = Int32.Parse(reader["ID"].ToString());
                            string egn = reader["ReaderEGN"].ToString();
                            Reader r = GetReaderByEGN(egn);
                            string serialNumber = reader["SerialNumber"].ToString();
                            string author = reader["Author"].ToString();
                            string title = reader["Title"].ToString();
                            int genre = Int32.Parse(reader["Genre"].ToString());
                            DateTime d1 = DateTime.Parse(reader["DateOfTaking"].ToString());
                            DateTime d2 = DateTime.Parse(reader["Deadline"].ToString());
                            TakenBookRecord newBook = new TakenBookRecord(id, egn, serialNumber, d1, d2, author, title, "", genre, r.Name, r.ClassAndParalelka);
                            books.Add(newBook);
                        }
                    }
                    return books;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        internal static List<TakenBookRecord> GetAllExpiredTakenBooks()
        {
            string query = "SELECT * FROM TakenBooks t INNER JOIN Books b ON t.BookSerialNumber = b.SerialNumber WHERE @today > t.Deadline";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@today", DateTime.Today.Date);
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<TakenBookRecord> books = new List<TakenBookRecord>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = Int32.Parse(reader["ID"].ToString());
                            string egn = reader["ReaderEGN"].ToString();
                            Reader r = GetReaderByEGN(egn);
                            string serialNumber = reader["SerialNumber"].ToString();
                            string author = reader["Author"].ToString();
                            string title = reader["Title"].ToString();
                            int genre = Int32.Parse(reader["Genre"].ToString());
                            DateTime d1 = DateTime.Parse(reader["DateOfTaking"].ToString());
                            DateTime d2 = DateTime.Parse(reader["Deadline"].ToString());
                            TakenBookRecord newBook = new TakenBookRecord(id, egn, serialNumber, d1, d2, author, title, "", genre, r.Name, r.ClassAndParalelka);
                            books.Add(newBook);
                        }
                    }
                    return books;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        static public List<TakenBookRecord> GetTakenBooksByEGN(string egn)
        {
            string query = "SELECT * FROM TakenBooks t INNER JOIN Books b ON t.BookSerialNumber = b.SerialNumber WHERE ReaderEGN = @egn";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@egn", egn);
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<TakenBookRecord> books = new List<TakenBookRecord>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = Int32.Parse(reader["ID"].ToString());
                            string serialNumber = reader["SerialNumber"].ToString();
                            string author = reader["Author"].ToString();
                            string title = reader["Title"].ToString();
                            int genre = Int32.Parse(reader["Genre"].ToString());
                            DateTime d1 = DateTime.Parse(reader["DateOfTaking"].ToString());
                            DateTime d2 = DateTime.Parse(reader["Deadline"].ToString());
                            TakenBookRecord newBook = new TakenBookRecord(id, egn, serialNumber, d1, d2, author, title, "", genre, "", "");
                            books.Add(newBook);
                        }
                    }
                    return books;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        internal static List<ReturnedBookRecord> GetArchivedReturnedBooks(string egn)
        {
            string query = "SELECT * FROM ReturnedBooks t INNER JOIN ArchivedBooks b ON t.BookSerialNumber = b.BookSerialNumber WHERE ReaderEGN = @egn";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@egn", egn);
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<ReturnedBookRecord> books = new List<ReturnedBookRecord>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = Int32.Parse(reader["ID"].ToString());
                            string serialNumber = reader["b.BookSerialNumber"].ToString();
                            string author = reader["Author"].ToString();
                            string title = reader["Title"].ToString();
                            int genre = Int32.Parse(reader["Genre"].ToString());
                            DateTime d1 = DateTime.Parse(reader["DateOfTaking"].ToString());
                            DateTime d2 = DateTime.Parse(reader["DateOfReturn"].ToString());
                            bool onTime = Boolean.Parse(reader["OnTime"].ToString());

                            ReturnedBookRecord newBook = new ReturnedBookRecord(id, egn, serialNumber, d1, d2, onTime, author, title, "", genre);
                            books.Add(newBook);
                        }
                    }
                    return books;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        static public List<ReturnedBookRecord> GetReturnedBooksByEGN(string egn)
        {
            string query = "SELECT * FROM ReturnedBooks t INNER JOIN Books b ON t.BookSerialNumber = b.SerialNumber WHERE ReaderEGN = @egn";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@egn", egn);
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<ReturnedBookRecord> books = new List<ReturnedBookRecord>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = Int32.Parse(reader["ID"].ToString());
                            string serialNumber = reader["SerialNumber"].ToString();
                            string author = reader["Author"].ToString();
                            string title = reader["Title"].ToString();
                            int genre = Int32.Parse(reader["Genre"].ToString());
                            DateTime d1 = DateTime.Parse(reader["DateOfTaking"].ToString());
                            DateTime d2 = DateTime.Parse(reader["DateOfReturn"].ToString());
                            bool onTime = Boolean.Parse(reader["OnTime"].ToString());

                            ReturnedBookRecord newBook = new ReturnedBookRecord(id, egn, serialNumber, d1, d2, onTime, author, title, "", genre);
                            books.Add(newBook);
                        }
                    }
                    return books;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        #endregion

        #region ArchivedBooks
        static public bool ArchivedBooksContainArchiveNumber(string n)
        {
            string query = "SELECT * FROM ArchivedBooks WHERE ArchiveNumber = @number";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@number", Int32.Parse(n));
                    con.Open();
                    var reader = command.ExecuteReader();
                    if (!reader.HasRows) return false;
                    else return true;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        static public bool ArchivedBooksContainBookSeralNumber(string n)
        {
            string query = "SELECT * FROM ArchivedBooks WHERE BookSerialNumber = @number";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@number", Int32.Parse(n));
                    con.Open();
                    var reader = command.ExecuteReader();
                    if (!reader.HasRows) return false;
                    else return true;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        internal static ArchivedBook GetArchivedBookBySerialNumber(string serial)
        {
            string query = "SELECT * FROM ArchivedBooks WHERE BookSerialNumber = @serial";
            try
            {
                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@serial", Int32.Parse(serial));
                    con.Open();
                    var dbReader = command.ExecuteReader();
                    if (dbReader.HasRows)
                    {
                        while (dbReader.Read())
                        {
                            string author = dbReader["Author"].ToString();
                            string title = dbReader["Title"].ToString();
                            string price = dbReader["Price"].ToString();
                            string year = dbReader["PublishedYear"].ToString();
                            int genre = Int32.Parse(dbReader["Genre"].ToString());
                            string archiveNumber = dbReader["ArchiveNumber"].ToString();
                            DateTime date = DateTime.Parse(dbReader["DateOfArchivation"].ToString());
                            ArchivedBook b = new ArchivedBook(serial, author, title, price, year, genre, archiveNumber, date);
                            return b;
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException();
            }
        }
        internal static List<ArchivedBook> GetAllArchivedBooks()
        {
            string query = "SELECT * FROM ArchivedBooks";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    con.Open();
                    var dbReader = command.ExecuteReader();
                    List<ArchivedBook> books = new List<ArchivedBook>();
                    if (dbReader.HasRows)
                    {
                        while (dbReader.Read())
                        {
                            string serial = dbReader["BookSerialNumber"].ToString();
                            string author = dbReader["Author"].ToString();
                            string title = dbReader["Title"].ToString();
                            string price = dbReader["Price"].ToString();
                            string year = dbReader["PublishedYear"].ToString();
                            int genre = Int32.Parse(dbReader["Genre"].ToString());
                            string archiveNumber = dbReader["ArchiveNumber"].ToString();
                            DateTime date = DateTime.Parse(dbReader["DateOfArchivation"].ToString());
                            ArchivedBook b = new ArchivedBook(serial, author, title, price, year, genre, archiveNumber, date);
                            books.Add(b);
                        }
                    }
                    return books;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        internal static List<ArchivedBook> GetArchivedBooksByGenre(int genre)
        {
            string query = "SELECT * FROM ArchivedBooks WHERE Genre = @genre";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@genre", genre);
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<ArchivedBook> books = new List<ArchivedBook>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string serial = reader["BookSerialNumber"].ToString();
                            string author = reader["Author"].ToString();
                            string title = reader["Title"].ToString();
                            string price = reader["Price"].ToString();
                            string year = reader["PublishedYear"].ToString();
                            string archiveNumber = reader["ArchiveNumber"].ToString();
                            DateTime date = DateTime.Parse(reader["DateOfArchivation"].ToString());
                            ArchivedBook book = new ArchivedBook(serial, author, title, price, year, genre, archiveNumber, date);
                            books.Add(book);
                        }
                    }
                    return books;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        #endregion

        #region Students
        static public List<Student> GetAllStudents()
        {
            string query = "SELECT * FROM AllStudents";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<Student> students = new List<Student>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string egn = reader["EGN"].ToString();
                            string name = reader["StudentName"].ToString();
                            int classNo = Int32.Parse(reader["ClassNo"].ToString());
                            string paral = reader["Paralelka"].ToString();
                            string address = reader["Address"].ToString();
                            Student stud = new Student(egn, name, classNo, ParalelkiTools.NumberFromChar(paral), address);
                            students.Add(stud);
                        }
                    }
                    return students;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        static public List<Student> GetStudentByClass(int classNo, int paralelka)
        {
            string query = "SELECT * FROM AllStudents WHERE ClassNo = @classNo AND Paralelka = @paralelka";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@classNo", classNo);
                    command.Parameters.AddWithValue("@paralelka", ParalelkiTools.CharFromNumber(paralelka));
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<Student> students = new List<Student>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string egn = reader["EGN"].ToString();
                            string name = reader["StudentName"].ToString();
                            string address = reader["Address"].ToString();
                            Student stud = new Student(egn, name, classNo, paralelka, address);
                            students.Add(stud);
                        }
                    }
                    return students;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        static public Student GetStudentByEGN(string egn)
        {
            string query = "SELECT * FROM AllStudents WHERE EGN = @egn";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, con);
                command.Parameters.AddWithValue("@egn", egn);
                con.Open();
                var dbReader = command.ExecuteReader();
                if (dbReader.HasRows)
                {
                    while (dbReader.Read())
                    {
                        string name = dbReader["StudentName"].ToString();
                        int classNo = Int32.Parse(dbReader["ClassNo"].ToString());
                        string paral = dbReader["Paralelka"].ToString();
                        string address = dbReader["Address"].ToString();
                        Student stud = new Student(egn, name, classNo, ParalelkiTools.NumberFromChar(paral), address);
                        return stud;
                    }
                }
                return null;
            }
        }
        #endregion


        internal static List<BookReader> GetBookReadersBySerialNumber(string serial)
        {
            string query = "SELECT * FROM ReturnedBooks t INNER JOIN Readers r ON t.ReaderEGN = r.EGN WHERE BookSerialNumber = @serial";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@serial", Int32.Parse(serial));
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<BookReader> bookReaders = new List<BookReader>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string egn = reader["EGN"].ToString();
                            string name = reader["ReaderName"].ToString();
                            int classNo = Int32.Parse(reader["ClassNo"].ToString());
                            int paralIndex = Int32.Parse(reader["Paralelka"].ToString());
                            string classAndParalelka;
                            if(classNo == 0) classAndParalelka = "Учител";
                            else classAndParalelka = classNo + HelperClasses.ParalelkiTools.CharFromNumber(paralIndex);

                            DateTime dateOfTaking = DateTime.Parse(reader["DateOfTaking"].ToString());
                            DateTime dateOfReturn = DateTime.Parse(reader["DateOfReturn"].ToString());

                            BookReader newBook = new BookReader(egn, name, classAndParalelka, dateOfTaking, dateOfReturn);
                            bookReaders.Add(newBook);
                        }
                    }
                    return bookReaders;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        static public List<Paralelka> GetAllParalelki()
        {
            string query = "SELECT * FROM Paralelki ORDER BY Index";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<Paralelka> paralelki = new List<Paralelka>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int index = Int32.Parse(reader["Index"].ToString());
                            string character = reader["Character"].ToString();
                            Paralelka par = new Paralelka(index, character);
                            paralelki.Add(par);
                        }
                    }
                    return paralelki;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        static public List<Genre> GetAllGenres()
        {
            string query = "SELECT * FROM Genres ORDER BY Index";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<Genre> genres = new List<Genre>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int index = Int32.Parse(reader["Index"].ToString());
                            string text = reader["GenreText"].ToString();
                            Genre gen = new Genre(index, text);
                            genres.Add(gen);
                        }
                    }
                    return genres;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        internal static bool StudentsInParalelka(int p)
        {
            string query = "SELECT * FROM Readers WHERE Paralelka = @p";

            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@p", p);
                    con.Open();
                    var reader = command.ExecuteReader();
                    if (!reader.HasRows) return false;
                    else return true;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        public static SettingRecord GetPropertyFromSettings(string title)
        {
            try
            {
                string xml = System.IO.File.ReadAllText("settings.xml");

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/settings");


                foreach (XmlNode node in nodes)
                {
                    if (node.SelectSingleNode("title").InnerText == title)
                    {
                        SettingRecord rec = new SettingRecord(Int32.Parse(node.SelectSingleNode("id").InnerText), node.SelectSingleNode("title").InnerText, node.SelectSingleNode("value").InnerText);
                        return rec;
                    }
                }
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        internal static List<string> GetTakenBooksSerials()
        {
            string query = "SELECT BookSerialNumber FROM TakenBooks";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<string> serials = new List<string>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string serial = reader["BookSerialNumber"].ToString();
                            serials.Add(serial);
                        }
                    }
                    return serials;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
        internal static List<string> GetArchivedBooksSerials()
        {
            string query = "SELECT BookSerialNumber FROM ArchivedBooks";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    con.Open();
                    var reader = command.ExecuteReader();
                    List<string> serials = new List<string>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string serial = reader["BookSerialNumber"].ToString();
                            serials.Add(serial);
                        }
                    }
                    return serials;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }


        #region Readers Stats
        internal static int GetReadersCount()
        {
            string query = "SELECT COUNT(*) FROM Readers";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    con.Open();
                    int count = Int32.Parse(command.ExecuteScalar().ToString());
                    return count;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
        public static List<StatsReader> GetTopReaders(DateTime startDate, DateTime finalDate, int top)
        {
            List<StatsReader> returnedBooks = GetReadersFromReturnedBooks(startDate, finalDate);
            List<StatsReader> takenBooks = GetReadersFromTakenBooksInPeriod(startDate, finalDate);
            List<StatsReader> newList = UniteReadersLists(returnedBooks, takenBooks);
            newList = newList.OrderByDescending(o=>o.NumberOfBooks).ToList();

            List<StatsReader> finalList = new List<StatsReader>();
            if(newList.Count > 1)
            {
                int prev = -1;
                int index = 0;
                int current = newList[index].NumberOfBooks;
                int pos = 0;
                foreach(var item in newList)
                {
                    if(current != prev)
                    {
                        pos++;
                        prev = current;
                        if (pos > top) return finalList;
                    }
                    item.Position = pos;
                    finalList.Add(item);
                    if (newList.Count > index + 1) index++;
                    current = newList[index].NumberOfBooks;
                }
            }
            return newList;
        }
        public static List<StatsReader> UniteReadersLists(List<StatsReader> returnedBooks, List<StatsReader> takenBooks)
        {
            List<StatsReader> newList = new List<StatsReader>();
            foreach (var returnedB in returnedBooks)
            {
                var reader = returnedB;
                foreach (var takenB in takenBooks)
                {
                    if (returnedB.EGN.Equals(takenB.EGN))
                    {
                        reader.NumberOfBooks += takenB.NumberOfBooks;
                        break;
                    }
                }
                newList.Add(reader);
            }

            foreach (var item in takenBooks)
            {
                bool found = false;
                foreach (var item2 in returnedBooks)
                {
                    if (item.EGN.Equals(item2.EGN)) found = true;
                }
                if (!found) newList.Add(item);
            }
            return newList;
        }
        private static List<StatsReader> GetReadersFromTakenBooksInPeriod(DateTime startDate, DateTime finalDate)
        {
            //Get readers that have taken a book in period
            string query = "SELECT t.ReaderEGN, Count(b.SerialNumber) AS CountOfSerialNumber " +
                "FROM TakenBooks AS t INNER JOIN Books AS b ON t.BookSerialNumber = b.SerialNumber " +
                "WHERE DateOfTaking >= @startDate AND DateOfTaking <= @finalDate " +
                "GROUP BY t.ReaderEGN " +
                "ORDER BY 2 DESC";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    List<StatsReader> readers = new List<StatsReader>();
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@finalDate", finalDate);
                    con.Open();
                    var dbReader = command.ExecuteReader();
                    if (dbReader.HasRows)
                    {
                        while (dbReader.Read())
                        {
                            string egn = dbReader["ReaderEGN"].ToString();
                            int books = Int32.Parse(dbReader["CountOfSerialNumber"].ToString());
                            Reader r = GetReaderByEGN(egn);
                            StatsReader sr = new StatsReader(r.EGN, r.Name, r.SerialNumber, r.ClasNo, r.Paralelka, books);
                            readers.Add(sr);
                        }
                    }
                    return readers;
                }
                catch (Exception ex)
                {
                    return new List<StatsReader>();
                }
            }
        }
        internal static List<StatsReader> GetReadersFromReturnedBooks(DateTime startDate, DateTime finalDate)
        {
            string query = "SELECT t.ReaderEGN, Count(b.SerialNumber) AS CountOfSerialNumber " +
                "FROM ReturnedBooks AS t INNER JOIN Books AS b ON t.BookSerialNumber = b.SerialNumber " +
                "WHERE DateOfTaking >= @startDate AND DateOfTaking <= @finalDate " +
                "GROUP BY t.ReaderEGN " +
                "ORDER BY 2 DESC";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    List<StatsReader> readers = new List<StatsReader>();
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@finalDate", finalDate);
                    con.Open();
                    var dbReader = command.ExecuteReader();
                    if (dbReader.HasRows)
                    {
                        while (dbReader.Read())
                        {
                            string egn = dbReader["ReaderEGN"].ToString();
                            int books = Int32.Parse(dbReader["CountOfSerialNumber"].ToString());
                            Reader r = GetReaderByEGN(egn);
                            StatsReader sr = new StatsReader(r.EGN, r.Name, r.SerialNumber, r.ClasNo, r.Paralelka, books);
                            readers.Add(sr);
                        }
                    }
                    return readers;
                }
                catch (Exception ex)
                {
                    return new List<StatsReader>();
                }
            }
        }
        internal static int GetActiveReadersCount(DateTime startDate, DateTime finalDate)
        {
            List<string> takenBooks = DataPersister.GetReadersTakenBooksInPeriod(startDate, finalDate);
            List<string> returnedBooks = DataPersister.GetReadersFromReturnedBooksInPeriod(startDate, finalDate);
            List<string> finalList = new List<string>();

            foreach (var item in takenBooks)
            {
                if (!finalList.Contains(item)) finalList.Add(item);
            }
            foreach (var item in returnedBooks)
            {
                if (!finalList.Contains(item)) finalList.Add(item);
            }

            return finalList.Count;
        }
        private static List<string> GetReadersFromReturnedBooksInPeriod(DateTime startDate, DateTime finalDate)
        {
            string query = "SELECT ReaderEGN FROM ReturnedBooks WHERE DateOfTaking >= @startDate AND DateOfTaking <= @finalDate";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@finalDate", finalDate);
                    con.Open();
                    var dbReader = command.ExecuteReader();
                    List<string> egns = new List<string>();
                    if (dbReader.HasRows)
                    {
                        while (dbReader.Read())
                        {
                            string egn = dbReader["ReaderEGN"].ToString();
                            egns.Add(egn);
                        }
                    }
                    return egns;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        private static List<string> GetReadersTakenBooksInPeriod(DateTime startDate, DateTime finalDate)
        {
            string query = "SELECT ReaderEGN FROM TakenBooks WHERE DateOfTaking >= @startDate AND DateOfTaking <= @finalDate";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@finalDate", finalDate);
                    con.Open();
                    List<string> egns = new List<string>();
                    var dbReader = command.ExecuteReader();
                    if (dbReader.HasRows)
                    {
                        while (dbReader.Read())
                        {
                            string egn = dbReader["ReaderEGN"].ToString();
                            egns.Add(egn);
                        }
                    }
                    return egns;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        #endregion

        #region Books Stats
        internal static int GetBooksCount()
        {
            string query = "SELECT COUNT(*) FROM Books";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    con.Open();
                    int count = Int32.Parse(command.ExecuteScalar().ToString());
                    return count;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
        public static int GetAllTakenBooksCountInPeriod(DateTime startDate, DateTime finalDate)
        {
            int taken = GetTakenBooksCountInPeriod(startDate, finalDate);
            int returned = GetReturnedBooksCountInPeriod(startDate, finalDate);
            return taken + returned;
        }
        public static int GetTakenBooksCountInPeriod(DateTime startDate, DateTime finalDate)
        {
            string query = "SELECT COUNT(*) FROM TakenBooks WHERE DateOfTaking >= @startDate AND DateOfTaking <= @finalDate";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    List<StatsReader> readers = new List<StatsReader>();
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@finalDate", finalDate);
                    con.Open();
                    int count = Int32.Parse(command.ExecuteScalar().ToString());
                    return count;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
        public static int GetReturnedBooksCountInPeriod(DateTime startDate, DateTime finalDate)
        {
            string query = "SELECT COUNT(*) FROM ReturnedBooks WHERE DateOfTaking >= @startDate AND DateOfTaking <= @finalDate";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    List<StatsReader> readers = new List<StatsReader>();
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@finalDate", finalDate);
                    con.Open();
                    int count = Int32.Parse(command.ExecuteScalar().ToString());
                    return count;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
        public static List<StatsBook> GetTopBooks(DateTime startDate, DateTime finalDate, int top)
        {
            List<StatsBook> returnedBooks = GetBooksFromReturnedBooksInPeriod(startDate, finalDate);
            List<StatsBook> takenBooks = GetBooksFromTakenBooksInPeriod(startDate, finalDate);
            List<StatsBook> newList = UniteBooksReadersLists(returnedBooks, takenBooks);
            newList = newList.OrderByDescending(o => o.NumberOfReaders).ToList();

            List<StatsBook> finalList = new List<StatsBook>();
            if (newList.Count > 1)
            {
                int prev = -1;
                int index = 0;
                int current = newList[index].NumberOfReaders;
                int pos = 0;
                foreach (var item in newList)
                {
                    if (current != prev)
                    {
                        pos++;
                        prev = current;
                        if (pos > top) return finalList;
                    }
                    item.Position = pos;
                    finalList.Add(item);
                    if (newList.Count > index + 1) index++;
                    current = newList[index].NumberOfReaders;
                }
            }
            return newList;
        }

        private static List<StatsBook> GetBooksFromTakenBooksInPeriod(DateTime startDate, DateTime finalDate)
        {
            //Get readers that have taken a book in period
            string query = "SELECT t.BookSerialNumber, Count(r.EGN) AS CountOfEGN " +
                "FROM TakenBooks AS t INNER JOIN Readers AS r ON t.ReaderEGN = r.EGN " +
                "WHERE DateOfTaking >= @startDate AND DateOfTaking <= @finalDate " +
                "GROUP BY t.BookSerialNumber " +
                "ORDER BY 2 DESC";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    List<StatsBook> books = new List<StatsBook>();
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@finalDate", finalDate);
                    con.Open();
                    var dbReader = command.ExecuteReader();
                    if (dbReader.HasRows)
                    {
                        while (dbReader.Read())
                        {
                            string serial = dbReader["BookSerialNumber"].ToString();
                            int readers = Int32.Parse(dbReader["CountOfEGN"].ToString());
                            Book r = GetBookBySerialNumber(serial);
                            StatsBook sb = new StatsBook(r.SerialNumber, r.Author, r.Title, readers);
                            books.Add(sb);
                        }
                    }
                    return books;
                }
                catch (Exception ex)
                {
                    return new List<StatsBook>();
                }
            }
        }

        private static List<StatsBook> GetBooksFromReturnedBooksInPeriod(DateTime startDate, DateTime finalDate)
        {
            string query = "SELECT t.BookSerialNumber, Count(r.EGN) AS CountOfEGN " +
                "FROM ReturnedBooks AS t INNER JOIN Readers AS r ON t.ReaderEGN = r.EGN " +
                "WHERE DateOfTaking >= @startDate AND DateOfTaking <= @finalDate " +
                "GROUP BY t.BookSerialNumber " +
                "ORDER BY 2 DESC";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    List<StatsBook> books = new List<StatsBook>();
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@finalDate", finalDate);
                    con.Open();
                    var dbReader = command.ExecuteReader();
                    if (dbReader.HasRows)
                    {
                        while (dbReader.Read())
                        {
                            string serial = dbReader["BookSerialNumber"].ToString();
                            int readers = Int32.Parse(dbReader["CountOfEGN"].ToString());
                            Book r = GetBookBySerialNumber(serial);
                            if (r != null)
                            {
                                StatsBook sb = new StatsBook(r.SerialNumber, r.Author, r.Title, readers);
                                books.Add(sb);
                            }
                        }
                    }
                    return books;
                }
                catch (Exception ex)
                {
                    return new List<StatsBook>();
                }
            }
        }

        private static List<StatsBook> UniteBooksReadersLists(List<StatsBook> returnedBooks, List<StatsBook> takenBooks)
        {
            List<StatsBook> newList = new List<StatsBook>();
            foreach (var returnedB in returnedBooks)
            {
                var book = returnedB;
                foreach (var takenB in takenBooks)
                {
                    if (returnedB.SerialNumber.Equals(takenB.SerialNumber))
                    {
                        book.NumberOfReaders += takenB.NumberOfReaders;
                        break;
                    }
                }
                newList.Add(book);
            }

            foreach (var item in takenBooks)
            {
                bool found = false;
                foreach (var item2 in returnedBooks)
                {
                    if (item.SerialNumber.Equals(item2.SerialNumber)) found = true;
                }
                if (!found) newList.Add(item);
            }
            return newList;
        }
        #endregion

    }
}
