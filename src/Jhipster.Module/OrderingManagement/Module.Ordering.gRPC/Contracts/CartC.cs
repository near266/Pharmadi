using System.Runtime.Serialization;

namespace Module.Ordering.gRPC.Contracts
{
    [DataContract]
    public class CartAddRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public Guid UserId { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Guid ProductId { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public int Quantity { get; set; }
    }

    [DataContract]
    public class CartUpdateRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public Guid UserId { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Guid ProductId { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public int Quantity { get; set; }
    }

    [DataContract]
    public class CartDeleteRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
    }


    [DataContract]
    public class CartBaseResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int message { get; set; }
    }

    [DataContract]
    public class CartInfor
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public ProductC Product { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public int Quantity { get; set; }
    }

    [DataContract]
    public class CartGetAllByUserRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int page { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public int pageSize { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Guid userId { get; set; }
    }


}
