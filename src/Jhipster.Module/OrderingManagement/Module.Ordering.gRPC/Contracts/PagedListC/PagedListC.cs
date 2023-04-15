using System.Runtime.Serialization;

namespace Module.Ordering.gRPC.Contracts.PagedListC
{
    [DataContract]
    public class PagedListC<T> where T : class
    {
        public PagedListC()
        {
            Data = new List<T>();
        }

        public PagedListC(IEnumerable<T> _Data, int _TotalCount)
        {
            Data = _Data;
            TotalCount = _TotalCount;
        }
        [DataMember(Order = 1)]
        public IEnumerable<T> Data { get; set; }
        [DataMember(Order = 2)]
        public int TotalCount { get; set; }
    }
}
