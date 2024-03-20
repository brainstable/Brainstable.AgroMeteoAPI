namespace Brainstable.AgroMeteoAPI.Shared.RequestParameters
{
    public abstract class RequestParameters
    {
        private const int MAX_PAGE_SIZE = 50;

        private int pageSize;
        public int PageNumber { get; set; } = 1;

        public int PageSize 
        {
            get => pageSize;
            set => pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
        }
    }
}
