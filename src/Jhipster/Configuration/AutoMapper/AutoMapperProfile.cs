using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Jhipster.Dto;
using Module.Catalog.Application.Commands.CategoryCm;
using Module.Catalog.Domain.Entities;
using BFF.Web.DTOs;
using Jhipster.gRPC.Contracts.Shared.Identity;
using Module.Factor.Application.Commands.MerchantCm;
using Module.Factor.Domain.Entities;
using Module.Catalog.Application.Commands.ProductCm;
using Module.Catalog.Application.Commands.BrandCm;
using Module.Catalog.Application.Commands.GroupBrandCm;
using Module.Catalog.Application.Commands.LabelCm;
using Module.Catalog.Application.Commands.TagCm;
using Module.Catalog.Application.Queries.ProductQ;
using BFF.Web.DTOs.CatalogSvc;
using Org.BouncyCastle.Asn1.X509;
using System.Collections.Generic;
using Module.Catalog.Application.Commands.WarehouseCm;
using Module.Catalog.Application.Queries.CategoryQ;
using Module.Ordering.Application.Commands.CartCm;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Application.Commands.OrderItemCm;
using Module.Ordering.Application.Commands.PurchaseOrderCm;

namespace Jhipster.Configuration.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, User>().ReverseMap();

            CreateMap<User, UserDto>()
                .ForMember(userDto => userDto.Roles, opt => opt.MapFrom(user => user.UserRoles.Select(iur => iur.Role.Name).ToHashSet()))
            .ReverseMap()
                .ForPath(user => user.UserRoles, opt => opt.MapFrom(userDto => userDto.Roles.Select(role => new UserRole { Role = new Domain.Role { Name = role }, UserId = userDto.Id }).ToHashSet()));

            CreateMap<User, RegisterAdminResponse>();
            CreateMap<User, RegisterResponse>();
                
            CreateMap<RegisterByUserDTO, RegisterRequest>();
            CreateMap<RegisterByUserDTO, Merchant>();
            CreateMap<RegisterByAdminDTO, RegisterAdminRequest>();
            CreateMap<RegisterByAdminDTO, Merchant>();

            CreateMap<User,RegisterRequest>()
             .ForMember(userDto => userDto.Roles, opt => opt.MapFrom(user => user.UserRoles.Select(iur => iur.Role.Name).ToHashSet()))
            .ReverseMap()
                .ForPath(user => user.UserRoles, opt => opt.MapFrom(userDto => userDto.Roles.Select(role => new UserRole { Role = new Domain.Role { Name = role }, UserId = userDto.Id }).ToHashSet()));
            CreateMap<User,RegisterAdminRequest>()
            .ForMember(userDto => userDto.Roles, opt => opt.MapFrom(user => user.UserRoles.Select(iur => iur.Role.Name).ToHashSet()))
            .ReverseMap()
                .ForPath(user => user.UserRoles, opt => opt.MapFrom(userDto => userDto.Roles.Select(role => new UserRole { Role = new Domain.Role { Name = role }, UserId = userDto.Id }).ToHashSet()));

            //merchant
            CreateMap<Merchant, MerchantAddCommand>();
            CreateMap<MerchantAddCommand, Merchant>();
            CreateMap<Merchant, Merchant>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #region 2.Module permission
            //CreateMap<Function, FunctionDTO>().ReverseMap();
            //CreateMap<FunctionTypeDTO, FunctionType>().ReverseMap();
            //CreateMap<Function, Functiondto>().ReverseMap();
            //CreateMap<FunctionType, FunctionTypedto>().ReverseMap();
            //CreateMap<Module.Permission.Core.Entities.Role, RoleDTO>().ReverseMap();
            //CreateMap<RoleFunction, RoleFunctionDTO>().ReverseMap();
            //CreateMap<RoleFunction, RoleFunctiondto>().ReverseMap();
            //CreateMap<Function, FunctionAddCommand>().ReverseMap();
            //CreateMap<Function, FunctionUpdateCommand>().ReverseMap();
            //CreateMap<FunctionType, FunctionTypeAddCommand>().ReverseMap();
            //CreateMap<FunctionType, FunctionTypeUpdateCommand>().ReverseMap();
            //CreateMap<Module.Permission.Core.Entities.Role, RoleAddCommand>().ReverseMap();
            //CreateMap<Module.Permission.Core.Entities.Role, RoleUpdateCommand>().ReverseMap();
            #endregion

            #region 3.CatalogSvc

            //Category
            CreateMap<CategoryAddCommand, Category>();
            CreateMap<Category,Category>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CategoryUpdateCommand, Category>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CategoryProductAddCommand, CategoryProduct>();
            CreateMap<CategoryProduct, CategoryProduct>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CategoryProductUpdateCommand, CategoryProduct>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ViewListCategoryLv1Query, Category>();
            CreateMap<ViewListCategoryLv2Query,Category>();

            //Brand
            CreateMap<BrandAddCommand, Brand>();
            CreateMap<Brand, Brand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<BrandUpdateCommand, Brand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<BrandPinCommand,Brand>();

            //GroupBrand
            CreateMap<GroupBrandAddCommand, GroupBrand>();
            CreateMap<GroupBrand, GroupBrand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<GroupBrandUpdateCommand, GroupBrand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PinGroupBrandCommand,GroupBrand>();

            //Label
            CreateMap<LabelAddCommand, Label>();
            CreateMap<Label, Label>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<LabelUpdateCommand, Label>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<LabelProductAddCommand, LabelProduct>();
            CreateMap<LabelProduct, LabelProduct>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<LabelProductUpdateCommand, LabelProduct>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            //Tag
            CreateMap<TagAddCommand, Tag>();
            CreateMap<Tag, Tag>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<TagUpdateCommand, Tag>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<TagProductAddCommand, TagProduct>();
            CreateMap<TagProduct, TagProduct>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<TagProductUpdateCommand, TagProduct>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            //product
            CreateMap<ProductAddRequest, ProductAddCommand>();
            CreateMap<ProductAddCommand, Product>();
            CreateMap<ProductUpdateCommand, Product>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Product, Product>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<ProductListDTO, Product>().ReverseMap();
            CreateMap<ViewProductWithBrandQuery, Product>();
            CreateMap<UpdateStatusProductCommand, Product>();


            CreateMap<ViewProductSimilarQuery, Product>();
            CreateMap<GetAllAminDTO,Product>().ReverseMap();
            //warehouse
            CreateMap<WarehouseProductAddCommand, WarehouseProduct>();
            CreateMap<WarehouseProduct, WarehouseProduct>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<WarehouseProductUpdateCommand, WarehouseProduct>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            //CreateMap<ProductSearchRequest, SearchProductQuery>();
            //CreateMap<Product, ProductInforSearchResponse>();
            //CreateMap<PagedList<Product>, PagedListC<ProductInforSearchResponse>>();

            //CreateMap<ProductGetAllAdminRequest, ProductGetAllAdminQuery>();
            //CreateMap<Product, ProductGetAllAdminResponse>();
            //CreateMap<PagedList<Product>, PagedListC<ProductGetAllAdminResponse>>();

            //CreateMap<ProductViewDetailRequest, ProductViewDetailQuery>();
            //CreateMap<Product, ProductViewDetailResponse>();

            //CreateMap<ProductSearchListRequest, ViewProductForUQuery>();
            //CreateMap<ProductSearchListRequest, ViewProductBestSaleQuery>();
            //CreateMap<ProductSearchListRequest, ViewProductNewQuery>();
            //CreateMap<ProductSearchListRequest, ViewProductPromotionQuery>();
            //CreateMap<Product, ProductInforSearchResponse>();

            CreateMap<Product, Product>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #endregion
            #region OrderSvc
            CreateMap<CartAddCommand,Cart>().ReverseMap();
            CreateMap<CartUpdateCommand,Cart>().ReverseMap();
            CreateMap<OrderItemAddCommand, OrderItem>().ReverseMap();
            CreateMap<OrderItemUpdateCommand, OrderItem>().ReverseMap();
            CreateMap<PurchaseOrderAddCommand, PurchaseOrder>().ReverseMap();
            CreateMap<PurchaseOrderUpdateCommand, PurchaseOrder>().ReverseMap();
            CreateMap<PurchaseOrderAddCommand, PurchaseOrder>().ReverseMap();
            #endregion
        }
    }
}
