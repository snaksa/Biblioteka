using Biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.OleDb;
using Biblioteka.HelperClasses;
using System.IO;
using System.Xml;

namespace Biblioteka.Data
{
    static public class DataInserter
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString;

        static public void AddReader(Reader reader)
        {
            string query = "INSERT INTO Readers(EGN, ReaderName, Address, SerialNumber, ClassNo, Paralelka, DateOfCreation) VALUES(?,?,?,?,?,?,?)";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("?", reader.EGN);
                    command.Parameters.AddWithValue("?", reader.Name);
                    command.Parameters.AddWithValue("?", reader.Address);
                    command.Parameters.AddWithValue("?", Int32.Parse(reader.SerialNumber));
                    command.Parameters.AddWithValue("?", reader.ClasNo);
                    command.Parameters.AddWithValue("?", reader.Paralelka);
                    command.Parameters.AddWithValue("?", reader.DateOfCreation);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        static public void AddBook(Book book)
        {
            string query = "INSERT INTO Books(SerialNumber, Author, Title, Price, PublishedYear, Genre) VALUES(?,?,?,?,?,?)";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("?", Int32.Parse(book.SerialNumber));
                    command.Parameters.AddWithValue("?", book.Author);
                    command.Parameters.AddWithValue("?", book.Title);
                    command.Parameters.AddWithValue("?", Double.Parse(book.Price));
                    command.Parameters.AddWithValue("?", book.PublishedYear);
                    command.Parameters.AddWithValue("?", book.Genre);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        static public void DeleteReader(string egn)
        {
            DeleteTakenBooksByEGN(egn);
            DeleteReturnedBooksByEGN(egn);
            string query = "DELETE FROM Readers WHERE EGN = @egn";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@egn", egn);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        static public void DeleteTakenBooksByEGN(string egn)
        {
            string query = "DELETE FROM TakenBooks WHERE ReaderEGN = @egn";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@egn", egn);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        static public void DeleteReturnedBooksByEGN(string egn)
        {
            string query = "DELETE FROM ReturnedBooks WHERE ReaderEGN = @egn";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@egn", egn);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        static public void UpdateReader(Reader reader, string originalEGN)
        {
            string query = "UPDATE Readers SET EGN = @egn, ReaderName = @name, Address = @address, SerialNumber = @serialNumber, ClassNo = @classNo, Paralelka = @par, DateOfCreation = @date WHERE EGN = @searchEGN";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@egn", reader.EGN);
                    command.Parameters.AddWithValue("@name", reader.Name);
                    command.Parameters.AddWithValue("@address", reader.Address);
                    command.Parameters.AddWithValue("@serialNumber", Int32.Parse(reader.SerialNumber));
                    command.Parameters.AddWithValue("@classNo", reader.ClasNo);
                    command.Parameters.AddWithValue("@par", reader.Paralelka);
                    command.Parameters.AddWithValue("@date", reader.DateOfCreation);
                    command.Parameters.AddWithValue("@searchEGN", originalEGN);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        static public void TakeBook(TakenBookRecord record)
        {
            string query = "INSERT INTO TakenBooks(ReaderEGN, BookSerialNumber, DateOfTaking, Deadline) VALUES(@egn, @serialNumber, @dateOfTaking, @deadline)";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@egn", record.ReaderEGN);
                    command.Parameters.AddWithValue("@serialNumber", record.SerialNumber);
                    command.Parameters.AddWithValue("@dateOfTaking", record.DateOfTaking);
                    command.Parameters.AddWithValue("@deadline", record.Deadline);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        static public void DeleteTakenBookRecord(int id)
        {
            string query = "DELETE FROM TakenBooks WHERE ID = @id";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@id", id);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        static public void SaveReturnedRecord(ReturnedBookRecord record)
        {
            string query = "INSERT INTO ReturnedBooks(ReaderEGN, BookSerialNumber, DateOfTaking, DateOfReturn, OnTime) VALUES(@egn, @serial, @dateOfTaking, @dateOfReturn, @onTime)";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@egn", record.ReaderEGN);
                    command.Parameters.AddWithValue("@serial", Int32.Parse(record.SerialNumber));
                    command.Parameters.AddWithValue("@dateOfTaking", record.DateOfTaking);
                    command.Parameters.AddWithValue("@dateOfReturn", record.DateOfReturn);
                    string str = record.ReturnedOnTime;
                    bool b = false;
                    if (str.Equals("Да")) b = true;
                    else b = false;
                    command.Parameters.AddWithValue("@onTime", b);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        static public void UpdateReaderReturnedBooks(string egn, string originalEGN)
        {
            string query = "UPDATE ReturnedBooks SET ReaderEGN = @egn WHERE ReaderEGN = @searchEGN";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@egn", egn);
                    command.Parameters.AddWithValue("@searchEGN", originalEGN);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        static public void UpdateReaderTakenBooks(string egn, string originalEGN)
        {
            string query = "UPDATE TakenBooks SET ReaderEGN = @egn WHERE ReaderEGN = @searchEGN";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@egn", egn);
                    command.Parameters.AddWithValue("@searchEGN", originalEGN);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void UpdateBookReturnedBooks(string serialNumber, string originalSerialNumber)
        {
            string query = "UPDATE ReturnedBooks SET BookSerialNumber = @serial WHERE BookSerialNumber = @originalSerialNumber";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@serial", serialNumber);
                    command.Parameters.AddWithValue("@originalSerialNumber", originalSerialNumber);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void UpdateBookTakenBook(string serialNumber, string originalSerialNumber)
        {
            string query = "UPDATE TakenBooks SET BookSerialNumber = @serial WHERE BookSerialNumber = @originalSerialNumber";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@serial", Int32.Parse(serialNumber));
                    command.Parameters.AddWithValue("@originalSerialNumber", Int32.Parse(originalSerialNumber));

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void UpdateBook(Book b, string originalSerialNumber)
        {
            string query = "UPDATE Books SET SerialNumber = @serial, Author = @author, Title = @title, Price = @price, PublishedYear = @year, Genre = @genre WHERE SerialNumber = @originalSerial";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@serial", Int32.Parse(b.SerialNumber));
                    command.Parameters.AddWithValue("@author", b.Author);
                    command.Parameters.AddWithValue("@title", b.Title);
                    command.Parameters.AddWithValue("@price", Double.Parse(b.Price));
                    command.Parameters.AddWithValue("@year", b.PublishedYear);
                    command.Parameters.AddWithValue("@genre", b.Genre);
                    command.Parameters.AddWithValue("@originalSerial", originalSerialNumber);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void DeleteBookBySerialNumber(string serial)
        {
            string query = "DELETE FROM Books WHERE SerialNumber = @serial";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@serial", Int32.Parse(serial));
                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void ArchiveBook(Book b, string number, DateTime date)
        {
            string query = "INSERT INTO ArchivedBooks(BookSerialNumber, Author, Title, Price, PublishedYear, Genre, ArchiveNumber, DateOfArchivation)" +
            "VALUES(@bookSerial, @author, @title, @price, @year, @genre, @archiveNumber, @date)";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@bookSerial", Int32.Parse(b.SerialNumber));
                    command.Parameters.AddWithValue("@author", b.Author);
                    command.Parameters.AddWithValue("@title", b.Title);
                    command.Parameters.AddWithValue("@price", Double.Parse(b.Price));
                    command.Parameters.AddWithValue("@year", b.PublishedYear);
                    command.Parameters.AddWithValue("@genre", b.Genre);
                    command.Parameters.AddWithValue("@archiveNumber", Int32.Parse(number));
                    command.Parameters.AddWithValue("@date", date);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void UpdateArchivedBook(Book b, string originalSerialNumber)
        {
            string query = "UPDATE ArchivedBooks SET BookSerialNumber = @serial, Author = @author, Title = @title, Price = @price, PublishedYear = @year, Genre = @genre WHERE BookSerialNumber = @originalSerial";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@serial", Int32.Parse(b.SerialNumber));
                    command.Parameters.AddWithValue("@author", b.Author);
                    command.Parameters.AddWithValue("@title", b.Title);
                    command.Parameters.AddWithValue("@price", Double.Parse(b.Price));
                    command.Parameters.AddWithValue("@year", b.PublishedYear);
                    command.Parameters.AddWithValue("@genre", b.Genre);
                    command.Parameters.AddWithValue("@originalSerial", originalSerialNumber);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void DeleteArchivedBook(string serial)
        {
            string query = "DELETE FROM ArchivedBooks WHERE BookSerialNumber = @serial";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@serial", Int32.Parse(serial));
                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void RenameGenre(int index, string text)
        {
            string query = "UPDATE Genres SET GenreText = @text WHERE Index = @index";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@text", text);
                    command.Parameters.AddWithValue("@index", index);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void AddGenre(string text, int index)
        {
            string query = "INSERT INTO Genres([Index], GenreText) VALUES(@index, @text)";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@index", index);
                    command.Parameters.AddWithValue("@text", text);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void DecreaseGenresInAllTables(int genre)
        {
            string[] query = 
            {
                "UPDATE Books SET Genre = 0 WHERE Genre = @ind ",
                "UPDATE ArchivedBooks SET Genre = 0 WHERE Genre = @ind ",
                "UPDATE Genres SET [Index] = [Index] - 1 WHERE [Index] > @ind ",
                "UPDATE Books SET Genre = Genre - 1 WHERE Genre > @ind ",
                "UPDATE ArchivedBooks SET Genre = Genre - 1 WHERE Genre > @ind "
            };
            foreach (var item in query)
            {
                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    try
                    {
                        OleDbCommand command = new OleDbCommand(item, con);

                        command.Parameters.AddWithValue("@ind", genre);

                        con.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException();
                    }
                }
            }
        }

        internal static void DeleteGenre(int genre)
        {
            string query = "DELETE FROM Genres WHERE [Index] = @ind";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@ind", genre);
                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void RenameParalelka(int index, string text)
        {
            string query = "UPDATE Paralelki SET [Character] = @text WHERE Index = @index";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@text", text);
                    command.Parameters.AddWithValue("@index", index);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void AddParalelka(string text, int index)
        {
            string query = "INSERT INTO Paralelki([Index], [Character]) VALUES(@index, @text)";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@index", index);
                    command.Parameters.AddWithValue("@text", text);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void DeleteParalelka(int index)
        {
            string query = "DELETE FROM Paralelki WHERE [Index] = @ind";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    command.Parameters.AddWithValue("@ind", index);
                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void DecreaseParalelki(int index)
        {
            string query = "UPDATE Paralelki SET [Index] = [Index] - 1 WHERE [Index] > @ind ";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);

                    command.Parameters.AddWithValue("@ind", index);

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void UpdateSetting(int id, string settingValue)
        {
            string xml = System.IO.File.ReadAllText("settings.xml");
            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/settings");


            foreach (XmlNode node in nodes)
            {
                if(node.SelectSingleNode("id").InnerText == id.ToString())
                {
                    node.SelectSingleNode("value").InnerText = settingValue;
                    doc.Save("settings.xml");
                    return;
                }
            }
        }

        internal static void UpdateReaderClassNo()
        {
            string query = "UPDATE Readers SET ClassNo = ClassNo + 1 WHERE ClassNo >= 1";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void UpdateAllStudentsClassNo()
        {
            string query = "UPDATE AllStudents SET ClassNo = ClassNo + 1";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, con);
                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }

        internal static void DeleteGraduatedReadersAndStudents()
        {
            string[] query =
            {
                "DELETE FROM Readers WHERE ClassNo = 13",
                "DELETE FROM AllStudents WHERE ClassNo = 13"
            };
            foreach (var item in query)
            {
                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    try
                    {
                        OleDbCommand command = new OleDbCommand(item, con);
                        con.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException();
                    }
                }
            }
        }
    }
}
