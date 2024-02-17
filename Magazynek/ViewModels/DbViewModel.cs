using Magazynek.Helpers;
using Magazynek.Models;
using Magazynek.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Magazynek.ViewModels
{
    public class DbViewModel : ObservableObject
    {

        private ObservableCollection<AsortymentyModel> _asortyment;
        public ObservableCollection<AsortymentyModel> Asortyment
        {
            get => _asortyment;
            set
            {
                _asortyment = value;
                OnPropertyChanged(nameof(Asortyment));
            }
        }

        private ICommand _showDataCommand;
        public ICommand ShowDataCommand
        {
            get
            {
                return _showDataCommand ??= new Command(() =>
                {
                    LoadData();
                });
            }
        }

        private void LoadData()
        {
            DatabaseService dbservice = new DatabaseService();

            if (dbservice != null)
            {
                //using (var _context = new ApplicationDbContext())
                //{
                //    // Retrieve data using LINQ query
                //    List<AsortymentyModel> dataList = _context.Asortyment.ToList();

                //}
                Console.WriteLine("Database is connected!");
                Asortyment = new ObservableCollection<AsortymentyModel>();
            }
            else
            {
                Console.WriteLine("Unable to connect to the database.");
                Asortyment = new ObservableCollection<AsortymentyModel>
                {
                    new AsortymentyModel { Nazwa = "TEST"}
                };
            }

        }
    }
}
