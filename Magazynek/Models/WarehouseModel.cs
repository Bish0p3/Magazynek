﻿namespace Magazynek.Models
{
    public class WarehouseModel
    {

        public int Id { get; set; }
        public int IloscDostepna { get; set; }
        public int IloscZarezerwowana { get; set; }
        public int AsortymentId { get; set; }
        public int MagazynId { get; set; }
    }
}
