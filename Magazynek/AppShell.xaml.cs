﻿using Magazynek.Views;

namespace Magazynek
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("main", typeof(LoginPage));
            Routing.RegisterRoute("magazyn", typeof(WarehousesPage));
            Routing.RegisterRoute("umowy", typeof(OrdersPage));

        }
    }
}