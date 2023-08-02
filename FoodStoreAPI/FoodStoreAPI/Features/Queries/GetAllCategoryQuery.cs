using AutoMapper;
using FoodStoreAPI.Features.Resources.Request;
using FoodStoreAPI.Repositories.Interfaces;
using MediatR;

namespace FoodStoreAPI.Features.Queries
{
    public class GetAllCategoryQuery : IRequest<List<CategoryRequest>>
    {
        public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, List<CategoryRequest>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;
            public GetAllCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<List<CategoryRequest>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
            {
                var result = await _categoryRepository.GetAllAsync();
                return _mapper.Map<List<CategoryRequest>>(result);
            }
        }
    }
}
