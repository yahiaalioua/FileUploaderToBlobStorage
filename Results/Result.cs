﻿using FileUploadApiAzureBlobStorage.DomainErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroSigma.Domain.Common.Results
{
    public abstract class Result<T>
    {
        public abstract ResultType ResultType { get; }
        public abstract CustomProblemDetails CustomProblemDetails { get; }
        public abstract T Data { get; }
        
    }
}
