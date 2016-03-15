﻿using System.IO;
using System.Net.Http;

namespace NQuandl.Domain.Responses
{
    public abstract class ResponseWithRawHttpContent
    {
        public RawHttpContent RawHttpContent { get; set; }
    }

    public class RawHttpContent
    {
        public string StatusCode { get; set; }
        public bool IsStatusSuccessCode { get; set; }
        public Stream Content { get; set; }
    }
}