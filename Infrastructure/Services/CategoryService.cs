using Application.Borrowings.Dtos;
using Application.Categories.Dtos;
using Application.Categories.Services;
using Application.Common.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;
        public CategoryService(IMapper mapper,
            IValidator<CreateCategoryDto> createCategoryValidator,
            IValidator<UpdateCategoryDto> updateCategoryValidator, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryValidator = updateCategoryValidator;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<CategoryDto>> GetCategoriesAsync(int pageNumber = 1, int pageSize = 10)
        {
            var categories = _unitOfWork.CategoryRepository.GetCategories();
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
            var category = await _unitOfWork.CategoryRepository.GetCategoryById(id);
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
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.CategoryRepository.SaveChangesAsync();
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
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return Result<CategoryDto>.Fail(ErrorMessages.NotFoundById(id));
            }
            _mapper.Map(categoryDto, category);
            await _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.CategoryRepository.SaveChangesAsync();
            var cDto = _mapper.Map<CategoryDto>(category);
            return Result<CategoryDto>.Ok(cDto);
        }

        public async Task<Result<CategoryDto>> DeleteCategoryAsync(string id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return Result<CategoryDto>.Fail(ErrorMessages.NotFoundById(id));
            }
            await _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.CategoryRepository.SaveChangesAsync();
            var cDto = _mapper.Map<CategoryDto>(category);
            return Result<CategoryDto>.Ok(cDto);
        }

    }
}
