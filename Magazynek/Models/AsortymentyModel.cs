using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Magazynek.Models
{
    public class AsortymentyModel
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Nazwa {  get; set; }
        public string Opis {  get; set; }
        public string CenaEwidencyjna { get; set; }
    }
}
