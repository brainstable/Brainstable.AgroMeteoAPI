namespace Brainstable.AgroMeteoAPI.Shared.RequestParameters;

public class PagedList<T> : List<T>
{
    public MetaData MetaData { get; set; }

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        MetaData = new MetaData
        {
            TotalCount = count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = pageSize == 0 ? 0 : (int)Math.Ceiling(count / (double)pageSize)
        };

        AddRange(items);
    }

    public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();

        if (pageSize == 0)
        {
            return new PagedList<T>(source.ToList(), count, pageNumber, pageSize);
        }

        var items = source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToList();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}