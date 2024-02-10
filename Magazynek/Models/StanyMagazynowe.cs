using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazynek.Models
{
    public class StanyMagazynowe
    {

        public int Id { get; set; }
        public int IloscDostepna {  get; set; }
        public int IloscZarezerwowana {  get; set; }
        public int AsortymentId {  get; set; }
        public int MagazynId {  get; set; }
    }
}
