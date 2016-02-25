using Biblioteka.Commands;
using Biblioteka.Data;
using Biblioteka.Models;
using Biblioteka.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace Biblioteka.ViewModels
{
    public class StartPageViewModel : ViewModelBase
    {
        string pageTitle;
        bool enableApp = true;

        ICommand addReaderToDB;
        ICommand addBookToDB;
        ICommand searchUser;
        ICommand searchBook;
        ICommand takenBooks;
        ICommand settings;
        ICommand stats;

        public StartPageViewModel()
        {
            //SendDatabaseAsAttachment();
            try
            {
                if (CloseApp())
                {
                    this.EnableApp = false;
                }
                string propertyName = "YearOfUpdate";
                SettingRecord record = DataPersister.GetPropertyFromSettings(propertyName);
                if (record == null) throw new ArgumentException();

                DateTime today = DateTime.Today;
                DateTime updateDate = new DateTime(Int32.Parse(record.SettingValue) + 1, 9, 15);
                if (today.Date >= updateDate)
                {
                    DataInserter.UpdateSetting(record.ID, updateDate.Year.ToString());
                    DataInserter.UpdateReaderClassNo();
                    DataInserter.UpdateAllStudentsClassNo();
                    DataInserter.DeleteGraduatedReadersAndStudents();
                    MessageBox.Show("Класът на читателите беше обновен!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                int year = Int32.Parse(DataPersister.GetPropertyFromSettings("YearOfUpdate").SettingValue);
                this.PageTitle = "Библиотека - " + year + "/" + (year + 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Има проблем с проверката за обновяване на читателите!", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                this.PageTitle = "Електронна библиотека";
            }

        }

        public string PageTitle
        {
            get
            {
                return this.pageTitle;
            }
            set
            {
                this.pageTitle = value;
                OnPropertyChanged("PageTitle");
            }
        }
        public bool EnableApp
        {
            get
            {
                return this.enableApp;
            }
            set
            {
                this.enableApp = value;
                OnPropertyChanged("EnableApp");
            }
        }

        public ICommand AddReaderToDB
        {
            get
            {
                if (this.addReaderToDB == null)
                {
                    this.addReaderToDB = new RelayCommand(this.HandleAddReaderToDBCommand);
                }
                return this.addReaderToDB;
            }
        }
        public ICommand AddBookToDBCommand
        {
            get
            {
                if (this.addBookToDB == null)
                {
                    this.addBookToDB = new RelayCommand(this.HandleAddBookToDBCommand);
                }
                return this.addBookToDB;
            }
        }
        public ICommand SearchUserCommand
        {
            get
            {
                if (this.searchUser == null)
                {
                    this.searchUser = new RelayCommand(this.HandleSearchUserCommand);
                }
                return this.searchUser;
            }
        }
        public ICommand SearchBookCommand
        {
            get
            {
                if (this.searchBook == null)
                {
                    this.searchBook = new RelayCommand(this.HandleSearchBookCommand);
                }
                return this.searchBook;
            }
        }
        public ICommand TakenBooksCommand
        {
            get
            {
                if (this.takenBooks == null)
                {
                    this.takenBooks = new RelayCommand(this.HandleTakenBooksCommand);
                }
                return this.takenBooks;
            }
        }
        public ICommand SettingsCommand
        {
            get
            {
                if (this.settings == null)
                {
                    this.settings = new RelayCommand(this.HandleSettingsCommand);
                }
                return this.settings;
            }
        }
        public ICommand StatsCommand
        {
            get
            {
                if (this.stats == null)
                {
                    this.stats = new RelayCommand(this.HandleStatsCommand);
                }
                return this.stats;
            }
        }



        private void HandleAddReaderToDBCommand(object parameter)
        {
            var wind = new AddReaderPage();
            wind.Show();
        }
        private void HandleAddBookToDBCommand(object parameter)
        {
            var win = new AddBookPage();
            win.Show();
        }
        private void HandleSearchUserCommand(object parameter)
        {
            var win = new SearchStudentPage();
            win.Show();
        }
        private void HandleSearchBookCommand(object parameter)
        {
            var win = new SearchBookPage();
            win.Show();
        }
        private void HandleTakenBooksCommand(object parameter)
        {
            var win = new TakenBooksList();
            win.Show();
        }
        private void HandleSettingsCommand(object parameter)
        {
            var win = new SettingsPage();
            win.Show();
        }
        private void HandleStatsCommand(object parameter)
        {
            var win = new StatisticsPage();
            win.Show();
        }

        //    private void SendDatabaseAsAttachment()
        //    {
        //        try
        //        {
        //            SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
        //            var mail = new MailMessage();
        //            mail.From = new MailAddress("snaksa2@live.com");
        //            mail.To.Add("biblioteka_kubrat@abv.bg");
        //            mail.Subject = "База данни - Електронна Библиотека - " + DateTime.Now;
        //            mail.IsBodyHtml = false;
        //            string body;
        //            body = "Копие от базата данни изпратено на " + DateTime.Now;
        //            mail.Body = body;
        //            SmtpServer.Port = 587;
        //            SmtpServer.UseDefaultCredentials = false;
        //            SmtpServer.Credentials = new System.Net.NetworkCredential("snaksa2@live.com", "***");
        //            SmtpServer.EnableSsl = true;

        //            string attachmentFilename = "Library.accdb";
        //            Attachment attachment = new Attachment(attachmentFilename);
        //            ContentDisposition disposition = attachment.ContentDisposition;
        //            disposition.CreationDate = File.GetCreationTime(attachmentFilename);
        //            disposition.ModificationDate = File.GetLastWriteTime(attachmentFilename);
        //            disposition.ReadDate = File.GetLastAccessTime(attachmentFilename);
        //            disposition.FileName = Path.GetFileName(attachmentFilename);
        //            disposition.Size = new FileInfo(attachmentFilename).Length;
        //            disposition.DispositionType = DispositionTypeNames.Attachment;
        //            mail.Attachments.Add(attachment);

        //            SmtpServer.Send(mail);
        //        }
        //        catch(Exception ex)
        //        {

        //        }
        //    }
        //}

        private bool CloseApp()
        {
            try
            {
                string xml;
                using (var client = new WebClient())
                {
                    string address = "http://zaedno.somee.com/work.xml";
                    xml = client.DownloadString(address);
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/settings");


                foreach (XmlNode node in nodes)
                {
                    if (node.SelectSingleNode("work").InnerText == "0")
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }
    }
}
