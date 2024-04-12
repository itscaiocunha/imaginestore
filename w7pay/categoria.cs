using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace w7pay
{
    internal class categoria
    {       
        public List<ListResquetStatus> request { get; set; }

        //Especifica os campos dentro do objeto 'request'
        public class ListResquetStatus
        {
            public string id { get; set; }
            public string Descricao { get; set; }
            public string Over18 { get; set; }

        }
    }

    
}