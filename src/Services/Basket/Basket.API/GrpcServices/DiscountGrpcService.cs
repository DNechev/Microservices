using Discount.gRPC.Protos;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountService.DiscountServiceClient _discountServiceClient;

        public DiscountGrpcService(DiscountService.DiscountServiceClient discountServiceClient)
        {
            _discountServiceClient = discountServiceClient;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest() { ProductName = productName };

            return await _discountServiceClient.GetDiscountAsync(discountRequest);
        }
    }
}
