using SeniorDesign.Core;

namespace SeniorDesign.FrontEnd
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
        public IConnectable A;
        public IConnectable B;
        public IDstruct(ObjectType objectType, IConnectable A, IConnectable B)
        {
            this.objectType = objectType;
            this.A = A;
            this.B = B;
        }
    }
}
