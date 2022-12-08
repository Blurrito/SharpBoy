using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBoy.Core;

namespace SharpBoy.Core
{
    internal class Memory
    {
        private byte _A = 0;
        private byte _F = 0;
        private byte _B = 0;
        private byte _C = 0;
        private byte _D = 0;
        private byte _E = 0;
        private byte _H = 0;
        private byte _L = 0;
        private ushort _SP = 0;
        private ushort _PC = 0;

        public Memory()
        {

        }

        /// <summary>
        /// Reads a byte from the specified register.
        /// </summary>
        /// <param name="Register">The register from which the value will be read.</param>
        /// <param name="IsPointer">Determines whether the specified register should be treated as a pointer.</param>
        /// <param name="IncreaseAfterFetch">Determines whether the pointer should be increased after the read operation has completed.</param>
        /// <param name="DecreaseAfterFetch">Determines whether the pointer should be decreased after the read operation has completed. Will be ignored if <c>IncreaseAfterFetch</c> is set.</param>
        public byte ReadByte(Registers Register, bool IsPointer = false, bool IncreaseAfterFetch = false, bool DecreaseAfterFetch = false)
        {
            if (IsPointer)
            {
                ushort Pointer = ReadUshortRegister(Register);
                if (IncreaseAfterFetch)
                    WriteUshortRegister(Register, (ushort)(Pointer + 1));
                else if (DecreaseAfterFetch)
                    WriteUshortRegister(Register, (ushort)(Pointer - 1));
                return ReadByte(Pointer);
            }
            else
                return ReadByteRegister(Register);
        }

        public byte ReadByte(ushort Address)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads an unsigned short from the specified register.
        /// </summary>
        /// <param name="Register">The register from which the value will be read.</param>
        /// <param name="IsPointer">Determines whether the specified register should be treated as a pointer.</param>
        /// <param name="IncreaseAfterFetch">Determines whether the pointer should be increased after the read operation has completed.</param>
        /// <param name="DecreaseAfterFetch">Determines whether the pointer should be decreased after the read operation has completed. Will be ignored if <c>IncreaseAfterFetch</c> is set.</param>
        public ushort ReadUshort(Registers Register, bool IsPointer = false, bool IncreaseAfterFetch = false, bool DecreaseAfterFetch = false)
        {
            if (Register == Registers.SP && IncreaseAfterFetch)
            {
                short Value = Convert.ToInt16(ReadUshortRegister(Register));
                sbyte Addition = Convert.ToSByte(ReadByteRegister(Registers.u8));
                WriteFlags(false, false, ((Value & 0xF) + (Addition & 0xF)) > 0xF, (Value + Addition) > 0xFF);
                return Convert.ToUInt16(Value + Addition);
            }

            if (IsPointer)
            {
                ushort Pointer = ReadUshortRegister(Register);
                if (IncreaseAfterFetch)
                    WriteUshortRegister(Register, (ushort)(Pointer + 2));
                else if (DecreaseAfterFetch)
                    WriteUshortRegister(Register, (ushort)(Pointer - 2));
                return ReadUshort(Pointer);
            }
            else
                return ReadUshortRegister(Register);
        }

        public ushort ReadUshort(ushort Address)
        {
            throw new NotImplementedException();
        }

        private byte ReadByteRegister(Registers Register) => Register switch
        {
            Registers.A => _A,
            Registers.F => _F,
            Registers.B => _B,
            Registers.C => _C,
            Registers.D => _D,
            Registers.E => _E,
            Registers.H => _H,
            Registers.L => _L,
            _ => throw new NotImplementedException()
        };

        private ushort ReadUshortRegister(Registers Register) => Register switch
        {
            Registers.AF => (ushort)((_A << 8) | _F),
            Registers.BC => (ushort)((_B << 8) | _C),
            Registers.DE => (ushort)((_D << 8) | _E),
            Registers.HL => (ushort)((_H << 8) | _L),
            Registers.SP => _SP,
            Registers.PC => _PC,
            _ => throw new NotImplementedException()
        };

        public bool ReadFlags(Flags Flag) => Flag switch
        {
            Flags.C => (_F & 0x10) != 0,
            Flags.H => (_F & 0x20) != 0,
            Flags.N => (_F & 0x40) != 0,
            Flags.Z => (_F & 0x80) != 0,
            _ => throw new NotImplementedException()
        };

