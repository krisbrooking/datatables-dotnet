using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatatablesQueryable.DataTables
{
    public class DataTablesModel
    {
        public int draw { get; set; }

        public int recordsFiltered { get; set; }

        public int recordsTotal { get; set; }

        public object[] data { get; set; }
    }
}
