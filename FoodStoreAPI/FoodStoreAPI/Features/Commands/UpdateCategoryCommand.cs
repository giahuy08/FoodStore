using AutoMapper;
using FoodStoreAPI.Commons.Constants;
using FoodStoreAPI.Entities;
using FoodStoreAPI.Features.Resources.Request;
using FoodStoreAPI.Repositories.Interfaces;
using FoodStoreAPI.Response;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FoodStoreAPI.Features.Commands
{
    public class UpdateCategoryCommand : IRequest<Response<int>>
    {
        public int Id { get; set; } 
        public CategoryRequest UpdateCategory { get; set; } = new CategoryRequest();
        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response<int>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<UpdateCategoryCommandHandler> _logger;

            public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateCategoryCommandHandler> logger)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            public async Task<Response<int>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var catExist = await _categoryRepository.FindAsync(x => x.Id == request.Id);
                if (catExist == null)
                {
                    return Response<int>.Fail(Constants.NOT_FOUND);
                }
                try
                {
                    var category = _mapper.Map(request.UpdateCategory, catExist);
                    throw new Exception("test");
                    _categoryRepository.Update(category);
                    await _unitOfWork.SaveChangesAsync();
                    return Response<int>.Success(Constants.UPDATE_SUCCESS);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to add category: {category} ", request.UpdateCategory.Name);
                    return Response<int>.Fail(Constants.UPDATE_FAIL);
                }
            }
        }
    }
}
