using AutoMapper;
using Bookify_Library_mgnt.Common;
using Bookify_Library_mgnt.Dtos.Borrowings;
using Bookify_Library_mgnt.Dtos.Categories;
using Bookify_Library_mgnt.Helper.Pagination;
using Bookify_Library_mgnt.Models;
using Bookify_Library_mgnt.Repositpries.Interfaces;
using Bookify_Library_mgnt.Services.Interfaces;
using FluentValidation;

namespace Bookify_Library_mgnt.Services.Implementations
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
            var categories = _categoryRepository.GetCategoriesAsync();
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
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) { return Result<CategoryDto>.Fail(ErrorMessages.NotFoundById(id)); }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Result<CategoryDto>.Ok(categoryDto);
        }

        public async Task<Result<Category>> CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            var validationResult = await _createCategoryValidator.ValidateAsync(categoryDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<Category>.Fail(errorMessages);
            }
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.CreateCategoryAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return Result<Category>.Ok(category);
        }
        public async Task<Result<Category>> UpdateCategoryAsync(string id, UpdateCategoryDto categoryDto)
        {
            var validationResult = await _updateCategoryValidator.ValidateAsync(categoryDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<Category>.Fail(errorMessages);
            }
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return Result<Category>.Fail(ErrorMessages.NotFoundById(id));
            }
            _mapper.Map(categoryDto, category);
            await _categoryRepository.UpdateCategoryAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return Result<Category>.Ok(category);
        }

        public async Task<Result<Category>> DeleteCategoryAsync(string id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return Result<Category>.Fail(ErrorMessages.NotFoundById(id));
            }
            await _categoryRepository.DeleteCategoryAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return Result<Category>.Ok(category);
        }

    }
}
