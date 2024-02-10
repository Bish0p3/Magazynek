using Magazynek.Data;
using Magazynek.Helpers;
using Magazynek.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Magazynek.ViewModels
{
    public class DbViewModel : ObservableObject
    {
        public DbViewModel() 
        {

        }
        private readonly ApplicationDbContext _context;

        public DbViewModel(ApplicationDbContext context)
        {
            _context = context;
        }

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
            var dbContext = new ApplicationDbContext();
            if (dbContext.IsDatabaseConnected())
            {
                Console.WriteLine("Database is connected!");
                Asortyment = new ObservableCollection<AsortymentyModel>(_context.Asortyment.ToList());
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
