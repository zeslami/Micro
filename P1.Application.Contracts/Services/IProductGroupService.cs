using P1.Application.Contracts.DTOs;
using P1.Application.Contracts.Services.Base;

namespace P1.Application.Contracts.Services;

public interface IProductGroupService : IGenericService<int, ProductGroupDto, CreateProductGroupDto, UpdateProductGroupDto>
{
}
