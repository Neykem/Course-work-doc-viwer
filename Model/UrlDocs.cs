using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Course_work_doc_lib.Model
{
    class UrlDocs
    {
        //[Key]
        public int id { get; set; }
        public string url { get; set; }

        public UrlDocs()
        {

        }
    }
}
