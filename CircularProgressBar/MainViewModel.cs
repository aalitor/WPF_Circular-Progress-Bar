using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CircularProgressBarApp.mvvmSupport;

namespace CircularProgressBarApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand StartCommand { get; private set; }
        private readonly BackgroundWorker worker;
        private int _progressValue;
        public int ProgressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                OnPropertyChanged("ProgressValue");
                OnPropertyChanged("ProgressText");
            }
        }

        public string ProgressText
        {
            get { return string.Format("{0} %", _progressValue); }
        }


        public MainViewModel()
        {
            this.worker = new BackgroundWorker();
            this.worker.WorkerReportsProgress = true;
            this.worker.DoWork += this.DoWork;
            this.worker.ProgressChanged += this.ProgressChanged;
            StartCommand = new CommandHandler(() =>
            {
                this.worker.RunWorkerAsync();
            }, () =>
            {
                return !this.worker.IsBusy;
            });

        }
        private void DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(100);//Do your Work Here instead of sleeping
                worker.ReportProgress(i);
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.ProgressValue = e.ProgressPercentage;
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
