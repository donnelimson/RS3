using System;
using System.Collections.Generic;
using System.Text;

namespace darkstar.reporting
{
    public static class WellknownConstants
    {
        public const string DtlProtocolPrefix = "dtl://";
        public const string DtlDirectiveChar = "@";
        public const string DtlReferenceChar = "$";
        public const string DtlReferenceProtocolPrefix = DtlProtocolPrefix + DtlReferenceChar;
        public const string DtlDirectiveProtocolPrefix = DtlProtocolPrefix + DtlDirectiveChar;
    }
}
