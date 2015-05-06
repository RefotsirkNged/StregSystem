﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem___EksamensOpgave
{
    class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException()
        {
        }

        public InsufficientCreditsException(string message)
            : base(message)
        {
        }

        public InsufficientCreditsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
