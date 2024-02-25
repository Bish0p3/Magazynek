namespace Magazynek.Models
{
    public class AsortymentyModel
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public object CenaEwidencyjna { get; set; }
        public object IloscDostepna { get; set; }
        public object IloscZarezerwowanaIlosciowo { get; set; }
        public object IloscZarezerwowanaDostawowo { get; set; }
        public object Ilosc { get; set; }
        public object Termin { get; set; }
        public object Ilosciowa { get; set; }
    }
}
