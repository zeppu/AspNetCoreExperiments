namespace Glyde.Core.Requests
{
    public interface IRequestContext
    {
        TData Get<TData>() where TData : class, IRequestData;
    }
}