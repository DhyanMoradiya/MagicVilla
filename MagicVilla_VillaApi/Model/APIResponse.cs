﻿using System.Net;

namespace MagicVilla_VillaApi.Model
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages {  get; set; } = new List<string>();
        public object Result { get; set; }
    }
}
