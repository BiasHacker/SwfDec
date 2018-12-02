using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfDec.AVM2.ByteCode.Instructions;
using SwfDec.AVM2.ByteCode.Instructions.Call;
using SwfDec.AVM2.ByteCode.Instructions.Cast;
using SwfDec.AVM2.ByteCode.Instructions.Conditions;
using SwfDec.AVM2.ByteCode.Instructions.Construct;
using SwfDec.AVM2.ByteCode.Instructions.Create;
using SwfDec.AVM2.ByteCode.Instructions.Debug;
using SwfDec.AVM2.ByteCode.Instructions.Delete;
using SwfDec.AVM2.ByteCode.Instructions.FastMemory;
using SwfDec.AVM2.ByteCode.Instructions.Find;
using SwfDec.AVM2.ByteCode.Instructions.Get;
using SwfDec.AVM2.ByteCode.Instructions.Iterator;
using SwfDec.AVM2.ByteCode.Instructions.Locals;
using SwfDec.AVM2.ByteCode.Instructions.Operators;
using SwfDec.AVM2.ByteCode.Instructions.Push;
using SwfDec.AVM2.ByteCode.Instructions.Return;
using SwfDec.AVM2.ByteCode.Instructions.Set;
using SwfDec.AVM2.ByteCode.Instructions.Type;

namespace SwfDec.AVM2.ByteCode
{
    public delegate void OptFunction(ByteCode byteCode, ref int index);

    public class Optimization
    {
        public Opcode[] Pattern { get; private set; }

        public OptFunction Function { get; set; }

        public Optimization()
        {
        }

        public void SetPattern(params Opcode[] pattern)
        {
            Pattern = pattern;
        }

    }

    public sealed class ByteCode
    {
        public List<As3Instruction> Instructions => _instructions;

        private List<As3Instruction> _instructions;
        private List<IJump> _jumpInstructions;
        private List<ICaseSwitch> _switchInstructions;
        private Dictionary<As3Instruction, int> _instructionPositions; 

        private Abc _abc;

