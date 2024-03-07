using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Dynamic;

public class DynamicQuery
{
    public IEnumerable<Sort>? Sort{ get; set; }
    public Filter? Filter { get; set; }

    public DynamicQuery()
    {
        
    }
    public DynamicQuery(IEnumerable<Sort>? sort, Filter? filter)
    {
        Filter = filter;
        Sort= sort;
    }
}

// select * from cars where unitPrice<100 and transmission=1 and (color=white or color=grey)
// 
