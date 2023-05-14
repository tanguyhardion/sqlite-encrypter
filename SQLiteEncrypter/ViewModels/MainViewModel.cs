using Microsoft.Win32;
using SQLiteEncrypter.Base;
using System;

namespace SQLiteEncrypter.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private static readonly Lazy<MainViewModel> _instance = new Lazy<MainViewModel>(() => new MainViewModel());
        public static MainViewModel Instance => _instance.Value;

        #region FIELDS
        private string _selectedFilePath;
        #endregion

        #region COMMANDS
        public RelayCommand BrowseCommand { get; set; }
        public RelayCommand EncryptCommand { get; set; }
        #endregion
        
        public MainViewModel()
        {
            BrowseCommand = new RelayCommand(_ => OpenDialog());
            EncryptCommand = new RelayCommand(_ => EncryptDatabase());
        }

        private void OpenDialog()
        {
            FileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".sqlite";
            fileDialog.Filter = "SQLite databases (*.sqlite)|*.sqlite | All Files (*.*)|*.*";
            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                this.SelectedFilePath = fileDialog.FileName;
            }
        }
 
        private void EncryptDatabase()
        {
            
        }

        public string SelectedFilePath
        {
            get => _selectedFilePath;
            set
            {
                _selectedFilePath = value;
                RaisePropertyChanged();
            }
        }
    }
}
