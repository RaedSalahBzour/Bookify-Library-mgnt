using Application.Categories.Dtos;
using Application.Categories.Services;
using AutoMapper;
using Data.Entities;
using Data.Interfaces;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;

            _unitOfWork = unitOfWork;
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var categories = _unitOfWork.CategoryRepository.GetCategories();
            return _mapper.Map<List<CategoryDto>>(categories);

        }
        public async Task<CategoryDto> GetByIdAsync(string id)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategoryById(id);
            if (category == null)
                throw new KeyNotFoundException($"Category With Id {id} Was Not Found");
            return _mapper.Map<CategoryDto>(category);

        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.CategoryRepository.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }
        public async Task<CategoryDto> UpdateCategoryAsync(string id, UpdateCategoryDto categoryDto)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category is null)
                throw new KeyNotFoundException($"Category With Id {id} Was Not Found");
            _mapper.Map(categoryDto, category);
            await _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.CategoryRepository.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);

        }

        public async Task<CategoryDto> DeleteCategoryAsync(string id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category is null)
                throw new KeyNotFoundException($"Category With Id {id} Was Not Found");
            await _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.CategoryRepository.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }

    }
}
