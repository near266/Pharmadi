using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Jhipster.Dto;

using Module.Permission.Core.Entities;
using Module.Permission.Application.Dtos;
using Module.Permission.Application.Commands;
using Module.Catalog.Application.Commands.CategoryCm;
using Module.Catalog.gRPC.Contracts;
using Module.Catalog.Domain.Entities;
using BFF.Web.DTOs;
using Jhipster.gRPC.Contracts.Shared.Identity;
using Module.Factor.gRPC.Contracts;
using Module.Factor.Application.Commands.MerchantCm;
using Module.Factor.Domain.Entities;
using Module.Catalog.Application.Commands.ProductCm;
using Module.Catalog.Application.Queries.ProductQ;
using Module.Catalog.gRPC.Contracts.PagedListC;
using Module.Catalog.Shared.Utilities;
using Module.Catalog.Application.Queries.CategoryQ;
using Module.Catalog.Application.Commands.BrandCm;
using Module.Catalog.Application.Queries.BrandQ;
using Module.Catalog.Application.Commands.GroupBrandCm;
using Module.Catalog.Application.Queries.GroupBrandQ;
using Module.Catalog.Application.Commands.LabelCm;
using Module.Catalog.Application.Queries.LabelQ;
using Module.Catalog.Application.Commands.TagCm;
using Module.Catalog.Application.Queries.TagQ;

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
            CreateMap<RegisterByUserDTO, MerchantAddRequest>();
            CreateMap<RegisterByAdminDTO, RegisterAdminRequest>();
            CreateMap<RegisterByAdminDTO, MerchantAddRequest>();

            CreateMap<RegisterRequest, User>();
            CreateMap<RegisterAdminRequest, User>();

            //merchant
            CreateMap<MerchantAddRequest, MerchantAddCommand>();
            CreateMap<MerchantAddCommand, Merchant>();
            CreateMap<Merchant, Merchant>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            #region 2.Module permission
            CreateMap<Function, FunctionDTO>().ReverseMap();
            CreateMap<FunctionTypeDTO, FunctionType>().ReverseMap();
            CreateMap<Function, Functiondto>().ReverseMap();
            CreateMap<FunctionType, FunctionTypedto>().ReverseMap();
            CreateMap<Module.Permission.Core.Entities.Role, RoleDTO>().ReverseMap();
            CreateMap<RoleFunction, RoleFunctionDTO>().ReverseMap();
            CreateMap<RoleFunction, RoleFunctiondto>().ReverseMap();
            CreateMap<Function, FunctionAddCommand>().ReverseMap();
            CreateMap<Function, FunctionUpdateCommand>().ReverseMap();
            CreateMap<FunctionType, FunctionTypeAddCommand>().ReverseMap();
            CreateMap<FunctionType, FunctionTypeUpdateCommand>().ReverseMap();
            CreateMap<Module.Permission.Core.Entities.Role, RoleAddCommand>().ReverseMap();
            CreateMap<Module.Permission.Core.Entities.Role, RoleUpdateCommand>().ReverseMap();
            #endregion

            #region 3.CatalogSvc

            //Category
            CreateMap<CategoryAddRequest, CategoryAddCommand>();
            CreateMap<CategoryAddCommand, Category>();
            CreateMap<Category,Category>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CategoryUpdateRequest, CategoryUpdateCommand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CategoryUpdateCommand, Category>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CategoryDeleteRequest, CategoryDeleteCommand>();

            CreateMap<CategorySearchRequest, CategorySearchQuery>();
            CreateMap<Category, CategorySearchResponse>();

            CreateMap<CategoryGetAllAdminRequest, CategoryGetAllAdminQuery>();
            CreateMap<Category, CategoryGetAllAdminResponse>();
            CreateMap<PagedList<Category>, PagedListC<CategoryGetAllAdminResponse>>();

            CreateMap<GetListCataloryRequest, GetListCategotyQuery>().ReverseMap();


            //Brand
            CreateMap<BrandAddRequest, BrandAddCommand>();
            CreateMap<BrandAddCommand, Brand>();
            CreateMap<Brand, Brand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<BrandUpdateRequest, BrandUpdateCommand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<BrandUpdateCommand, Brand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<BrandDeleteRequest, BrandDeleteCommand>();

            CreateMap<BrandSearchRequest, BrandSearchQuery>();
            CreateMap<Brand, BrandSearchResponse>();

            CreateMap<BrandGetAllAdminRequest, BrandGetAllAdminQuery>();
            CreateMap<Brand, BrandGetAllAdminResponse>();
            CreateMap<PagedList<Brand>, PagedListC<BrandGetAllAdminResponse>>();


            //GroupBrand
            CreateMap<GroupBrandAddRequest, GroupBrandAddCommand>();
            CreateMap<GroupBrandAddCommand, GroupBrand>();
            CreateMap<GroupBrand, GroupBrand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<GroupBrandUpdateRequest, GroupBrandUpdateCommand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<GroupBrandUpdateCommand, GroupBrand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<GroupBrandDeleteRequest, GroupBrandDeleteCommand>();

            CreateMap<GroupBrandSearchRequest, GroupBrandSearchQuery>();
            CreateMap<GroupBrand, GroupBrandSearchResponse>();

            CreateMap<GroupBrandGetAllAdminRequest, GroupBrandGetAllAdminQuery>();
            CreateMap<GroupBrand, GroupBrandGetAllAdminResponse>();
            CreateMap<PagedList<GroupBrand>, PagedListC<GroupBrandGetAllAdminResponse>>();

            //Label
            CreateMap<LabelAddRequest, LabelAddCommand>();
            CreateMap<LabelAddCommand, Label>();
            CreateMap<Label, Label>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<LabelUpdateRequest, LabelUpdateCommand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<LabelUpdateCommand, Label>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<LabelDeleteRequest, LabelDeleteCommand>();

            CreateMap<LabelSearchRequest, LabelSearchQuery>();
            CreateMap<Label, LabelSearchResponse>();

            CreateMap<LabelGetAllAdminRequest, LabelGetAllAdminQuery>();
            CreateMap<Label, LabelGetAllAdminResponse>();
            CreateMap<PagedList<Label>, PagedListC<LabelGetAllAdminResponse>>();

            //Tag
            CreateMap<TagAddRequest, TagAddCommand>();
            CreateMap<TagAddCommand, Tag>();
            CreateMap<Tag, Tag>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<TagUpdateRequest, TagUpdateCommand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<TagUpdateCommand, Tag>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<TagDeleteRequest, TagDeleteCommand>();

            CreateMap<TagSearchRequest, TagSearchQuery>();
            CreateMap<Tag, TagSearchResponse>();

            CreateMap<TagGetAllAdminRequest, TagGetAllAdminQuery>();
            CreateMap<Tag, TagGetAllAdminResponse>();
            CreateMap<PagedList<Tag>, PagedListC<TagGetAllAdminResponse>>();

            //product
            CreateMap<ProductAddRequest, ProductAddCommand>();
            CreateMap<ProductAddCommand, Product>();

            CreateMap<ProductUpdateRequest, ProductUpdateCommand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ProductUpdateCommand, Product>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ProductDeleteRequest, ProductDeleteCommand>();


            CreateMap<ProductSearchRequest, SearchProductQuery>();
            CreateMap<Product, ProductInforSearchResponse>();
            CreateMap<PagedList<Product>, PagedListC<ProductInforSearchResponse>>();

            CreateMap<ProductGetAllAdminRequest, ProductGetAllAdminQuery>();
            CreateMap<Product, ProductGetAllAdminResponse>();
            CreateMap<PagedList<Product>, PagedListC<ProductGetAllAdminResponse>>();

            CreateMap<ProductViewDetailRequest, ProductViewDetailQuery>();
            CreateMap<Product, ProductViewDetailResponse>();

            CreateMap<ProductSearchListRequest, ViewProductForUQuery>();
            CreateMap<ProductSearchListRequest, ViewProductBestSaleQuery>();
            CreateMap<ProductSearchListRequest, ViewProductNewQuery>();
            CreateMap<ProductSearchListRequest, ViewProductPromotionQuery>();
            CreateMap<Product, ProductInforSearchResponse>();

            CreateMap<Product, Product>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion
        }
    }
}
