using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy.Core
{
    internal struct Instruction
    {
        /// <summary>
        /// The opcode of the instruction.
        /// </summary>
        public Opcodes Opcode { get; set; }
        /// <summary>
        /// The condition of the instruction. If not set to <c>Conditions.NONE</c>, the instruction will only be executed if the condition is met.
        /// </summary>
        public Conditions Condition { get; set; }
        /// <summary>
        /// The total length of the instruction in bytes.
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// The minimum amount of machine cycles used to execute the instruction.
        /// </summary>
        public int MinCycleDuration { get; set; }
        /// <summary>
        /// The maximum amount of machine cycles used to execute the instruction.
        /// Only used if the current instruction is conditional, if the condition is met.
        /// </summary>
        public int MaxCycleDuration { get; set; }
        /// <summary>
        /// Determines whether the current instruction performs read/write operations to 16-bit registers.
        /// </summary>
        public bool Is16Bit { get; set; }
        /// <summary>
        /// The destination register of the instruction.
        /// Doubles as source if the instruction does not accept a second parameter.
        /// </summary>
        public Parameter Parameter0 { get; set; }
        /// <summary>
        /// The source register of the instruction.
        /// </summary>
        public Parameter Parameter1 { get; set; }

        public Instruction(Opcodes Opcode, Conditions Condition, int Length, int MinCycleDuration, int MaxCycleDuration, bool Is16Bit, Parameter Parameter0, Parameter Parameter1)
        {
            this.Opcode = Opcode;
            this.Condition = Condition;
            this.Length = Length;
            this.MinCycleDuration = MinCycleDuration;
            this.MaxCycleDuration = MaxCycleDuration;
            this.Is16Bit = Is16Bit;
            this.Parameter0 = Parameter0;
            this.Parameter1 = Parameter1;
        }
    }
}