        /// <summary>
        /// Writes a byte to the specified register.
        /// </summary>
        /// <param name="Register">The register to which the value will be written.</param>
        /// <param name="Value">The value to write.</param>
        /// <param name="IsPointer">Determines whether the specified register should be treated as a pointer.</param>
        /// <param name="IncreaseBeforeWrite">Determines whether the pointer should be increased before the write operation will execute.</param>
        /// <param name="DecreaseBeforeWrite">Determines whether the pointer should be decreased before the write operation will execute. Will be ignored if <c>IncreaseAfterFetch</c> is set.</param>
        public void WriteByte(Registers Register, byte Value, bool IsPointer = false, bool IncreaseBeforeWrite = false, bool DecreaseBeforeWrite = false)
        {
            if (IsPointer)
            {
                ushort Pointer = ReadUshortRegister(Register);
                if (IncreaseBeforeWrite)
                    WriteUshortRegister(Register, ++Pointer);
                else if (DecreaseBeforeWrite)
                    WriteUshortRegister(Register, --Pointer);
                WriteByte(Pointer, Value);
            }
            else
                WriteByteRegister(Register, Value);
        }

        public void WriteByte(ushort Address, byte Value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes an unsigned short to the specified register.
        /// </summary>
        /// <param name="Register">The register to which the value will be written.</param>
        /// <param name="Value">The value to write.</param>
        /// <param name="IsPointer">Determines whether the specified register should be treated as a pointer.</param>
        /// <param name="IncreaseBeforeWrite">Determines whether the pointer should be increased before the write operation will execute.</param>
        /// <param name="DecreaseBeforeWrite">Determines whether the pointer should be decreased before the write operation will execute. Will be ignored if <c>IncreaseAfterFetch</c> is set.</param>
        public void WriteUshort(Registers Register, ushort Value, bool IsPointer = false, bool IncreaseBeforeWrite = false, bool DecreaseBeforeWrite = false)
        {
            if (IsPointer)
            {
                ushort Pointer = ReadUshortRegister(Register);
                if (IncreaseBeforeWrite)
                    WriteUshortRegister(Register, Pointer += 2);
                else if (DecreaseBeforeWrite)
                    WriteUshortRegister(Register, Pointer -= 2);
                WriteUshort(Pointer, Value);
            }
            else
                WriteUshortRegister(Register, Value);
        }

        public void WriteUshort(ushort Address, ushort Value)
        {
            throw new NotImplementedException();
        }

        private void WriteByteRegister(Registers Register, byte Value)
        {
            switch (Register)
            {
                case Registers.A:
                    _A = Value;
                    break;
                case Registers.F:
                    _F = Value;
                    break;
                case Registers.B:
                    _B = Value;
                    break;
                case Registers.C:
                    _C = Value;
                    break;
                case Registers.D:
                    _D = Value;
                    break;
                case Registers.E:
                    _E = Value;
                    break;
                case Registers.H:
                    _H = Value;
                    break;
                case Registers.L:
                    _L = Value;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void WriteUshortRegister(Registers Register, ushort Value)
        {
            switch (Register)
            {
                case Registers.AF:
                    _A = (byte)(Value >> 8);
                    _F = (byte)(Value & 0xFF);
                    break;
                case Registers.BC:
                    _B = (byte)(Value >> 8);
                    _C = (byte)(Value & 0xFF);
                    break;
                case Registers.DE:
                    _D = (byte)(Value >> 8);
                    _E = (byte)(Value & 0xFF);
                    break;
                case Registers.HL:
                    _H = (byte)(Value >> 8);
                    _L = (byte)(Value & 0xFF);
                    break;
                case Registers.SP:
                    _SP = Value;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void WriteFlags(bool? Zero = null, bool? Subtraction = null, bool? HalfCarry = null, bool? Carry = null)
        {
            if (Zero != null)
                _F = (bool)Zero ? (byte)(_F | 0x80) : (byte)(_F & 0x7F);
            if (Subtraction != null)
                _F = (bool)Subtraction ? (byte)(_F | 0x40) : (byte)(_F & 0xBF);
            if (HalfCarry != null)
                _F = (bool)HalfCarry ? (byte)(_F | 0x20) : (byte)(_F & 0xDF);
            if (Carry != null)
                _F = (bool)Carry ? (byte)(_F | 0x10) : (byte)(_F & 0xEF);
        }
    }
}
