using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazynek.Models
{
    public class RezerwacjeModel
    {
        public int Id { get; set; }
        public int Ilosc {  get; set; }
        public int IloscZrealizowana {  get; set; }
        public string AsortymentId { get; set; }
    }
}
