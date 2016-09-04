namespace DelegatesTest.RequestContext
{
    public interface IRequestContext
    {
        TData Get<TData>() where TData : class, IRequestData;
    }
}