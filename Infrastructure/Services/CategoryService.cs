using Application.Borrowings.Dtos;
using Application.Categories.Dtos;
using Application.Categories.Services;
using AutoMapper;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Helper.Pagination;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Shared;
using FluentValidation;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper,
            IValidator<CreateCategoryDto> createCategoryValidator,
            IValidator<UpdateCategoryDto> updateCategoryValidator)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryValidator = updateCategoryValidator;
        }

        public async Task<PagedResult<CategoryDto>> GetCategoriesAsync(int pageNumber = 1, int pageSize = 10)
        {
            var categories = _categoryRepository.GetCategories();
            var paginatedCategories = await categories.ToPaginationForm(pageNumber, pageSize);
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(paginatedCategories.Items);
            return new PagedResult<CategoryDto>
            {
                TotalCount = paginatedCategories.TotalCount,
                Items = categoriesDto,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<Result<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null) { return Result<CategoryDto>.Fail(ErrorMessages.NotFoundById(id)); }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Result<CategoryDto>.Ok(categoryDto);
        }

        public async Task<Result<CategoryDto>> CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            var validationResult = await _createCategoryValidator.ValidateAsync(categoryDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<CategoryDto>.Fail(errorMessages);
            }
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
            var cDto = _mapper.Map<CategoryDto>(category);
            return Result<CategoryDto>.Ok(cDto);
        }
        public async Task<Result<CategoryDto>> UpdateCategoryAsync(string id, UpdateCategoryDto categoryDto)
        {
            var validationResult = await _updateCategoryValidator.ValidateAsync(categoryDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<CategoryDto>.Fail(errorMessages);
            }
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return Result<CategoryDto>.Fail(ErrorMessages.NotFoundById(id));
            }
            _mapper.Map(categoryDto, category);
            await _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();
            var cDto = _mapper.Map<CategoryDto>(category);
            return Result<CategoryDto>.Ok(cDto);
        }

        public async Task<Result<CategoryDto>> DeleteCategoryAsync(string id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return Result<CategoryDto>.Fail(ErrorMessages.NotFoundById(id));
            }
            await _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();
            var cDto = _mapper.Map<CategoryDto>(category);
            return Result<CategoryDto>.Ok(cDto);
        }

    }
}
