using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazynek.Models
{
    public class OrderModel
    {
        public object Id { get; set; }
        public string MiejsceWydaniaWystawienia { get; set; }
        public DateTime DataWprowadzenia { get; set; }
        public string NumerZewnetrzny { get; set; }
        public DateTime DataWydaniaWystawienia { get; set; }
        public DateTime TerminRealizacji { get; set; }
        public object KwotaDoZaplaty {  get; set; }
        public object Symbol {  get; set; }
        public string Tytul {  get; set; }
        public object Podtytul { get; set; }
        public object Wystawil {  get; set; }
        public object Odebral {  get; set; }
        public object Uwagi {  get; set; }
        public object PelnaSyngatura { get; set; }
    }
}
