using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class RecieptManager
    {
        private ApplicationDBContext database = null;

        public RecieptManager()
        {
            database = new ApplicationDBContext();
        }

        /*public List<Reciept> FindAllRecieptsByProjectID(Guid ProjectID)
        {
            return database.Reciepts.Where(cols => cols.ProjectID == ProjectID).ToList();    
        }*/
    }
}
