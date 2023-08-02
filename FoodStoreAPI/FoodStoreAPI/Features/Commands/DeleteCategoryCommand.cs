using AutoMapper;
using FoodStoreAPI.Commons.Constants;
using FoodStoreAPI.Entities;
using FoodStoreAPI.Features.Resources.Request;
using FoodStoreAPI.Repositories.Interfaces;
using FoodStoreAPI.Response;
using MediatR;

namespace FoodStoreAPI.Features.Commands
{
    public class DeleteCategoryCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Response<int>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
                _unitOfWork = unitOfWork;
            }

            public async Task<Response<int>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var catExist = await _categoryRepository.FindAsync(x => x.Id == request.Id);
                if (catExist == null)
                {
                    return Response<int>.Fail(Constants.NOT_FOUND);
                }
                try
                {
                    _categoryRepository.Remove(catExist);
                    await _unitOfWork.SaveChangesAsync();
                    return Response<int>.Success(Constants.DELETE_SUCCESS);
                }
                catch (Exception ex)
                {
                    return Response<int>.Fail(Constants.DELETE_FAIL);
                }
            }
        }
    }
}
