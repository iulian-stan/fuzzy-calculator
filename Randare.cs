using System;

namespace Fuzzy
{
    class Randare
    {
        private float _hFactor, _wFactor, _offset;
        private int _wI, _hI;
        private int _min, _max;

        public int WindowH
        {
            get { return _hI; }
        }

        public int WindowW
        {
            get { return _wI; }
        }

        public float Offset
        {
            get { return _offset; }
        }

        public int Min
        {
            get { return _min; }
        }

        public int Max
        {
            get { return _max; }
        }

        public Randare(float widthI, float heightI, float widthRmin, float widthRmax, float heightR, float offset)
        {
            _wI = (int)Math.Round(widthI,MidpointRounding.AwayFromZero);
            _hI = (int)Math.Round(heightI,MidpointRounding.AwayFromZero);
            _min = (int)Math.Round(widthRmin,MidpointRounding.AwayFromZero);
            _max = (int)Math.Round(widthRmax, MidpointRounding.AwayFromZero);
            _wFactor = (widthI - 2 * offset) / (_max - _min);
            _hFactor = (heightI - 2 * offset) / heightR;
            _offset = offset;
        }

        public float RandeazaW(float x)
        {
            return (x - _min) * _wFactor + _offset;
        }

        public float RandeazaH(float y)
        {
            return (1 - y) * _hFactor + _offset;
        }

        public float WFactor
        {
            get { return _wFactor; }
        }
    }
}
