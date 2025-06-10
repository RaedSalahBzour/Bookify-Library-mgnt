using Application.Categories.Dtos;
using Application.Categories.Services;
using AutoMapper;
using Data.Entities;
using Data.Interfaces;

namespace Service.Services;

public class CategoryService(IMapper mapper, IUnitOfWork unitOfWork) : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        List<Category> categories = _unitOfWork.CategoryRepository.GetCategories();
        List<CategoryDto> categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
        return categoryDtos;

    }
    public async Task<CategoryDto> GetByIdAsync(string id)
    {
        Category? category = await _unitOfWork.CategoryRepository.GetCategoryById(id);
        if (category == null)
            throw new KeyNotFoundException($"Category With Id {id} Was Not Found");
        CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);
        return categoryDto;
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto CreateCategoryDto)
    {
        Category? category = _mapper.Map<Category>(CreateCategoryDto);
        await _unitOfWork.CategoryRepository.AddAsync(category);
        await _unitOfWork.CategoryRepository.SaveChangesAsync();
        CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);
        return categoryDto;
    }
    public async Task<CategoryDto> UpdateCategoryAsync(string id, UpdateCategoryDto UpdateCategoryDto)
    {
        Category? category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        if (category is null)
            throw new KeyNotFoundException($"Category With Id {id} Was Not Found");
        _mapper.Map(UpdateCategoryDto, category);
        await _unitOfWork.CategoryRepository.Update(category);
        await _unitOfWork.CategoryRepository.SaveChangesAsync();
        CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);
        return categoryDto;
    }

    public async Task<CategoryDto> DeleteCategoryAsync(string id)
    {
        Category? category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        if (category is null)
            throw new KeyNotFoundException($"Category With Id {id} Was Not Found");
        await _unitOfWork.CategoryRepository.Delete(category);
        await _unitOfWork.CategoryRepository.SaveChangesAsync();
        CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);
        return categoryDto;
    }

}
