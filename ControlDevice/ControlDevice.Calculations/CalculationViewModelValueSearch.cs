using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDevice.Calculations
{
    partial class CalculationViewModel
    {
        public float ValueFromRange(float inboundCurrentFromControl)
        {
            float _lowValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueY).FirstOrDefault();

            float _highValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueY).FirstOrDefault();

            float _lowValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueX).FirstOrDefault();

            float _highValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueX).FirstOrDefault();

            _angleValueK = (_highValueY - _lowValueY) / (_highValueX - _lowValueX);

            return _angleValueK;
        }

        public float ValueFromRangeReversed(float inboundCurrentFromControl)
        {
            float _lowValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueY).FirstOrDefault();

            float _highValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueY).FirstOrDefault();

            float _lowValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueX).FirstOrDefault();

            float _highValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueX).FirstOrDefault();

            _angleValueK = (_highValueX - _lowValueX) / (_highValueY - _lowValueY);

            return _angleValueK;
        }

        public float ShiftAmountB(float inboundCurrentFromControl)
        {
            float _lowValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueY).FirstOrDefault();

            float _highValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueY).FirstOrDefault();

            float _lowValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueX).FirstOrDefault();

            float _highValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueX).FirstOrDefault();

            _shiftAmountB = _lowValueY - _angleValueK * _lowValueX;

            return _shiftAmountB;
        }

        public float ShiftAmountBReversed(float inboundCurrentFromControl)
        {
            float _lowValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueY).FirstOrDefault();

            float _highValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueY).FirstOrDefault();

            float _lowValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueX).FirstOrDefault();

            float _highValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueX).FirstOrDefault();

            _shiftAmountB = _lowValueX - _angleValueK * _lowValueY;

            return _shiftAmountB;
        }

        public float ValueFromRangeDAC(float inboundCurrentFromControl)
        {
            float _lowValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueDAC).FirstOrDefault();

            float _highValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueDAC).FirstOrDefault();

            float _lowValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueX).FirstOrDefault();

            float _highValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueX).FirstOrDefault();

            _angleValueK = (_highValueY - _lowValueY) / (_highValueX - _lowValueX);

            return _angleValueK;
        }

        public float ValueFromRangeDACReversed(float inboundCurrentFromControl)
        {
            float _lowValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueDAC).FirstOrDefault();

            float _highValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueDAC).FirstOrDefault();

            float _lowValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueX).FirstOrDefault();

            float _highValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueX).FirstOrDefault();

            _angleValueK = (_highValueY - _lowValueY) / (_highValueX - _lowValueX);

            return _angleValueK;
        }

        public float ShiftAmountBDAC(float inboundCurrentFromControl)
        {
            float _lowValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueDAC).FirstOrDefault();

            float _highValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueDAC).FirstOrDefault();

            float _lowValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueX).FirstOrDefault();

            float _highValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueX).FirstOrDefault();

            _shiftAmountB = _lowValueY - _angleValueK * _lowValueX;

            return _shiftAmountB;
        }

        public float ShiftAmountBDACReversed(float inboundCurrentFromControl)
        {
            float _lowValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueDAC).FirstOrDefault();

            float _highValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueDAC).FirstOrDefault();

            float _lowValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueX).FirstOrDefault();

            float _highValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueX).FirstOrDefault();

            _shiftAmountB = _lowValueX - _angleValueK * _lowValueY;

            return _shiftAmountB;
        }

        public void VoltageAveraging()
        {
            int i = InternalQueue.Queue.Count - 10;

            if (i < 0)
            {
                i = 0;
            }

            for (; i < InternalQueue.Queue.Count; i++)
            {
                InboundVoltageAverage = InternalQueue.Queue.ElementAt(i) + InboundVoltageAverage;
            }

            InboundVoltageAverage = InboundVoltageAverage / 10;

            InboundVoltageAverage = Math.Round(InboundVoltageAverage, 4, MidpointRounding.ToEven);

        }
    }
}
