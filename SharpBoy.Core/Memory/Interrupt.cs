using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy.Core
{
    internal class Interrupt
    {
        private bool _MasterEnable = false;
        private InterruptEnableStatus _MasterEnableStatus = InterruptEnableStatus.DISABLED;
        private byte _EnabledInterrupts = 0;
        private byte _RequestedInterrupts = 0;

        private readonly byte[] _InterruptAddresses = new byte[5]
        {
            0x40,
            0x48,
            0x50,
            0x58,
            0x60
        };

        public Interrupt() { }

        //TODO: Interrupt enabling should be delayed by one cycle
        public void EnableInterrupts() => _MasterEnableStatus = InterruptEnableStatus.REQUESTED;

        public void DisableInterrupts()
        {
            _MasterEnableStatus = InterruptEnableStatus.DISABLED;
            _MasterEnable = false;
        }

        public bool InterruptPending()
        {
            if (_MasterEnableStatus == InterruptEnableStatus.REQUESTED)
                _MasterEnableStatus = InterruptEnableStatus.PROCESSING;
            else if (_MasterEnableStatus == InterruptEnableStatus.PROCESSING)
            {
                _MasterEnableStatus = InterruptEnableStatus.ENABLED;
                _MasterEnable = true;
            }
            return _MasterEnable && (_RequestedInterrupts & _EnabledInterrupts) != 0;
        }

        public byte GetInterruptAddress()
        {
            for (int i = 0; i < _InterruptAddresses.Length; i++)
                if (((_RequestedInterrupts & (0x1 << i)) & (_EnabledInterrupts & (0x1 << i))) != 0)
                {
                    _RequestedInterrupts = (byte)(_RequestedInterrupts & ~(0x1 << i));
                    return _InterruptAddresses[i];
                }
            return 0;
        }
    }

    internal enum InterruptEnableStatus
    {
        DISABLED,
        REQUESTED,
        PROCESSING,
        ENABLED
    }
}
