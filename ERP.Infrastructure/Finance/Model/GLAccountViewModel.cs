using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.Finance.Model
{
    public class GLAccountVM
    {


        /*
         * 
         {
              id          : "string" // required
              parent      : "string" // required
              text        : "string" // node text
              icon        : "string" // string for custom
              state       : {
                opened    : boolean  // is the node open
                disabled  : boolean  // is the node disabled
                selected  : boolean  // is the node selected
              },
              li_attr     : {}  // attributes for the generated LI node
              a_attr      : {}  // attributes for the generated A node
            }

            -------------
            [{
                "id": "ajson1",
                "parent": "#",
                "text": "Simple root node"
            }, {
                "id": "ajson2",
                "parent": "#",
                "text": "Root node 2"
            }, {
                "id": "ajson3",
                "parent": "ajson2",
                "text": "Child 1"
            }, {
                "id": "ajson4",
                "parent": "ajson2",
                "text": "Child 2"
            }]
             */

        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public GLAccountVMState state { get; set; }
        public ICollection<GLAccountVM> children { get; set; }
        public GLAccountVM()
        {
            children = new List<GLAccountVM>();
        }
    }


    public class GLAccountVMState
    {
        public string selected { get; set; } = "false";
        public string isopened { get; set; } = "false";
        public string iconclass { get; set; } 
    }
}