        public ByteCode(byte[] byteCode, Abc abc)
        {
            _abc = abc;

            _instructions = new List<As3Instruction>();
            AbcStream stream = byteCode;
            stream.Position = 0;

            _jumpInstructions = new List<IJump>();
            _switchInstructions = new List<ICaseSwitch>();
            _instructionPositions = new Dictionary<As3Instruction, int>();
            while (stream.Position < stream.Length)
            {
                int pos = stream.Position;
                #region Switch

                As3Instruction instruction = null;
                Opcode o = (Opcode) stream.ReadByte();

                switch (o)
                {
                    case Opcode.Add:
                        instruction = new As3Add();
                        break;

                    case Opcode.AddD:
                        instruction = new As3AddD();
                        break;

                    case Opcode.AddI:
                        instruction = new As3AddI();
                        break;

                    case Opcode.ApplyType:
                        instruction = new As3ApplyType(stream.ReadU30());
                        break;

                    case Opcode.AsType:
                        instruction = new As3AsType(abc.ConstantPool.GetMultinameAt(stream.ReadU30()));
                        break;

                    case Opcode.AsTypeLate:
                        instruction = new As3AsTypeLate();
                        break;

                    case Opcode.BitAnd:
                        instruction = new As3BitAnd();
                        break;

                    case Opcode.BitNot:
                        instruction = new As3BitNot();
                        break;

                    case Opcode.BitOr:
                        instruction = new As3BitOr();
                        break;

                    case Opcode.BitXor:
                        instruction = new As3BitXor();
                        break;

                    case Opcode.Call:
                        instruction = new As3Call(stream.ReadU30());
                        break;

                    case Opcode.CallProperty:
                        instruction = new As3CallProperty(abc.ConstantPool.GetMultinameAt(stream.ReadU30()), stream.ReadU30());
                        break;

                    case Opcode.CallPropLex:
                        instruction = new As3CallPropLex(abc.ConstantPool.GetMultinameAt(stream.ReadU30()), stream.ReadU30());
                        break;

                    case Opcode.CallPropVoid:
                        instruction = new As3CallPropVoid(abc.ConstantPool.GetMultinameAt(stream.ReadU30()), stream.ReadU30());
                        break;

                    case Opcode.CallSuper:
                        instruction = new As3CallSuper(abc.ConstantPool.GetMultinameAt(stream.ReadU30()), stream.ReadU30());
                        break;

                    case Opcode.CallSuperVoid:
                        instruction = new As3CallSuperVoid(abc.ConstantPool.GetMultinameAt(stream.ReadU30()), stream.ReadU30());
                        break;

                    case Opcode.CheckFilter:
                        instruction = new As3CheckFilter();
                        break;

                    case Opcode.Coerce:
                        instruction = new As3Coerce(abc.ConstantPool.GetMultinameAt(stream.ReadU30()));
                        break;

                    case Opcode.CoerceA:
                        instruction = new As3CoerceA();
                        break;

                    case Opcode.CoerceB:
                        instruction = new As3CoerceB();
                        break;

                    case Opcode.CoerceD:
                        instruction = new As3CoerceD();
                        break;

                    case Opcode.CoerceI:
                        instruction = new As3CoerceI();
                        break;

                    case Opcode.CoerceO:
                        instruction = new As3CoerceO();
                        break;

                    case Opcode.CoerceS:
                        instruction = new As3CoerceS();
                        break;

                    case Opcode.CoerceU:
                        instruction = new As3CoerceU();
                        break;

                    case Opcode.Construct:
                        instruction = new As3Construct(stream.ReadU30());
                        break;

                    case Opcode.ConstructProp:
                        instruction = new As3ConstructProp(abc.ConstantPool.GetMultinameAt(stream.ReadU30()), stream.ReadU30());
                        break;

                    case Opcode.ConstructSuper:
                        instruction = new As3ConstructSuper(stream.ReadU30());
                        break;

                    case Opcode.ConvertB:
                        instruction = new As3ConvertB();
                        break;

                    case Opcode.ConvertD:
                        instruction = new As3ConvertD();
                        break;

                    case Opcode.ConvertI:
                        instruction = new As3ConvertI();
                        break;

                    case Opcode.ConvertO:
                        instruction = new As3ConvertO();
                        break;

                    case Opcode.ConvertS:
                        instruction = new As3ConvertS();
                        break;

                    case Opcode.ConvertU:
                        instruction = new As3ConvertU();
                        break;

                    case Opcode.DebugLine:
                        instruction = new As3DebugLine(stream.ReadU30());
                        break;

                    case Opcode.DebugFile:
                        instruction = new As3DebugFile(stream.ReadU30());
                        break;

                    case Opcode.DecLocal:
                        instruction = new As3DecLocal(stream.ReadU30());
                        break;

                    case Opcode.DecLocalI:
                        instruction = new As3DecLocalI(stream.ReadU30());
                        break;

                    case Opcode.Decrement:
                        instruction = new As3Decrement();
                        break;

                    case Opcode.DecrementI:
                        instruction = new As3DecrementI();
                        break;

                    case Opcode.DeleteProperty:
                        instruction = new As3DeleteProperty(abc.ConstantPool.GetMultinameAt(stream.ReadU30()));
                        break;

                    case Opcode.DeletePropertyLate:
                        instruction = new As3DeletePropertyLate();
                        break;

                    case Opcode.Divide:
                        instruction = new As3Divide();
                        break;

                    case Opcode.Dup:
                        instruction = new As3Dup();
                        break;

                    case Opcode.Equals:
                        instruction = new As3Equals();
                        break;

                    case Opcode.FindProperty:
                        instruction = new As3FindProperty(abc.ConstantPool.GetMultinameAt(stream.ReadU30()));
                        break;

                    case Opcode.FindPropStrict:
                        instruction = new As3FindPropStrict(abc.ConstantPool.GetMultinameAt(stream.ReadU30()));
                        break;

                    case Opcode.GetDescendants:
                        instruction = new As3GetDescendants(abc.ConstantPool.GetMultinameAt(stream.ReadU30()));
                        break;

                    case Opcode.GetGlobalScope:
                        instruction = new As3GetGlobalScope();
                        break;

                    case Opcode.GetLex:
                        instruction = new As3GetLex(abc.ConstantPool.GetMultinameAt(stream.ReadU30()));
                        break;

                    case Opcode.GetLocal0:
                        instruction = new As3GetLocal0();
                        break;

                    case Opcode.GetLocal1:
                        instruction = new As3GetLocal1();
                        break;

                    case Opcode.GetLocal2:
                        instruction = new As3GetLocal2();
                        break;

                    case Opcode.GetLocal3:
                        instruction = new As3GetLocal3();
                        break;

                    case Opcode.GetLocal:
                        instruction = new As3GetLocal(stream.ReadU30());
                        break;

                    case Opcode.GetProperty:
                        instruction = new As3GetProperty(abc.ConstantPool.GetMultinameAt(stream.ReadU30()));
                        break;

                    case Opcode.GetScopeObject:
                        instruction = new As3GetScopeObject(stream.ReadU30());
                        break;

                    case Opcode.GetSlot:
                        instruction = new As3GetSlot(stream.ReadU30());
                        break;

                    case Opcode.GetSuper:
                        instruction = new As3GetSuper(abc.ConstantPool.GetMultinameAt(stream.ReadU30()));
                        break;

                    case Opcode.GreaterEquals:
                        instruction = new As3GreaterEquals();
                        break;

                    case Opcode.GreaterThan:
                        instruction = new As3GreaterThan();
                        break;

                    case Opcode.HasNext2:
                        instruction = new As3HasNext2(stream.ReadU30(), stream.ReadU30());
                        break;

                    case Opcode.HasNext:
                        instruction = new As3HasNext();
                        break;

                    case Opcode.IfEqual:
                        instruction = new As3IfEqual(stream.ReadS24());
                        break;

                    case Opcode.IfFalse:
                        instruction = new As3IfFalse(stream.ReadS24());
                        break;

                    case Opcode.IfGreaterEqual:
                        instruction = new As3IfGreaterEqual(stream.ReadS24());
                        break;

                    case Opcode.IfGreaterThan:
                        instruction = new As3IfGreaterThan(stream.ReadS24());
                        break;

                    case Opcode.IfLessEqual:
                        instruction = new As3IfLessEqual(stream.ReadS24());
                        break;

                    case Opcode.IfLessThan:
                        instruction = new As3IfLessThan(stream.ReadS24());
                        break;

                    case Opcode.IfNotEqual:
                        instruction = new As3IfNotEqual(stream.ReadS24());
                        break;

                    case Opcode.IfNotGreaterEqual:
                        instruction = new As3IfNotGreaterEqual(stream.ReadS24());
                        break;

                    case Opcode.IfNotGreaterThan:
                        instruction = new As3IfNotGreaterThan(stream.ReadS24());
                        break;

                    case Opcode.IfNotLessEqual:
                        instruction = new As3IfNotLessEqual(stream.ReadS24());
                        break;

                    case Opcode.IfNotLessThan:
                        instruction = new As3IfNotLessThan(stream.ReadS24());
                        break;

                    case Opcode.IfStrictEqual:
                        instruction = new As3IfStrictEqual(stream.ReadS24());
                        break;

                    case Opcode.IfStrictNotEqual:
                        instruction = new As3IfStrictNotEqual(stream.ReadS24());
                        break;

                    case Opcode.IfTrue:
                        instruction = new As3IfTrue(stream.ReadS24());
                        break;

                    case Opcode.In:
                        instruction = new As3In();
                        break;

                    case Opcode.Inclocal:
                        instruction = new As3IncLocal(stream.ReadU30());
                        break;

                    case Opcode.InclocalI:
                        instruction = new As3IncLocalI(stream.ReadU30());
                        break;

                    case Opcode.Increment:
                        instruction = new As3Increment();
                        break;

                    case Opcode.IncrementI:
                        instruction = new As3IncrementI();
                        break;

                    case Opcode.InitProperty:
                        instruction = new As3InitProperty(abc.ConstantPool.GetMultinameAt(stream.ReadU30()));
                        break;

                    case Opcode.IsType:
                        instruction = new As3IsType(abc.ConstantPool.GetMultinameAt(stream.ReadU30()));
                        break;

                    case Opcode.IsTypeLate:
                        instruction = new As3IsTypeLate();
                        break;

                    case Opcode.Jump:
                        instruction = new As3Jump(stream.ReadS24());
                        break;

                    case Opcode.Kill:
                        instruction = new As3Kill(stream.ReadU30());
                        break;

                    case Opcode.Label:
                        instruction = new As3Label();
                        break;

                    case Opcode.LessEquals:
                        instruction = new As3LessEquals();
                        break;

                    case Opcode.LessThan:
                        instruction = new As3LessThan();
                        break;

                    case Opcode.LookupSwitch:
                        int defOffset = stream.ReadS24();
                        int[] caseOffsets = new int[stream.ReadU30() + 1];
                        for (int i = 0; i < caseOffsets.Length; ++i)
                            caseOffsets[i] = stream.ReadS24();
                        instruction = new As3LookupSwitch(defOffset, caseOffsets);
                        break;

                    case Opcode.LShift:
                        instruction = new As3LShift();
                        break;

                    case Opcode.Modulo:
                        instruction = new As3Modulo();
                        break;

                    case Opcode.Multiply:
                        instruction = new As3Multiply();
                        break;

                    case Opcode.MultiplyI:
                        instruction = new As3MultiplyI();
                        break;

                    case Opcode.Negate:
                        instruction = new As3Negate();
                        break;

                    case Opcode.NegateI:
                        instruction = new As3NegateI();
                        break;

                    case Opcode.NewActivation:
                        instruction = new As3NewActivation();
                        break;

                    case Opcode.NewArray:
                        instruction = new As3NewArray(stream.ReadU30());
                        break;

                    case Opcode.NewCatch:
                        instruction = new As3NewCatch(stream.ReadU30());
                        break;

                    case Opcode.NewClass:
                        instruction = new As3NewClass(stream.ReadU30());
                        break;

                    case Opcode.NewFunction:
                        instruction = new As3NewFunction(stream.ReadU30());
                        break;

                    case Opcode.NewObject:
                        instruction = new As3NewObject(stream.ReadU30());
                        break;

                    case Opcode.NextName:
                        instruction = new As3NextName();
                        break;

                    case Opcode.NextValue:
                        instruction = new As3NextValue();
                        break;

                    case Opcode.Nop:
                        instruction = new As3Nop();
                        break;

                    case Opcode.Not:
                        instruction = new As3Not();
                        break;

                    case Opcode.Pop:
                        instruction = new As3Pop();
                        break;

                    case Opcode.PopScope:
                        instruction = new As3PopScope();
                        break;

                    case Opcode.PushByte:
                        instruction = new As3PushByte((sbyte) stream.ReadByte());
                        break;

                    case Opcode.PushDouble:
                        instruction = new As3PushDouble(stream.ReadU30());
                        break;

                    case Opcode.PushFalse:
                        instruction = new As3PushFalse();
                        break;

                    case Opcode.PushInt:
                        instruction = new As3PushInt(stream.ReadU30());
                        break;

                    case Opcode.PushNan:
                        instruction = new As3PushNan();
                        break;

                    case Opcode.PushNull:
                        instruction = new As3PushNull();
                        break;

                    case Opcode.PushScope:
                        instruction = new As3PushScope();
                        break;

                    case Opcode.PushShort:
                        instruction = new As3PushShort(stream.ReadU30());
                        break;

                    case Opcode.PushString:
                        instruction = new As3PushString(abc.ConstantPool.GetStringAt(stream.ReadU30()));
                        break;

                    case Opcode.PushTrue:
                        instruction = new As3PushTrue();
                        break;

                    case Opcode.PushUInt:
                        instruction = new As3PushUInt(stream.ReadU30());
                        break;

                    case Opcode.PushUndefined:
                        instruction = new As3PushUndefined();
                        break;

                    case Opcode.PushWith:
                        instruction = new As3PushWith();
                        break;

                    case Opcode.ReturnValue:
                        instruction = new As3ReturnValue();
                        break;

                    case Opcode.ReturnVoid:
                        instruction = new As3ReturnVoid();
                        break;

                    case Opcode.RShift:
                        instruction = new As3RShift();
                        break;

                    case Opcode.SetLocal0:
                        instruction = new As3SetLocal0();
                        break;

                    case Opcode.SetLocal1:
                        instruction = new As3SetLocal1();
                        break;

                    case Opcode.SetLocal2:
                        instruction = new As3SetLocal2();
                        break;

                    case Opcode.SetLocal3:
                        instruction = new As3SetLocal3();
                        break;

                    case Opcode.SetLocal:
                        instruction = new As3SetLocal(stream.ReadU30());
                        break;

                    case Opcode.SetProperty:
                        instruction = new As3SetProperty(abc.ConstantPool.GetMultinameAt(stream.ReadU30()));
                        break;

                    case Opcode.SetSlot:
                        instruction = new As3SetSlot(stream.ReadU30());
                        break;

                    case Opcode.SetSuper:
                        instruction = new As3SetSuper(abc.ConstantPool.GetMultinameAt(stream.ReadU30()));
                        break;

                    case Opcode.StrictEquals:
                        instruction = new As3StrictEquals();
                        break;

                    case Opcode.Subtract:
                        instruction = new As3Subtract();
                        break;

                    case Opcode.SubtractI:
                        instruction = new As3SubtractI();
                        break;

                    case Opcode.Swap:
                        instruction = new As3Swap();
                        break;

                    case Opcode.Throw:
                        instruction = new As3Throw();
                        break;

                    case Opcode.TypeOf:
                        instruction = new As3TypeOf();
                        break;

                    case Opcode.URShift:
                        instruction = new As3URShift();
                        break;

                    case Opcode.SI8:
                        instruction = new As3SI8();
                        break;

                    case Opcode.SI16:
                        instruction = new As3SI16();
                        break;

                    case Opcode.SI32:
                        instruction = new As3SI32();
                        break;

                    case Opcode.SF32:
                        instruction = new As3SF32();
                        break;

                    case Opcode.LI8:
                        instruction = new As3LI8();
                        break;

                    case Opcode.LI16:
                        instruction = new As3LI16();
                        break;

                    case Opcode.LI32:
                        instruction = new As3LI32();
                        break;

                    case Opcode.LF32:
                        instruction = new As3LF32();
                        break;

                    case Opcode.LF64:
                        instruction = new As3LF64();
                        break;

                    case Opcode.SXI1:
                        instruction = new As3SX1();
                        break;

                    case Opcode.SXI8:
                        instruction = new As3SXI8();
                        break;

                    case Opcode.SXI16:
                        instruction = new As3SX16();
                        
                        break;

                    default:
                        throw new Exception("Unknown opcode: " + o);
                }

                #endregion

                _instructions.Add(instruction);

                _instructionPositions[instruction] = pos;

                if (instruction is IJump)
                    _jumpInstructions.Add(instruction as IJump);
                

                if (instruction is ICaseSwitch)
                    _switchInstructions.Add(instruction as ICaseSwitch);
                
            }
        }

