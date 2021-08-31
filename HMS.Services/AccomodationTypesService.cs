using HMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace HMS.Services
{
    public class AccomodationTypesService
    {
        public List<AccomodationType> GetAccomodationTypes()
        {
            var context = new HMSContext();
            return context.AccomodationTypes.ToList();
        }
        //public IEnumerable <AccomodationType> GetAllAccomodationTypes()
        //{
        //   dynamic context = new HMSContext();
        //    return context.AccomodationTypes.ToList();
        //}
    }
}
