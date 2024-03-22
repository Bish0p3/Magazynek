using Magazynek.Helpers;
using Magazynek.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazynek.ViewModels
{
    internal class NewOrderViewModel : ObservableObject
    {
        private ObservableCollection<WarehouseNamesModel> _warehouseNames;
        public ObservableCollection<WarehouseNamesModel> WarehouseNames
        {
            get { return _warehouseNames; }
            set
            {
                _warehouseNames = value;
                OnPropertyChanged(nameof(WarehouseNames));
            }
        }

        private ObservableCollection<ItemModel> _items;
        public ObservableCollection<ItemModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public NewOrderViewModel()
        {
            WarehouseNames = new ObservableCollection<WarehouseNamesModel>
            {
                new WarehouseNamesModel
                {
                    Nazwa = "Magazyn główny",
                },
                new WarehouseNamesModel
                {
                    Nazwa = "W transporcie"
                },
                new WarehouseNamesModel
                {
                    Nazwa = "W produkcji"
                }
            };


            Items = new ObservableCollection<ItemModel>
            {
                new ItemModel
                {
                    Nazwa = "Auto model Standard 1.2 TFI 74kW 100HP",
                    Ilosc = 40
                },
                new ItemModel
                {
                    Nazwa = "Auto model Premium 1.8d 90kW 140HP",
                    Ilosc = 10
                },
                new ItemModel
                {
                    Nazwa = "Auto model Premium 1.2 TFI 80kW 120HP",
                    Ilosc = 10
                },
                new ItemModel
                {
                    Nazwa = "Auto model Standard 1.8d 76kW 110HP",
                    Ilosc = 25
                }
            };

        }
    }
}
