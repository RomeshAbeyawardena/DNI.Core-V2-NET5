using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Constants
{
    public static class Characters
    {
        public const int A = 1;
        public const int B = 2;
        public const int C = 4;
        public const int D = 8;
        public const int E = 16;
        public const int F = 32;
        public const int G = 64;
        public const int H = 128;
        public const int I = 256;
        public const int J = 512;
        public const int K = 1024;
        public const int L = 2048;
        public const int M = 4096;
        public const int N = 8192;
        public const int O = 16384;
        public const int P = 32768;
        public const int Q = 65536;
        public const int R = 131072;
        public const int S = 262144;
        public const int T = 524288;
        public const int U = 1048576;
        public const int V = 2097152;
        public const int W = 4194304;
        public const int X = 8388608;
        public const int Y = 16777216;
        public const int Z = 33554432;

        public static IReadOnlyDictionary<char, int> Lookup => new Dictionary<char, int>(new []
        { 
            KeyValuePair.Create('A', A),
            KeyValuePair.Create('B', B),
            KeyValuePair.Create('C', C),
            KeyValuePair.Create('D', D),
            KeyValuePair.Create('E', E),
            KeyValuePair.Create('F', F),
            KeyValuePair.Create('G', G),
            KeyValuePair.Create('H', H),
            KeyValuePair.Create('I', I),
            KeyValuePair.Create('J', J),
            KeyValuePair.Create('K', K),
            KeyValuePair.Create('L', L),
            KeyValuePair.Create('M', M),
            KeyValuePair.Create('N', N),
            KeyValuePair.Create('O', O),
            KeyValuePair.Create('P', P),
            KeyValuePair.Create('Q', Q),
            KeyValuePair.Create('R', R),
            KeyValuePair.Create('S', S),
            KeyValuePair.Create('T', T),
            KeyValuePair.Create('U', U),
            KeyValuePair.Create('V', V),
            KeyValuePair.Create('W', W),
            KeyValuePair.Create('X', X),
            KeyValuePair.Create('Y', Y),
            KeyValuePair.Create('Z', Z),
        });
    }
}
