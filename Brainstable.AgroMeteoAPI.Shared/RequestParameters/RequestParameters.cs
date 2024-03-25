﻿namespace Brainstable.AgroMeteoAPI.Shared.RequestParameters
{
    public abstract class RequestParameters
    {
        private const int MAX_PAGE_SIZE = 50;

        private int pageSize = MAX_PAGE_SIZE;
        public int PageNumber { get; set; } = 1;

        public int PageSize 
        {
            get => pageSize;
            set => pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
        }

        public RequestParameters()
        {
            pageSize = 0;
        }
    }
}
