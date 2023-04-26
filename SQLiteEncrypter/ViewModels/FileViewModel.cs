using Microsoft.Win32;
using SQLiteEncrypter.Base;

namespace SQLiteEncrypter.ViewModels
{
    public class FileViewModel : ViewModelBase
    {
        public RelayCommand BrowseCommand { get; set; }
        private string _selectedFilePath;

        public FileViewModel()
        {
            BrowseCommand = new RelayCommand(_ => OpenDialog());
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
