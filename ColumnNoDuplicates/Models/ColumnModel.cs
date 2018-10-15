using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColumnNoDuplicates.Models
{
    public enum DataType
    {
        Integer,
        Boolean,
        Double,
        DateTime
    }

    class ColumnModel
    {
        public string Name { get; set; }
        public DataType DataType { get; set; }
    }
}
