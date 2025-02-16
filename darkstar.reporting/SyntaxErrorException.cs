﻿//Creation Time: 2010-08-20
using System;

namespace darkstar.reporting
{
    public class SyntaxErrorException : Exception
    {
        public SyntaxErrorException()
            : base()
        {
        }

        public SyntaxErrorException(string msg)
            : base(msg)
        {
        }

        public SyntaxErrorException(string msg, Exception ex)
            : base(msg, ex)
        {
        }
    }
}