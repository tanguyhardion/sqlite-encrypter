using System;

namespace SQLiteEncrypter.ViewModels
{
    public class MainViewModel
    {
        private static readonly Lazy<MainViewModel> _instance = new Lazy<MainViewModel>(() => new MainViewModel());
        public static MainViewModel Instance => _instance.Value;

        public FileViewModel FileViewModel { get; set; }

        private MainViewModel()
        {
            FileViewModel = new FileViewModel();
        }
    }
}