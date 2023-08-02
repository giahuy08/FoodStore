using AutoMapper;
using FoodStoreAPI.Commons.Constants;
using FoodStoreAPI.Entities;
using FoodStoreAPI.Features.Resources.Request;
using FoodStoreAPI.Repositories.Interfaces;
using FoodStoreAPI.Response;
using MediatR;

namespace FoodStoreAPI.Features.Commands
{
    public class CreateCategoryCommand : IRequest<Response<int>>
    {
        public CategoryRequest createCategory { get; set; } = new CategoryRequest();
        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<int>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
                _unitOfWork = unitOfWork;
            }

            public async Task<Response<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var catExist = await _categoryRepository.FindAsync(x => x.Name == request.createCategory.Name);

                if (catExist != null)
                {
                    return Response<int>.Fail(Constants.CATEGORY_EXIST);
                }

                try
                {
                    var category = _mapper.Map<Category>(request.createCategory);
                    await _categoryRepository.AddAsync(category);
                    await _unitOfWork.SaveChangesAsync();
                    return Response<int>.Success(Constants.CREATE_SUCCESS);
                }
                catch (Exception ex)
                {
                    return Response<int>.Fail(Constants.CREATE_FAIL);
                }
            }
        }
    }
}