        ~ByteCode()
        {
            _instructions = null;
            _jumpInstructions = null;
            _switchInstructions = null;
            _instructionPositions = null;

            _abc = null;
        }

        public As3Instruction[] GetInstructions()
        {
            return _instructions.ToArray();
        }

        public byte[] GetBytes()
        {
            AbcStream stream = new AbcStream();
            foreach (var as3Instruction in _instructions)
                stream.WriteBytes(as3Instruction.ToBytes());
            return stream;
        }

        public void AddInstructionAt(As3Instruction instruction, int index)
        {
            if (index >= _instructions.Count)
            {

                As3Instruction lastIns = _instructions[_instructions.Count - 1];
                _instructions.Add(instruction);

                _instructionPositions.Add(instruction, _instructionPositions[lastIns] + lastIns.Length);
                if (instruction is IJump)
                    _jumpInstructions.Add(instruction as IJump);


                if (instruction is ICaseSwitch)
                    _switchInstructions.Add(instruction as ICaseSwitch);
                return;
            }

            int pos = _instructionPositions[_instructions[index]];

            foreach (var jumpInstruction in _jumpInstructions)
            {
                int jumpInsPos = _instructionPositions[jumpInstruction as As3Instruction];
                int jumpOffset = jumpInstruction.Offset;
                int jumpInsLen = (jumpInstruction as As3Instruction).Length;
                int target = jumpInsPos + jumpInsLen + jumpOffset;
                if (jumpOffset > 0)
                {
                    if (target > pos && pos > jumpInsPos)
                    {
                        jumpInstruction.Offset += instruction.Length;
                    }
                }
                if (jumpOffset < 0)
                {
                    if (target < pos && pos < jumpInsPos)
                    {
                        jumpInstruction.Offset -= instruction.Length;
                    }
                }

                foreach (var switchInstruction in _switchInstructions)
                {
                    var caseIns = switchInstruction as As3Instruction;
                    int casePos = _instructionPositions[caseIns];
                    int[] indexes = new int[switchInstruction.CaseOffsets.Length + 1];
                    for (int i = 0; i < switchInstruction.CaseOffsets.Length; i++)
                        indexes[i + 1] = switchInstruction.CaseOffsets[i];
                    indexes[0] = switchInstruction.DefaultOffset;

                    for (int i = 0; i < indexes.Length; i++)
                    {
                        if (indexes[i] >= 0)
                            throw new Exception();

                        if (casePos + indexes[i] <= pos && pos > casePos)
                        {
                            indexes[i] += instruction.Length;
                        }
                    }

                    switchInstruction.DefaultOffset = indexes[0];
                    for (int i = 0; i < indexes.Length - 1; i++)
                        switchInstruction.CaseOffsets[i] = indexes[i + 1];
                }
            }

            if (instruction is IJump)
            {
                _jumpInstructions.Add(instruction as IJump);
            }

            if (instruction is ICaseSwitch)
            {
                _switchInstructions.Add(instruction as ICaseSwitch);
            }

            _instructions.Insert(index, instruction);
            _instructionPositions.Add(instruction, pos);
            for (int j = index + 1; j < _instructions.Count; j++)
                _instructionPositions[_instructions[j]] += instruction.Length;
        }

