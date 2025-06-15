using System;

namespace Fuzzy
{
    class Functie
    {
        public static void Calc(ref NumarFuzzy N)
        {
            float x1 = N.C - N.A, x2 = N.C, x3 = N.C + N.B, y1 = 0, y2 = 1, y3 = 0;
            float a1 = 1 / (x2 - x1) * (y2 - y1);
            float b1 = y2 - x2 * a1;
            float a2 = 1 / (x3 - x2) * (y3 - y2);
            float b2 = y3 - x3 * a2;

            for (int i = 0; i < N.vect.Length; ++i)
            {
                if (i >= N.C - N.A && i < N.C + N.B)
                    if (i < N.C)
                    {
                        // x1 = c - a; x2 = c; y1 = pb.Height - offset; y2 = offset;
                        N.vect[i] = a1 * i + b1;
                    }
                    else
                    {
                        // x1 = c; x2 = c + b; y1 = offset; y2 = pb.Height - offset;
                        N.vect[i] = a2 * i + b2;
                    }
                else
                    N.vect[i] = 0;
            }
        }
    }
}
