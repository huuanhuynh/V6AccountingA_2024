﻿using System.Collections.Generic;

namespace V6ThuePostBkavApi
{
    public class WebApiResponse<T>
    {
        public List<T> Value { get; set; }
    }
}