        public void RemoveInstructionAt(int index)
        {
            As3Instruction ins = _instructions[index];
            int insLen = ins.Length;
            int pos = _instructionPositions[ins];

            foreach (var jumpInstruction in _jumpInstructions)
            {
                
                int jumpInsPos = _instructionPositions[jumpInstruction as As3Instruction];
                int jumpOffset = jumpInstruction.Offset;
                int jumpInsLen = (jumpInstruction as As3Instruction).Length;
                int target = jumpInsPos + jumpInsLen + jumpOffset;

                if (jumpOffset > 0)
                {
                    if (target > pos && pos > jumpInsPos)
                    {
                        jumpInstruction.Offset -= ins.Length;
                    }
                }
                if (jumpOffset < 0)
                {
                    if (target <= pos && pos < jumpInsPos)
                    {
                        jumpInstruction.Offset += insLen;
                    }
                }
            }

            foreach (var switchInstruction in _switchInstructions)
            {
                var caseIns = switchInstruction as As3Instruction;
                int casePos = _instructionPositions[caseIns];
                int[] indexes = new int[switchInstruction.CaseOffsets.Length + 1];
                for (int i = 0; i < switchInstruction.CaseOffsets.Length; i++)
                    indexes[i + 1] = switchInstruction.CaseOffsets[i];
                indexes[0] = switchInstruction.DefaultOffset;

                for (int i = 0; i < indexes.Length; i++)
                {
                    if (indexes[i] >= 0)
                        return;

                    if (casePos + indexes[i] <= pos && pos > casePos)
                    {
                        indexes[i] -= insLen; 
                    }
                }

                switchInstruction.DefaultOffset = indexes[0];
                for (int i = 0; i < indexes.Length - 1; i++)
                    switchInstruction.CaseOffsets[i] = indexes[i + 1];
            }

            if (ins is IJump)
            {
                _jumpInstructions.Remove(ins as IJump);
            }

            if (ins is ICaseSwitch)
            {
                _switchInstructions.Remove(ins as ICaseSwitch);
            }

            _instructions.Remove(ins);
            _instructionPositions.Remove(ins);
            for (int j = index; j < _instructions.Count; j++)
                _instructionPositions[_instructions[j]] -= insLen;
        }

