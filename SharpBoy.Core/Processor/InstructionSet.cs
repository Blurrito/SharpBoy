using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy.Core
{
    internal static class InstructionSet
    {
        public static readonly List<Instruction> RegularSet = new List<Instruction>(256)
        {
            #region 0x00 - 0x0F
            new Instruction(
                Opcodes.NOP,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                3,
                12,
                12,
                true,
                new Parameter(
                    Registers.BC),
                new Parameter(
                    Registers.u16)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.BC,
                    true),
                new Parameter(
                    Registers.A)),
            new Instruction(
                Opcodes.INC,
                Conditions.NONE,
                1,
                8,
                8,
                true,
                new Parameter(
                    Registers.BC),
                new Parameter()),
            new Instruction(
                Opcodes.INC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter()),
            new Instruction(
                Opcodes.DEC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                2,
                8,
                8,
                false,
                new Parameter(
                    Registers.B),
                new Parameter(
                    Registers.u8)),
            new Instruction(
                Opcodes.RLCA,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                3,
                20,
                20,
                true,
                new Parameter(
                    Registers.u16),
                new Parameter(
                    Registers.SP)),
            new Instruction(
                Opcodes.ADD,
                Conditions.NONE,
                1,
                8,
                8,
                true,
                new Parameter(
                    Registers.HL),
                new Parameter(
                    Registers.BC)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.A),
                new Parameter(
                    Registers.BC,
                    true)),
            new Instruction(
                Opcodes.DEC,
                Conditions.NONE,
                1,
                8,
                8,
                true,
                new Parameter(
                    Registers.BC),
                new Parameter()),
            new Instruction(
                Opcodes.INC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter()),
            new Instruction(
                Opcodes.DEC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                2,
                8,
                8,
                false,
                new Parameter(
                    Registers.C),
                new Parameter(
                    Registers.u8)),
            new Instruction(
                Opcodes.RRCA,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter()),
            #endregion
            #region 0x10 - 0x1F
            new Instruction(
                Opcodes.STOP,
                Conditions.NONE,
                2,
                8,
                8,
                false,
                new Parameter(
                    Registers.u8),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                3,
                12,
                12,
                true,
                new Parameter(
                    Registers.DE),
                new Parameter(
                    Registers.u16)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.DE,
                    true),
                new Parameter(
                    Registers.A)),
            new Instruction(
                Opcodes.INC,
                Conditions.NONE,
                1,
                8,
                8,
                true,
                new Parameter(
                    Registers.DE),
                new Parameter()),
            new Instruction(
                Opcodes.INC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter()),
            new Instruction(
                Opcodes.DEC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                2,
                8,
                8,
                false,
                new Parameter(
                    Registers.D),
                new Parameter(
                    Registers.u8)),
            new Instruction(
                Opcodes.RLA,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter()),
            new Instruction(
                Opcodes.JR,
                Conditions.NONE,
                2,
                12,
                12,
                true,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.A),
                new Parameter(
                    Registers.DE,
                    true)),
            new Instruction(
                Opcodes.DEC,
                Conditions.NONE,
                1,
                8,
                8,
                true,
                new Parameter(
                    Registers.DE),
                new Parameter()),
            new Instruction(
                Opcodes.INC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter()),
            new Instruction(
                Opcodes.DEC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                2,
                8,
                8,
                false,
                new Parameter(
                    Registers.E),
                new Parameter(
                    Registers.u8)),
            new Instruction(
                Opcodes.RRA,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter()),
            #endregion
            #region 0x20 - 0x2F
            new Instruction(
                Opcodes.JR,
                Conditions.NOTZERO,
                2,
                8,
                12,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                3,
                12,
                12,
                true,
                new Parameter(
                    Registers.HL),
                new Parameter(
                    Registers.u16)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.HL,
                    true,
                    0,
                    true),
                new Parameter(
                    Registers.A)),
            new Instruction(
                Opcodes.INC,
                Conditions.NONE,
                1,
                8,
                8,
                true,
                new Parameter(
                    Registers.HL),
                new Parameter()),
            new Instruction(
                Opcodes.INC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter()),
            new Instruction(
                Opcodes.DEC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                2,
                8,
                8,
                false,
                new Parameter(
                    Registers.H),
                new Parameter(
                    Registers.u8)),
            new Instruction(
                Opcodes.DAA,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.JR,
                Conditions.ZERO,
                2,
                8,
                12,
                true,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.A),
                new Parameter(
                    Registers.HL,
                    true)),
            new Instruction(
                Opcodes.DEC,
                Conditions.NONE,
                1,
                8,
                8,
                true,
                new Parameter(
                    Registers.HL),
                new Parameter()),
            new Instruction(
                Opcodes.INC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter()),
            new Instruction(
                Opcodes.DEC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                2,
                8,
                8,
                false,
                new Parameter(
                    Registers.L),
                new Parameter(
                    Registers.u8)),
            new Instruction(
                Opcodes.CPL,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(),
                new Parameter()),
            #endregion
            #region 0x30 - 0x3F
            new Instruction(
                Opcodes.JR,
                Conditions.NOCARRY,
                2,
                8,
                12,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                3,
                12,
                12,
                true,
                new Parameter(
                    Registers.SP),
                new Parameter(
                    Registers.u16)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.HL,
                    true,
                    0,
                    false,
                    true),
                new Parameter(
                    Registers.A)),
            new Instruction(
                Opcodes.INC,
                Conditions.NONE,
                1,
                8,
                8,
                true,
                new Parameter(
                    Registers.SP),
                new Parameter()),
            new Instruction(
                Opcodes.INC,
                Conditions.NONE,
                1,
                12,
                12,
                false,
                new Parameter(
                    Registers.HL,
                    false),
                new Parameter()),
            new Instruction(
                Opcodes.DEC,
                Conditions.NONE,
                1,
                12,
                12,
                false,
                new Parameter(
                    Registers.HL,
                    false),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                2,
                12,
                12,
                false,
                new Parameter(
                    Registers.HL,
                    true),
                new Parameter(
                    Registers.u8)),
            new Instruction(
                Opcodes.SCF,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.JR,
                Conditions.CARRY,
                2,
                8,
                12,
                true,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.A),
                new Parameter(
                    Registers.HL,
                    true,
                    0,
                    false,
                    true)),
            new Instruction(
                Opcodes.DEC,
                Conditions.NONE,
                1,
                8,
                8,
                true,
                new Parameter(
                    Registers.SP),
                new Parameter()),
            new Instruction(
                Opcodes.INC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter()),
            new Instruction(
                Opcodes.DEC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                2,
                8,
                8,
                false,
                new Parameter(
                    Registers.A),
                new Parameter(
                    Registers.u8)),
            new Instruction(
                Opcodes.CCF,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(),
                new Parameter()),
            #endregion
            #region 0x40 - 0x4F
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter(
                    Registers.B)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter(
                    Registers.C)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter(
                    Registers.D)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter(
                    Registers.E)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter(
                    Registers.H)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter(
                    Registers.L)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.B),
                new Parameter(
                    Registers.HL,
                    true)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter(
                    Registers.A)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter(
                    Registers.B)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter(
                    Registers.C)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter(
                    Registers.D)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter(
                    Registers.E)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter(
                    Registers.H)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter(
                    Registers.L)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.C),
                new Parameter(
                    Registers.HL,
                    true)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter(
                    Registers.A)),
            #endregion
            #region 0x50 - 0x5F
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter(
                    Registers.B)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter(
                    Registers.C)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter(
                    Registers.D)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter(
                    Registers.E)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter(
                    Registers.H)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter(
                    Registers.L)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.D),
                new Parameter(
                    Registers.HL,
                    true)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter(
                    Registers.A)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter(
                    Registers.B)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter(
                    Registers.C)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter(
                    Registers.D)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter(
                    Registers.E)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter(
                    Registers.H)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter(
                    Registers.L)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.E),
                new Parameter(
                    Registers.HL,
                    true)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter(
                    Registers.A)),
            #endregion
            #region 0x60 - 0x6F
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter(
                    Registers.B)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter(
                    Registers.C)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter(
                    Registers.D)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter(
                    Registers.E)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter(
                    Registers.H)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter(
                    Registers.L)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.H),
                new Parameter(
                    Registers.HL,
                    true)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter(
                    Registers.A)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter(
                    Registers.B)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter(
                    Registers.C)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter(
                    Registers.D)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter(
                    Registers.E)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter(
                    Registers.H)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter(
                    Registers.L)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.L),
                new Parameter(
                    Registers.HL,
                    true)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter(
                    Registers.A)),
            #endregion
            #region 0x70 - 0x7F
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.HL,
                    true),
                new Parameter(
                    Registers.B)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.HL,
                    true),
                new Parameter(
                    Registers.C)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.HL,
                    true),
                new Parameter(
                    Registers.D)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.HL,
                    true),
                new Parameter(
                    Registers.E)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.HL,
                    true),
                new Parameter(
                    Registers.H)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.HL,
                    true),
                new Parameter(
                    Registers.L)),
            new Instruction(
                Opcodes.HALT,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.HL,
                    true),
                new Parameter(
                    Registers.A)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter(
                    Registers.B)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter(
                    Registers.C)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter(
                    Registers.D)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter(
                    Registers.E)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter(
                    Registers.H)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter(
                    Registers.L)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.A),
                new Parameter(
                    Registers.HL,
                    true)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter(
                    Registers.A)),
            #endregion
            #region 0x80 - 0x8F
            new Instruction(
                Opcodes.ADD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter()),
            new Instruction(
                Opcodes.ADD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter()),
            new Instruction(
                Opcodes.ADD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter()),
            new Instruction(
                Opcodes.ADD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter()),
            new Instruction(
                Opcodes.ADD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter()),
            new Instruction(
                Opcodes.ADD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter()),
            new Instruction(
                Opcodes.ADD,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.HL,
                    true),
                new Parameter()),
            new Instruction(
                Opcodes.ADD,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter()),
            new Instruction(
                Opcodes.ADC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter()),
            new Instruction(
                Opcodes.ADC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter()),
            new Instruction(
                Opcodes.ADC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter()),
            new Instruction(
                Opcodes.ADC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter()),
            new Instruction(
                Opcodes.ADC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter()),
            new Instruction(
                Opcodes.ADC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter()),
            new Instruction(
                Opcodes.ADC,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.HL,
                    true),
                new Parameter()),
            new Instruction(
                Opcodes.ADC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter()),
            #endregion
            #region 0x90 - 0x9F
            new Instruction(
                Opcodes.SUB,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter()),
            new Instruction(
                Opcodes.SUB,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter()),
            new Instruction(
                Opcodes.SUB,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter()),
            new Instruction(
                Opcodes.SUB,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter()),
            new Instruction(
                Opcodes.SUB,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter()),
            new Instruction(
                Opcodes.SUB,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter()),
            new Instruction(
                Opcodes.SUB,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.HL,
                    true),
                new Parameter()),
            new Instruction(
                Opcodes.SUB,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter()),
            new Instruction(
                Opcodes.SBC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter()),
            new Instruction(
                Opcodes.SBC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter()),
            new Instruction(
                Opcodes.SBC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter()),
            new Instruction(
                Opcodes.SBC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter()),
            new Instruction(
                Opcodes.SBC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter()),
            new Instruction(
                Opcodes.SBC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter()),
            new Instruction(
                Opcodes.SBC,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.HL,
                    true),
                new Parameter()),
            new Instruction(
                Opcodes.SBC,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter()),
            #endregion
            #region 0xA0 - 0xAF
            new Instruction(
                Opcodes.AND,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter()),
            new Instruction(
                Opcodes.AND,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter()),
            new Instruction(
                Opcodes.AND,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter()),
            new Instruction(
                Opcodes.AND,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter()),
            new Instruction(
                Opcodes.AND,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter()),
            new Instruction(
                Opcodes.AND,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter()),
            new Instruction(
                Opcodes.AND,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.HL,
                    true),
                new Parameter()),
            new Instruction(
                Opcodes.AND,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter()),
            new Instruction(
                Opcodes.XOR,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter()),
            new Instruction(
                Opcodes.XOR,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter()),
            new Instruction(
                Opcodes.XOR,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter()),
            new Instruction(
                Opcodes.XOR,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter()),
            new Instruction(
                Opcodes.XOR,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter()),
            new Instruction(
                Opcodes.XOR,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter()),
            new Instruction(
                Opcodes.XOR,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.HL,
                    true),
                new Parameter()),
            new Instruction(
                Opcodes.XOR,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter()),
            #endregion
            #region 0xB0 - 0xBF
            new Instruction(
                Opcodes.OR,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter()),
            new Instruction(
                Opcodes.OR,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter()),
            new Instruction(
                Opcodes.OR,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter()),
            new Instruction(
                Opcodes.OR,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter()),
            new Instruction(
                Opcodes.OR,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter()),
            new Instruction(
                Opcodes.OR,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter()),
            new Instruction(
                Opcodes.OR,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.HL,
                    true),
                new Parameter()),
            new Instruction(
                Opcodes.OR,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter()),
            new Instruction(
                Opcodes.CP,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.B),
                new Parameter()),
            new Instruction(
                Opcodes.CP,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.C),
                new Parameter()),
            new Instruction(
                Opcodes.CP,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.D),
                new Parameter()),
            new Instruction(
                Opcodes.CP,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.E),
                new Parameter()),
            new Instruction(
                Opcodes.CP,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.H),
                new Parameter()),
            new Instruction(
                Opcodes.CP,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.L),
                new Parameter()),
            new Instruction(
                Opcodes.CP,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.HL,
                    true),
                new Parameter()),
            new Instruction(
                Opcodes.CP,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.A),
                new Parameter()),
            #endregion
            #region 0xC0 - 0xCF
            new Instruction(
                Opcodes.RET,
                Conditions.NOTZERO,
                1,
                8,
                20,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.POP,
                Conditions.NONE,
                1,
                12,
                12,
                true,
                new Parameter(
                    Registers.BC),
                new Parameter()),
            new Instruction(
                Opcodes.JP,
                Conditions.NOTZERO,
                3,
                12,
                16,
                false,
                new Parameter(
                    Registers.u16),
                new Parameter()),
            new Instruction(
                Opcodes.JP,
                Conditions.NONE,
                3,
                12,
                16,
                false,
                new Parameter(
                    Registers.u16),
                new Parameter()),
            new Instruction(
                Opcodes.CALL,
                Conditions.NOTZERO,
                3,
                12,
                24,
                false,
                new Parameter(
                    Registers.PC),
                new Parameter()),
            new Instruction(
                Opcodes.PUSH,
                Conditions.NONE,
                1,
                16,
                16,
                true,
                new Parameter(
                    Registers.BC),
                new Parameter()),
            new Instruction(
                Opcodes.ADD,
                Conditions.NONE,
                2,
                8,
                8,
                false,
                new Parameter(
                    Registers.u8),
                new Parameter()),
            new Instruction(
                Opcodes.RST,
                Conditions.NONE,
                1,
                16,
                16,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.RET,
                Conditions.ZERO,
                1,
                8,
                20,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.RET,
                Conditions.NONE,
                1,
                16,
                16,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.JP,
                Conditions.ZERO,
                3,
                12,
                16,
                false,
                new Parameter(
                    Registers.u16),
                new Parameter()),
            new Instruction(
                Opcodes.PREFIX,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.CALL,
                Conditions.ZERO,
                3,
                12,
                24,
                false,
                new Parameter(
                    Registers.PC),
                new Parameter()),
            new Instruction(
                Opcodes.CALL,
                Conditions.NONE,
                1,
                24,
                24,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.ADC,
                Conditions.NONE,
                2,
                8,
                8,
                false,
                new Parameter(
                    Registers.u8),
                new Parameter()),
            new Instruction(
                Opcodes.RST,
                Conditions.NONE,
                1,
                16,
                16,
                false,
                new Parameter(
                    Registers.u8,
                    false,
                    0x08),
                new Parameter()),
            #endregion
            #region 0xD0 - 0xDF
            new Instruction(
                Opcodes.RET,
                Conditions.NOCARRY,
                1,
                8,
                20,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.POP,
                Conditions.NONE,
                1,
                12,
                12,
                true,
                new Parameter(
                    Registers.DE),
                new Parameter()),
            new Instruction(
                Opcodes.JP,
                Conditions.NOCARRY,
                3,
                12,
                16,
                false,
                new Parameter(
                    Registers.u16),
                new Parameter()),
            new Instruction(),
            new Instruction(
                Opcodes.CALL,
                Conditions.NOCARRY,
                3,
                12,
                24,
                false,
                new Parameter(
                    Registers.PC),
                new Parameter()),
            new Instruction(
                Opcodes.PUSH,
                Conditions.NONE,
                1,
                16,
                16,
                true,
                new Parameter(
                    Registers.DE),
                new Parameter()),
            new Instruction(
                Opcodes.SUB,
                Conditions.NONE,
                2,
                8,
                8,
                false,
                new Parameter(
                    Registers.u8),
                new Parameter()),
            new Instruction(
                Opcodes.RST,
                Conditions.NONE,
                1,
                16,
                16,
                false,
                new Parameter(
                    Registers.u8,
                    false,
                    0x10),
                new Parameter()),
            new Instruction(
                Opcodes.RET,
                Conditions.CARRY,
                1,
                8,
                20,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.RETI,
                Conditions.NONE,
                1,
                16,
                16,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(
                Opcodes.JP,
                Conditions.CARRY,
                3,
                12,
                16,
                false,
                new Parameter(
                    Registers.u16),
                new Parameter()),
            new Instruction(),
            new Instruction(
                Opcodes.CALL,
                Conditions.CARRY,
                3,
                12,
                24,
                false,
                new Parameter(
                    Registers.PC),
                new Parameter()),
            new Instruction(),
            new Instruction(
                Opcodes.SBC,
                Conditions.NONE,
                2,
                8,
                8,
                false,
                new Parameter(
                    Registers.u8),
                new Parameter()),
            new Instruction(
                Opcodes.RST,
                Conditions.NONE,
                1,
                16,
                16,
                false,
                new Parameter(
                    Registers.u8,
                    false,
                    0x18),
                new Parameter()),
            #endregion
            #region 0xE0 - 0xEF
            new Instruction(
                Opcodes.LDH,
                Conditions.NONE,
                2,
                12,
                12,
                false,
                new Parameter(
                    Registers.u8,
                    true),
                new Parameter(
                    Registers.A)),
            new Instruction(
                Opcodes.POP,
                Conditions.NONE,
                1,
                12,
                12,
                true,
                new Parameter(
                    Registers.HL),
                new Parameter()),
            new Instruction(
                Opcodes.LDH,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.C,
                    true),
                new Parameter(
                    Registers.A)),
            new Instruction(),
            new Instruction(),
            new Instruction(
                Opcodes.PUSH,
                Conditions.NONE,
                1,
                16,
                16,
                true,
                new Parameter(
                    Registers.HL),
                new Parameter()),
            new Instruction(
                Opcodes.AND,
                Conditions.NONE,
                2,
                8,
                8,
                false,
                new Parameter(
                    Registers.u8),
                new Parameter()),
            new Instruction(
                Opcodes.RST,
                Conditions.NONE,
                1,
                16,
                16,
                false,
                new Parameter(
                    Registers.u8,
                    false,
                    0x20),
                new Parameter()),
            new Instruction(
                Opcodes.ADD,
                Conditions.ZERO,
                2,
                16,
                16,
                true,
                new Parameter(
                    Registers.SP),
                new Parameter(
                    Registers.u8)),
            new Instruction(
                Opcodes.JP,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(
                    Registers.HL),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.ZERO,
                3,
                16,
                16,
                false,
                new Parameter(
                    Registers.u16,
                    true),
                new Parameter(
                    Registers.A)),
            new Instruction(),
            new Instruction(),
            new Instruction(),
            new Instruction(
                Opcodes.XOR,
                Conditions.NONE,
                2,
                8,
                8,
                false,
                new Parameter(
                    Registers.u8),
                new Parameter()),
            new Instruction(
                Opcodes.RST,
                Conditions.NONE,
                1,
                16,
                16,
                false,
                new Parameter(
                    Registers.u8,
                    false,
                    0x28),
                new Parameter()),
            #endregion
            #region 0xF0 - 0xFF
            new Instruction(
                Opcodes.LDH,
                Conditions.NONE,
                2,
                12,
                12,
                false,
                new Parameter(
                    Registers.A),
                new Parameter(
                    Registers.u8,
                    true)),
            new Instruction(
                Opcodes.POP,
                Conditions.NONE,
                1,
                12,
                12,
                true,
                new Parameter(
                    Registers.AF),
                new Parameter()),
            new Instruction(
                Opcodes.LDH,
                Conditions.NONE,
                1,
                8,
                8,
                false,
                new Parameter(
                    Registers.A),
                new Parameter(
                    Registers.C,
                    true)),
            new Instruction(
                Opcodes.DI,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(),
            new Instruction(
                Opcodes.PUSH,
                Conditions.NONE,
                1,
                16,
                16,
                true,
                new Parameter(
                    Registers.AF),
                new Parameter()),
            new Instruction(
                Opcodes.OR,
                Conditions.NONE,
                2,
                8,
                8,
                false,
                new Parameter(
                    Registers.u8),
                new Parameter()),
            new Instruction(
                Opcodes.RST,
                Conditions.NONE,
                1,
                16,
                16,
                false,
                new Parameter(
                    Registers.u8,
                    false,
                    0x30),
                new Parameter()),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                2,
                12,
                12,
                true,
                new Parameter(
                    Registers.HL),
                new Parameter(
                    Registers.SP)),
            new Instruction(
                Opcodes.LD,
                Conditions.NONE,
                1,
                8,
                8,
                true,
                new Parameter(
                    Registers.SP),
                new Parameter(
                    Registers.HL)),
            new Instruction(
                Opcodes.LD,
                Conditions.ZERO,
                3,
                16,
                16,
                false,
                new Parameter(
                    Registers.A),
                new Parameter(
                    Registers.u16,
                    true)),
            new Instruction(
                Opcodes.EI,
                Conditions.NONE,
                1,
                4,
                4,
                false,
                new Parameter(),
                new Parameter()),
            new Instruction(),
            new Instruction(),
            new Instruction(
                Opcodes.CP,
                Conditions.NONE,
                2,
                8,
                8,
                false,
                new Parameter(
                    Registers.u8),
                new Parameter()),
            new Instruction(
                Opcodes.RST,
                Conditions.NONE,
                1,
                16,
                16,
                false,
                new Parameter(
                    Registers.u8,
                    false,
                    0x38),
                new Parameter()),
            #endregion
        };

        public static readonly List<Instruction> ExtendedSet = new List<Instruction>(256)
        {

        };
    }
}
