using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;

namespace ControlLayer
{
    public class MainCtr
    {
        DataContext db = new dmab0913_3DataContext();
        public AirplaneCtr AirplaneCtr = new AirplaneCtr();
        public AirportCtr AirportCtr = new AirportCtr();

    }
}