        public void RemoveInstructionsByOpcodes(params Opcode[] opcodes)
        {
            int i = 0; 
            while(i < _instructions.Count)
            {
                As3Instruction ins = _instructions[i];
                if (Array.IndexOf(opcodes, ins.Opcode) != -1)
                {
                    if (ins is IJump)
                        RemoveJumpAt(i);
                    else
                        RemoveInstructionAt(i);
                }
                i++;
            }
        }

        public void RemoveInstructionsRange(int startIndex, int endIndex)
        {
            for (int i = startIndex; i <= endIndex; i++)
                RemoveInstructionAt(startIndex);
        }

        public void RemoveJumpAt(int index)
        {
            As3Instruction ins = _instructions[index];
            int offset = (ins as IJump).Offset;
            int currOffset = 0;

            if (offset > 0)
            {
                while (currOffset < offset)
                {
                    currOffset += _instructions[index].Length;
                    RemoveInstructionAt(index);
                }

                return;
            }

            if (offset < 0)
                throw new Exception();
            
        }

        public void MoveInstruction(int index, int newIndex)
        {
            if (index == newIndex) return;

            As3Instruction ins = _instructions[index];
            int pos = _instructionPositions[ins];
            RemoveInstructionAt(index);
            AddInstructionAt(ins, newIndex);
            if (ins is IJump)
            {
                int targetJump = pos + (ins as IJump).Offset;
                int newPos = _instructionPositions[ins];
                (ins as IJump).Offset = targetJump - newPos;
            }
        }

        public Opcode[] GetOpcodesAt(int index, int count)
        {
            Opcode[] result = new Opcode[count];
            for (int i = 0; i < count; i++)
                result[i] = _instructions[index + i].Opcode;
            return result;
        }

        public void Optimize(Optimization opt, uint matchesCount = 0)
        {
            int matches = 0;
            for (int i = 0; i < _instructions.Count - opt.Pattern.Length; i++)
            {
                bool equal = !opt.Pattern.Where((t, j) => _instructions[i + j].Opcode != t).Any();

                if (!equal) continue;
                opt.Function(this, ref i);
                matches++;

                if (matchesCount != 0 && matches >= matchesCount)
                    return;
            }
        }

        public string ToPCodeSource()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var ins in _instructions)
                builder.AppendLine(ins.ToString());
            return builder.ToString();
        }
    }
}
