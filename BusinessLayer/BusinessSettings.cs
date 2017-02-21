using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayers;

namespace BusinessLayer
{
    public class BusinessSettings
    {
        public static void SetBusiness()
        {
            DatabaseSettings.SetDatabase();
        }
    }
}
