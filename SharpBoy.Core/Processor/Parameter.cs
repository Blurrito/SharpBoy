using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy.Core
{
    internal struct Parameter
    {
        /// <summary>
        /// The register this parameter reads from/writes to.
        /// </summary>
        public Registers Register { get; set; }
        /// <summary>
        /// Determines whether the value stored in the register should be handled as a pointer.
        /// </summary>
        public bool IsPointer { get; set; }
        /// <summary>
        /// The proprietary value assigned to this instruction.
        /// (e.g. shift distance in bit shift instructions, destination address in reset instructions)
        /// </summary>
        public byte Value { get; set; }
        /// <summary>
        /// Determines whether the value stored in the register should be incremented after performing the instruction.
        /// </summary>
        public bool IncreaseAfterFetch { get; set; }
        /// <summary>
        /// Determines whether the value stored in the register should be decremented after perfoming the instruction
        /// </summary>
        public bool DecreaseAfterFetch { get; set; }

        public Parameter(Registers Register, bool IsPointer = false, byte Value = 0, bool IncreaseAfterFetch = false, bool DecreaseAfterFetch = false)
        {
            this.Register = Register;
            this.Value = Value;
            this.IsPointer = IsPointer;
            this.IncreaseAfterFetch = IncreaseAfterFetch;
            this.DecreaseAfterFetch = DecreaseAfterFetch;
        }
    }
}
