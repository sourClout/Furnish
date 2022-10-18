using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furnish
{
    internal class Global
    {
        


        private static FurnishDbConnection _dbContextInternal;

        public static FurnishDbConnection dbContextAuto
        {

            get {
                if (_dbContextInternal == null)
                {
                    _dbContextInternal = new FurnishDbConnection();
                }
                    return _dbContextInternal;
            

            }
        }
    }
}
