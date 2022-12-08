using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy.Core
{
    internal class Processor
    {
        private Memory _Memory = new Memory();
        private Interrupt _Interrupts = new Interrupt();

        private bool _Halted = false;
        private bool _Stopped = false;

        private void PerformCycle()
        {
            if (_Interrupts.InterruptPending())
                ProcessInterrupt();

            byte FetchedInstruction = _Memory.ReadByte(Registers.PC, true, true);
            Instruction DecodedInstruction = InstructionSet.RegularSet[FetchedInstruction];
            int AdvancedCycles = ProcessInstruction(DecodedInstruction);
        }

        private void ProcessInterrupt()
        {
            byte Address = _Interrupts.GetInterruptAddress();
            _Interrupts.DisableInterrupts();
        }

        private int ProcessInstruction(Instruction Instruction) => Instruction.Opcode switch
        {
            Opcodes.LD => ProcessLoadInstruction(Instruction),
            Opcodes.ADD => ProcessAddInstruction(Instruction),
            Opcodes.ADC => ProcessAddInstruction(Instruction),
            Opcodes.SUB => ProcessSubtractInstruction(Instruction),
            Opcodes.SBC => ProcessSubtractInstruction(Instruction),
            Opcodes.INC => ProcessIncrementInstruction(Instruction),
            Opcodes.DEC => ProcessDecrementInstruction(Instruction),
            Opcodes.AND => ProcessBitwiseInstruction(Instruction),
            Opcodes.OR => ProcessBitwiseInstruction(Instruction),
            Opcodes.XOR => ProcessBitwiseInstruction(Instruction),
            Opcodes.CP => ProcessCompareInstruction(Instruction),
            Opcodes.RES => ProcessAlterBitInstruction(Instruction),
            Opcodes.SET => ProcessAlterBitInstruction(Instruction),
            Opcodes.BIT => ProcessBitTestInstruction(Instruction),
            Opcodes.RL => ProcessRotationInstruction(Instruction),
            Opcodes.RR => ProcessRotationInstruction(Instruction),
            Opcodes.RLC => ProcessRotationInstruction(Instruction),
            Opcodes.RRC => ProcessRotationInstruction(Instruction),
            Opcodes.RLA => ProcessRotationInstruction(Instruction),
            Opcodes.RRA => ProcessRotationInstruction(Instruction),
            Opcodes.RLCA => ProcessRotationInstruction(Instruction),
            Opcodes.RRCA => ProcessRotationInstruction(Instruction),
            Opcodes.SLA => ProcessShiftInstruction(Instruction),
            Opcodes.SRA => ProcessShiftInstruction(Instruction),
            Opcodes.SRL => ProcessShiftInstruction(Instruction),
            Opcodes.SWAP => ProcessSwapInstruction(Instruction),
            Opcodes.JP => ProcessJumpInstruction(Instruction),
            Opcodes.JR => ProcessRelativeJumpInstruction(Instruction),
            Opcodes.CALL => ProcessCallInstruction(Instruction),
            Opcodes.RST => ProcessResetInstruction(Instruction),
            Opcodes.RET => ProcessReturnInstruction(Instruction),
            Opcodes.RETI => ProcessReturnInstruction(Instruction),
            Opcodes.PUSH => ProcessPushInstruction(Instruction),
            Opcodes.POP => ProcessPopInstruction(Instruction),
            Opcodes.CCF => ProcessComplementCarryFlagInstruction(Instruction),
            Opcodes.SCF => ProcessSetCarryFlagInstruction(Instruction),
            Opcodes.CPL => ProcessComplementAccumulatorInstruction(Instruction),
            Opcodes.DAA => ProcessDecimalAdjustInstruction(Instruction),
            Opcodes.EI => ProcessEnableInterruptsInstruction(Instruction),
            Opcodes.DI => ProcessDisableInterruptsInstruction(Instruction),
            Opcodes.NOP => ProcessIdleInstruction(Instruction),
            Opcodes.STOP => ProcessStopInstruction(Instruction),
            Opcodes.HALT => ProcessHaltInstruction(Instruction),
            Opcodes.PREFIX => ProcessPrefixInstruction(Instruction),
            _ => throw new NotImplementedException()
        };

        private int ProcessLoadInstruction(Instruction Instruction) => Instruction.Is16Bit ? Load16Bit(Instruction) : Load8Bit(Instruction);

        private int Load8Bit(Instruction Instruction)
        {
            byte Input = _Memory.ReadByte(Instruction.Parameter1.Register, Instruction.Parameter1.IsPointer, Instruction.Parameter1.IncreaseAfterFetch, Instruction.Parameter1.DecreaseAfterFetch);
            _Memory.WriteByte(Instruction.Parameter0.Register, Input, Instruction.Parameter0.IsPointer, Instruction.Parameter0.IncreaseAfterFetch, Instruction.Parameter0.DecreaseAfterFetch);
            return Instruction.MinCycleDuration;
        }

        private int Load16Bit(Instruction Instruction)
        {
            ushort Input = _Memory.ReadUshort(Instruction.Parameter1.Register, Instruction.Parameter1.IsPointer);
            _Memory.WriteUshort(Instruction.Parameter0.Register, Input, Instruction.Parameter0.IsPointer);
            return Instruction.MinCycleDuration;
        }

        /// <summary>
        /// <br>Performs an increment on the value stored in the specified register.</br>
        /// <para></para>
        /// <br>Opcodes handled: INC(8-bit), INC(16-bit)</br>
        /// <para></para>
        /// <br>Flags (8-bit):</br>
        /// <br>C: Unaffected.</br>
        /// <br>H: Set if the addition of the first 4 bits causes an overflow.</br>
        /// <br>N: Reset.</br>
        /// <br>Z: Set if the result of the increment is 0.</br>
        /// <para></para>
        /// <br>Flags (16-bit): Unaffected.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessIncrementInstruction(Instruction Instruction)
        {
            if (Instruction.Is16Bit)
            {
                ushort Value = _Memory.ReadUshort(Instruction.Parameter0.Register);
                _Memory.WriteUshort(Instruction.Parameter0.Register, Increment16Bit(Value));
            }
            else
            {
                byte Value = _Memory.ReadByte(Instruction.Parameter0.Register, Instruction.Parameter0.IsPointer);
                _Memory.WriteByte(Instruction.Parameter0.Register, Increment8Bit(Value), Instruction.Parameter0.IsPointer);
            }
            return Instruction.MinCycleDuration;
        }

        private byte Increment8Bit(byte Value)
        {
            _Memory.WriteFlags(Value == 0xFF, false, (Value & 0xF) == 0xF, null);
            return ++Value;
        }

        private ushort Increment16Bit(ushort Value) => ++Value;

        /// <summary>
        /// <br>Performs a decrement on the value stored in the specified register.</br>
        /// <para></para>
        /// <br>Opcodes handled: DEC(8-bit), DEC(16-bit)</br>
        /// <para></para>
        /// <br>Flags (8-bit):</br>
        /// <br>C: Unaffected.</br>
        /// <br>H: Set if the subtraction of the first 4 bits causes an underflow.</br>
        /// <br>N: Set.</br>
        /// <br>Z: Set if the result of the increment is 0.</br>
        /// <para></para>
        /// <br>Flags (16-bit): Unaffected.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessDecrementInstruction(Instruction Instruction)
        {
            if (Instruction.Is16Bit)
            {
                ushort Value = _Memory.ReadByte(Instruction.Parameter0.Register);
                _Memory.WriteUshort(Instruction.Parameter0.Register, Decrement16Bit(Value));
            }
            else
            {
                byte Value = _Memory.ReadByte(Instruction.Parameter0.Register, Instruction.Parameter0.IsPointer);
                _Memory.WriteByte(Instruction.Parameter0.Register, Decrement8Bit(Value), Instruction.Parameter0.IsPointer);
            }
            return Instruction.MinCycleDuration;
        }

        private byte Decrement8Bit(byte Value)
        {
            _Memory.WriteFlags(Value == 0x1, false, (Value & 0xF) == 0, null);
            return --Value;
        }

        private ushort Decrement16Bit(ushort Value) => --Value;

        /// <summary>
        /// <br>Performs an addition on the target register, using the value stored in the source register.</br>
        /// <para></para>
        /// <br>Opcodes handled: ADD(8-bit), ADD(16-bit), ADC</br>
        /// <para></para>
        /// <br>Flags:</br>
        /// <br>C: Set if the addition causes an overflow.</br>
        /// <br>H: Set if the addition of the first 4 (8-bit) or 12 (16-bit) bits causes an overflow.</br>
        /// <br>N: Reset.</br>
        /// <br>Z:</br>
        /// <br>   - ADD (8-bit), ADC: Set if the result of the addition is 0.</br>
        /// <br>   - ADD (16-bit): reset if the instruction is <c>ADD SP, i8</c>, otherwise unaltered.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessAddInstruction(Instruction Instruction)
        {
            if (Instruction.Is16Bit)
                return Add16Bit(Instruction);

            byte Accumulator = _Memory.ReadByte(Registers.A);
            byte Addition = _Memory.ReadByte(Instruction.Parameter0.Register, Instruction.Parameter0.IsPointer);
            Accumulator = Instruction.Opcode switch
            {
                Opcodes.ADD => Add8Bit(Accumulator, Addition),
                Opcodes.ADC => AddCarry(Accumulator, Addition),
                _ => throw new NotImplementedException()
            };
            _Memory.WriteByte(Registers.A, Accumulator);
            return Instruction.MinCycleDuration;
        }

        private byte Add8Bit(byte Value, byte Addition)
        {
            _Memory.WriteFlags(Value == 0, false, ((Value & 0xF) + (Addition & 0xF)) > 0xF, (Value + Addition) > 0xFF);
            return (byte)(Value + Addition);
        }

        private byte AddCarry(byte Value, byte Addition)
        {
            byte Carry = (byte)(_Memory.ReadFlags(Flags.C) ? 1 : 0);
            _Memory.WriteFlags(Value == 0, false, ((Value & 0xF) + (Addition & 0xF) + Carry) > 0xF, (Value + Addition + Carry) > 0xFF);
            return (byte)(Value + Addition + Carry);
        }

        private int Add16Bit(Instruction Instruction)
        {
            ushort Result = 0;
            if (Instruction.Parameter0.Register == Registers.SP)
            {
                short Value = Convert.ToInt16(_Memory.ReadUshort(Registers.SP));
                sbyte Addition = Convert.ToSByte(_Memory.ReadByte(Registers.PC, true, true));
                _Memory.WriteFlags(false, false, ((Value & 0xF) + (Addition & 0xF)) > 0xF, (Value + Addition) > 0xFF);
                Result = Convert.ToUInt16(Value + Addition);
            }
            else
            {
                ushort Value = _Memory.ReadUshort(Instruction.Parameter0.Register);
                ushort Addition = _Memory.ReadUshort(Instruction.Parameter1.Register);
                _Memory.WriteFlags(null, false, ((Value & 0xFFF) + (Addition & 0xFFF)) > 0xFFF, (Value + Addition) > 0xFFFF);
                Result = (ushort)(Value + Addition);
            }
            _Memory.WriteUshort(Instruction.Parameter0.Register, Result);
            return Instruction.MinCycleDuration;
        }

        /// <summary>
        /// <br>Subtracts the content of the specified 8-bit register from the accumulator.</br>
        /// <para></para>
        /// <br>Opcodes handled: SUB, SBC</br>
        /// <para></para>
        /// <br>Flags:</br>
        /// <br>C: Set if the value in the target register is larger than the accumulator.</br>
        /// <br>H: Set if the accumulator needs to borrow from the high nibble.</br>
        /// <br>N: Set.</br>
        /// <br>Z: Set if the result of th subtraction is 0.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessSubtractInstruction(Instruction Instruction)
        {
            byte Accumulator = _Memory.ReadByte(Registers.A);
            byte Subtraction = _Memory.ReadByte(Instruction.Parameter0.Register, Instruction.Parameter0.IsPointer);
            Accumulator = Instruction.Opcode switch
            {
                Opcodes.SUB => Subtract8Bit(Accumulator, Subtraction),
                Opcodes.SBC => SubtractCarry(Accumulator, Subtraction),
                _ => throw new NotImplementedException()
            };
            _Memory.WriteByte(Registers.A, Accumulator);
            return Instruction.MinCycleDuration;
        }

        private byte Subtract8Bit(byte Value, byte Subtraction)
        {
            _Memory.WriteFlags(Value == 0, true, (Value & 0xF) < (Subtraction & 0xF), Value < Subtraction);
            return (byte)(Value - Subtraction);
        }

        private byte SubtractCarry(byte Value, byte Subtraction)
        {
            byte Carry = (byte)(_Memory.ReadFlags(Flags.C) ? 1 : 0);
            _Memory.WriteFlags(Value == 0, true, (Value & 0xF) < ((Subtraction & 0xF) + Carry), Value < (Subtraction + Carry));
            return (byte)(Value - (Subtraction + Carry));
        }

        /// <summary>
        /// <br>Performs a bitwise operation on the value stored in the source register.</br>
        /// <para></para>
        /// <br>Opcodes handled: AND, OR, XOR</br>
        /// <para></para>
        /// <br>Flags:</br>
        /// <br>C: Reset.</br>
        /// <br>H:</br>
        /// <br>   - AND: Set.</br>
        /// <br>   - OR, XOR: Reset.</br>
        /// <br>N: Reset.</br>
        /// <br>Z: Set if the result of the bitwise operation is 0.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessBitwiseInstruction(Instruction Instruction)
        {
            byte Accumulator = _Memory.ReadByte(Registers.A);
            byte Comparison = _Memory.ReadByte(Instruction.Parameter0.Register, Instruction.Parameter0.IsPointer);
            Accumulator = Instruction.Opcode switch
            {
                Opcodes.AND => (byte)(Accumulator & Comparison),
                Opcodes.OR => (byte)(Accumulator | Comparison),
                Opcodes.XOR => (byte)(Accumulator ^ Comparison),
                _ => throw new NotImplementedException()
            };
            _Memory.WriteByte(Registers.A, Accumulator);
            _Memory.WriteFlags(Accumulator == 0, false, true, false);
            return Instruction.MinCycleDuration;
        }

        #region Rotation
        /// <summary>
        /// <br>Performs a logical/arythmic rotation on the value stored in the target register.</br>
        /// <para></para>
        /// <br>Opcodes handled: RL, RR, RLC, RRC, RLA, RRA, RLCA, RRCA</br>
        /// <para></para>
        /// <br>Flags:</br>
        /// <br>C: Set if the bit being rotated out is set.</br>
        /// <br>H: Reset.</br>
        /// <br>N: Reset.</br>
        /// <br>Z:</br>
        /// <br>   - RL, RR, RLC, RRC: Set if the result of the rotation is 0.</br>
        /// <br>   - RLA, RRA, RLCA, RRCA: Unaffected</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessRotationInstruction(Instruction Instruction)
        {
            byte Value = _Memory.ReadByte(Instruction.Parameter0.Register, Instruction.Parameter0.IsPointer);
            bool CurrentCarry = _Memory.ReadFlags(Flags.C);

            if (Instruction.Opcode == Opcodes.RLA || Instruction.Opcode == Opcodes.RRA ||
                Instruction.Opcode == Opcodes.RLCA || Instruction.Opcode == Opcodes.RRCA)
                Value = RotateA(Instruction.Opcode, Value, CurrentCarry);
            else
                Value = Rotate(Instruction.Opcode, Value, CurrentCarry);

            _Memory.WriteByte(Instruction.Parameter0.Register, Value, Instruction.Parameter0.IsPointer);
            return Instruction.MinCycleDuration;
        }

        private byte RotateA(Opcodes Opcode, byte Value, bool CurrentCarry) => Opcode switch
        {
            Opcodes.RLA => Rotate(Opcodes.RL, Value, CurrentCarry, true),
            Opcodes.RRA => Rotate(Opcodes.RR, Value, CurrentCarry, true),
            Opcodes.RLCA => Rotate(Opcodes.RLC, Value, CurrentCarry, true),
            Opcodes.RRCA => Rotate(Opcodes.RRC, Value, CurrentCarry, true),
            _ => throw new NotImplementedException()
        };

        private byte Rotate(Opcodes Opcode, byte Value, bool CurrentCarry, bool IsRotateA = false)
        {
            bool NewCarry = Opcode switch
            {
                Opcodes.RL => (Value >> 7) > 0,
                Opcodes.RR => (Value & 0x1) > 0,
                Opcodes.RLC => (Value >> 7) > 0,
                Opcodes.RRC => (Value & 0x1) > 0,
                _ => throw new NotImplementedException()
            };
            byte Result = Opcode switch
            {
                Opcodes.RL => (byte)((Value << 1) | (CurrentCarry ? 1 : 0)),
                Opcodes.RR => (byte)((Value >> 1) | (CurrentCarry ? 0x80 : 0)),
                Opcodes.RLC => (byte)((Value << 1) | (NewCarry ? 1 : 0)),
                Opcodes.RRC => (byte)((Value >> 1) | (NewCarry ? 0x80 : 0)),
                _ => throw new NotImplementedException()
            };
            if (IsRotateA)
                _Memory.WriteFlags(false, false, false, NewCarry);
            else
                _Memory.WriteFlags(Result == 0, false, false, NewCarry);
            return Result;
        }
        #endregion

        /// <summary>
        /// <br>Performs a logical/arythmic shift in the direction determined by the opcode on the value stored in the specified register.</br>
        /// <para></para>
        /// <br>Opcodes handled: SLA, SRA, SRL</br>
        /// <para></para>
        /// <br>Flags:</br>
        /// <br>C: Set if the bit shifted out is 1.</br>
        /// <br>H: Reset.</br>
        /// <br>N: Reset.</br>
        /// <br>Z: Set if the result of the shift zero.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessShiftInstruction(Instruction Instruction)
        {
            byte Value = _Memory.ReadByte(Instruction.Parameter0.Register, Instruction.Parameter0.IsPointer);
            bool NewCarry = false;
            switch (Instruction.Opcode)
            {
                case Opcodes.SLA:
                    NewCarry = (Value & 0x80) != 0;
                    Value <<= 1;
                    break;
                case Opcodes.SRA:
                    NewCarry = (Value & 0x1) != 0;
                    Value = (byte)((Value & 0x80) | (Value >> 1));
                    break;
                case Opcodes.SRL:
                    NewCarry = (Value & 0x1) != 0;
                    Value >>= 1;
                    break;
                default:
                    throw new NotImplementedException();
            }
            _Memory.WriteByte(Instruction.Parameter0.Register, Value, Instruction.Parameter0.IsPointer);
            _Memory.WriteFlags(Value == 0, false, false, NewCarry);
            return Instruction.MinCycleDuration;
        }

        /// <summary>
        /// <br>Swaps the higher and lower nibble of the value stored in the selected register.</br>
        /// <para></para>
        /// <br>Opcodes handled: SWAP</br>
        /// <para></para>
        /// <br>Flags: Unaltered.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessSwapInstruction(Instruction Instruction)
        {
            byte Value = _Memory.ReadByte(Instruction.Parameter0.Register, Instruction.Parameter0.IsPointer);
            Value = (byte)((Value << 4) | (Value >> 4));
            _Memory.WriteByte(Instruction.Parameter0.Register, Value, Instruction.Parameter0.IsPointer);
            _Memory.WriteFlags(Value == 0, false, false, false);
            return Instruction.MinCycleDuration;
        }

        /// <summary>
        /// <br>Tests a bit in the specified register, bit index based on a proprietary number.</br>
        /// <para></para>
        /// <br>Opcodes handled: BIT</br>
        /// <para></para>
        /// <br>Flags:</br>
        /// <br>C: Unaltered.</br>
        /// <br>H: Set.</br>
        /// <br>N: Reset.</br>
        /// <br>Z: Set if the tested bit is zero.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessBitTestInstruction(Instruction Instruction)
        {
            byte Value = _Memory.ReadByte(Instruction.Parameter0.Register, Instruction.Parameter0.IsPointer);
            bool IsZero = (Value & (0x1 << Instruction.Parameter0.Value)) == 0;
            _Memory.WriteFlags(IsZero, false, true, null);
            return Instruction.MinCycleDuration;
        }

        /// <summary>
        /// <br>(Re)sets a bit in the specified register, bit index based on a proprietary number.</br>
        /// <para></para>
        /// <br>Opcodes handled: SET, RES</br>
        /// <para></para>
        /// <br>Flags: Unaltered.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessAlterBitInstruction(Instruction Instruction)
        {
            byte Value = _Memory.ReadByte(Instruction.Parameter0.Register, Instruction.Parameter0.IsPointer);
            Value = Instruction.Opcode switch
            {
                Opcodes.SET => (byte)(Value | (0x1 << Instruction.Parameter0.Value)),
                Opcodes.RES => (byte)(Value & ~(0x1 << Instruction.Parameter0.Value)),
                _ => throw new NotImplementedException()
            };
            _Memory.WriteByte(Instruction.Parameter0.Register, Value, Instruction.Parameter0.IsPointer);
            return Instruction.MinCycleDuration;
        }

        /// <summary>
        /// <br>Subtracts the content of the specified 8-bit register from the accumulator.</br>
        /// <br>The result of this subtraction is thrown away.</br>
        /// <para></para>
        /// <br>Opcodes handled: CP</br>
        /// <para></para>
        /// <br>Flags:</br>
        /// <br>C: Set if the value in the target register is larger than the accumulator.</br>
        /// <br>H: Set if the accumulator needs to borrow from the high nibble.</br>
        /// <br>N: Set.</br>
        /// <br>Z: Set if the result of th subtraction is 0.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessCompareInstruction(Instruction Instruction)
        {
            byte Value = _Memory.ReadByte(Registers.A);
            byte Comparison = _Memory.ReadByte(Instruction.Parameter0.Register, Instruction.Parameter0.IsPointer);
            _Memory.WriteFlags(Value == Comparison, true, (Value & 0xF) < (Comparison & 0xF), Value < Comparison);
            return Instruction.MinCycleDuration;
        }

        /// <summary>
        /// <br>Complements the value stored in the carry flag.</br>
        /// <para></para>
        /// <br>Opcodes handled: CCF</br>
        /// <para></para>
        /// <br>Flags:</br>
        /// <br>C: Set if previously unset, unset if previously set</br>
        /// <br>H: Reset.</br>
        /// <br>N: Reset.</br>
        /// <br>Z: Unaltered.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessComplementCarryFlagInstruction(Instruction Instruction)
        {
            bool Carry = _Memory.ReadFlags(Flags.C);
            _Memory.WriteFlags(null, false, false, !Carry);
            return Instruction.MinCycleDuration;
        }

        private int ProcessComplementAccumulatorInstruction(Instruction Instruction)
        {
            byte Accumulator = _Memory.ReadByte(Registers.A);
            Accumulator = (byte)~Accumulator;
            _Memory.WriteByte(Registers.A, Accumulator);
            _Memory.WriteFlags(null, false, false, null);
            return Instruction.MinCycleDuration;
        }

        private int ProcessSetCarryFlagInstruction(Instruction Instruction)
        {
            _Memory.WriteFlags(null, false, false, true);
            return Instruction.MinCycleDuration;
        }

        private int ProcessDecimalAdjustInstruction(Instruction Instruction)
        {
            byte Value = _Memory.ReadByte(Registers.A);
            bool Subtraction = _Memory.ReadFlags(Flags.N);
            bool HalfCarry = _Memory.ReadFlags(Flags.H);
            bool Carry = _Memory.ReadFlags(Flags.C);

            if (Subtraction)
            {
                if (Value > 0x99 || Carry)
                {
                    Value -= 0x60;
                    Carry = true;
                }
                if ((Value & 0xF) > 0x9 || HalfCarry)
                    Value -= 0x6;
            }
            else
            {
                if (Value > 0x99 || Carry)
                {
                    Value += 0x60;
                    Carry = true;
                }
                else
                    Carry = false;
                if ((Value & 0xF) > 0x9 || HalfCarry)
                    Value += 0x6;
            }
            _Memory.WriteByte(Registers.A, Value);
            _Memory.WriteFlags(Value == 0, null, false, Carry);
            return Instruction.MinCycleDuration;
        }

        /// <summary>
        /// <br>Makes a jump to the address contained within the specified 16-bit register.</br>
        /// <para></para>
        /// <br>Opcodes handled: JP</br>
        /// <para></para>
        /// <br>Flags: Unaltered.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessJumpInstruction(Instruction Instruction)
        {
            ushort Address = _Memory.ReadUshort(Instruction.Parameter0.Register);
            if (ConditionMet(Instruction))
            {
                _Memory.WriteUshort(Registers.PC, Address);
                return Instruction.MaxCycleDuration;
            }
            return Instruction.MinCycleDuration;
        }

        /// <summary>
        /// <br>Makes a jump the size of the specified register, relative to the current program counter value.</br>
        /// <para></para>
        /// <br>Opcodes handled: JR</br>
        /// <para></para>
        /// <br>Flags: Unaltered.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessRelativeJumpInstruction(Instruction Instruction)
        {
            sbyte Offset = Convert.ToSByte(_Memory.ReadByte(Registers.PC, true, true));
            if (ConditionMet(Instruction))
            {
                ushort CurrentAddress = _Memory.ReadUshort(Registers.PC);
                short Result = (short)(Convert.ToInt16(CurrentAddress) + Offset);
                _Memory.WriteUshort(Registers.PC, Convert.ToUInt16(Result));
                return Instruction.MaxCycleDuration;
            }
            return Instruction.MinCycleDuration;
        }

        /// <summary>
        /// <br>Pushes the value of the program counter to the stack and jumps to the address specified in the target register.</br>
        /// <para></para>
        /// <br>Opcodes handled: CALL</br>
        /// <para></para>
        /// <br>Flags: Unaltered.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessCallInstruction(Instruction Instruction)
        {
            ushort NewAddress = _Memory.ReadUshort(Registers.PC, true, true);
            if (ConditionMet(Instruction))
            {
                ProcessPushInstruction(Instruction);
                _Memory.WriteUshort(Registers.PC, NewAddress);
                return Instruction.MaxCycleDuration;
            }
            return Instruction.MinCycleDuration;
        }

        /// <summary>
        /// <br>Pops a value from the stack and assigns it to the program counter.</br>
        /// <br>Also enables interrupts in case of the RETI instruction</br>
        /// <para></para>
        /// <br>Opcodes handled: RET, RETI</br>
        /// <para></para>
        /// <br>Flags: Unaltered.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessReturnInstruction(Instruction Instruction)
        {
            if (ConditionMet(Instruction))
            {
                ProcessPopInstruction(Instruction);
                if (Instruction.Opcode == Opcodes.RETI)
                    ProcessEnableInterruptsInstruction(Instruction);
                return Instruction.MaxCycleDuration;
            }
            return Instruction.MinCycleDuration;
        }

        /// <summary>
        /// <br>Resets the program counter to a proprietary value.</br>
        /// <para></para>
        /// <br>Opcodes handled: RST</br>
        /// <para></para>
        /// <br>Flags: Unaltered.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessResetInstruction(Instruction Instruction)
        {
            ProcessPushInstruction(Instruction);
            _Memory.WriteUshort(Registers.PC, Instruction.Parameter0.Value);
            return Instruction.MaxCycleDuration;
        }

        /// <summary>
        /// <br>Pushes the value of the specified 16-bit register to the stack.</br>
        /// <para></para>
        /// <br>Opcodes handled: PUSH</br>
        /// <para></para>
        /// <br>Flags: Unaltered.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessPushInstruction(Instruction Instruction)
        {
            ushort Value = _Memory.ReadUshort(Instruction.Parameter0.Register);
            _Memory.WriteUshort(Registers.SP, Value, true, false, true);
            return Instruction.MinCycleDuration;
        }

        /// <summary>
        /// <br>Pops a value from the stack and assigns it to the specified 16-bit register</br>
        /// <para></para>
        /// <br>Opcodes handled: POP</br>
        /// <para></para>
        /// <br>Flags: Unaltered.</br>
        /// </summary>
        /// <param name="Instruction">The decoded instruction.</param>
        /// <returns>The amount of machine cycles passed.</returns>
        private int ProcessPopInstruction(Instruction Instruction)
        {
            ushort Value = _Memory.ReadUshort(Registers.SP, true, true);
            _Memory.WriteUshort(Instruction.Parameter0.Register, Value);
            return Instruction.MinCycleDuration;
        }

        private int ProcessEnableInterruptsInstruction(Instruction Instruction)
        {
            _Interrupts.EnableInterrupts();
            return Instruction.MinCycleDuration;
        }

        private int ProcessDisableInterruptsInstruction(Instruction Instruction)
        {
            _Interrupts.DisableInterrupts();
            return Instruction.MinCycleDuration;
        }

        private int ProcessIdleInstruction(Instruction Instruction) => Instruction.MinCycleDuration;

        private int ProcessHaltInstruction(Instruction Instruction)
        {
            throw new NotImplementedException();
        }

        private int ProcessStopInstruction(Instruction Instruction)
        {
            throw new NotImplementedException();
        }

        private int ProcessPrefixInstruction(Instruction Instruction)
        {
            throw new NotImplementedException();
        }

        private bool ConditionMet(Instruction Instruction)
        {
            bool Carry = _Memory.ReadFlags(Flags.C);
            bool Zero = _Memory.ReadFlags(Flags.Z);
            return Instruction.Condition switch
            {
                Conditions.NONE => true,
                Conditions.ZERO => Zero,
                Conditions.NOTZERO => !Zero,
                Conditions.CARRY => Carry,
                Conditions.NOCARRY => !Carry,
                _ => throw new NotImplementedException()
            };
        }
    }
}
