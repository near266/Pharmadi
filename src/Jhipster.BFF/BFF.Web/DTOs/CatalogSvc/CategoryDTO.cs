﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BFF.Web.DTOs.CatalogSvc
{
    //public class CategoryProductAddRequest
    //{
    //    [JsonIgnore]
    //    public Guid Id { get; set; }
    //    public Guid ProductId { get; set; }
    //    public Guid CategoryId { get; set; }
    //    public
    //}

    public class CategoryProductUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public bool Priority { get; set; }
    }

    public class CategoryProductDeleteRequest
    {
        public Guid ProductId { get; set; }
        public Guid Id { get; set; }
    }
    public class CategoryLv1 {
     public Guid Id { get; set; }
     public bool IsLeaf { get; set; }

    }
    public class PinCategoryLv1Request {
    public List<CategoryLv1> categoryLv1s { get; set; }
    
    }


}
