using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfLogger
{
    public class LoggerVM : INotifyPropertyChanged
    {
        private LoggerModel _model = new LoggerModel(String.Empty, DateTime.Now);
        public LoggerModel model
        {
            get { return _model; }
            set
            {
                OnPropertyChanged("model"); 
            }
        }

        public string logInput
        {
            get { return model.Message; }
            set 
            {
                model.Message = value;
                OnPropertyChanged("logInput"); 
            }
        }

        private ObservableCollection<string> _LoggedData = new ObservableCollection<string>();
        public ObservableCollection<string> LoggedData 
        {
            get { return _LoggedData; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand LogText
        {
            get { return new DelegateCommand(LogActivity); }
        }

        public ICommand getLog
        {
            get { return new DelegateCommand(readLog); }
        }

        public ICommand clearLog
        {
            get { return new DelegateCommand(clearLogFile); }
        }

        public void LogActivity()
        {
            model.Date = DateTime.Now;

            string LogMessage = model.Date.ToShortTimeString() + ", " + model.Date.ToShortDateString() + " " + logInput + "\n";
            if (File.Exists("LogFile.txt"))
            {
                File.AppendAllText("LogFile.txt", LogMessage);
            }
            logInput = String.Empty;
        }
        public void readLog()
        {
            StreamReader sr = new StreamReader("LogFile.txt");
            string readData = String.Empty;

            LoggedData.Clear();
            while (sr.EndOfStream != true)
            {
                readData = sr.ReadLine();
                if (readData != null)
                {
                    LoggedData.Add(readData);
                }
            }

            sr.Close();
        }
        public void clearLogFile()
        {
            LoggedData.Clear();
            File.WriteAllText("LogFile.txt", String.Empty);
        }
    }
}
