using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SwfDec.AVM2.Types.Traits;

namespace SwfDec.AVM2.Types.Extended
{
    public class TraitDescription
    {
        public TraitKind Kind { get; set; }
        public string TypeName { get; set; }

        public TraitDescription(TraitKind kind, string typeName) {
            Kind = kind;
            TypeName = typeName;
        }

        public bool IsMatch(TraitsInfo info) {
            if (info.Kind == Kind) {
                switch (Kind) {
                    case TraitKind.Slot:
                    case TraitKind.Const:
                        if (TypeName == info.TraitField.GetTypeName())
                            return true;
                        break;

                    case TraitKind.Method:
                        if (TypeName == info.TraitMethod.Method.GetReturnTypeName())
                            return true;
                        break;

                    case TraitKind.Getter:
                        return true;

                    case TraitKind.Setter:
                        return true;

                    case TraitKind.Class:
                        return true;

                    case TraitKind.Function:
                        return true;
                }
            }

            return false;
        }
    }
}
