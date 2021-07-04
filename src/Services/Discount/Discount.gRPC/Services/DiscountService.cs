using AutoMapper;
using Discount.gRPC.Entities;
using Discount.gRPC.Protos;
using Discount.gRPC.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static Discount.gRPC.Protos.DiscountService;

namespace Discount.gRPC.Services
{
    public class DiscountService : DiscountServiceBase
    {
        private readonly IDiscountRepository _discountRepository;

        private readonly ILogger<DiscountService> _logger;

        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);

            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with {request.ProductName} not found!"));
            }

            _logger.LogInformation($"Discount for {request.ProductName} is retrieved - {coupon.Amount}");

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var model = _mapper.Map<CouponModel>(await _discountRepository.CreateDiscount(_mapper.Map<Coupon>(request.Coupon)));

            _logger.LogInformation($"Created Discount for {request.Coupon.ProductName} with amount - {model.Amount}");

            return model;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var model = _mapper.Map<CouponModel>(await _discountRepository.UpdateDiscount(_mapper.Map<Coupon>(request.Coupon)));

            _logger.LogInformation($"Updated Discount for {request.Coupon.ProductName}.");

            return model;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _discountRepository.DeleteDiscount(request.ProductName);

            _logger.LogInformation(deleted ? $"Deleted Discount for {request.ProductName}" : $"Deleted Discount for { request.ProductName}");

            return new DeleteDiscountResponse() { Success = deleted };
        }
    }
}
