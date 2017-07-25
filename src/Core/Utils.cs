using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeniorDesign.Core
{
    public enum ObjectType
    {
        Null,
        Line,
        Block,
        Input,
        Ouput,
    };
    public struct IDstruct
    {
        public ObjectType objectType;
        public int ID;
        public int ID2;

        public IDstruct(ObjectType objectType, int ID, int ID2)
        {
            this.objectType = objectType;
            this.ID = ID;
            this.ID2 = ID;
        }
    }
}
