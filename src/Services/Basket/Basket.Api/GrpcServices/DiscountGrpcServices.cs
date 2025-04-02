using Discount.Grpc.Protos;

namespace Basket.Api.GrpcServices
{
    public class DiscountGrpcServices
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;
        public DiscountGrpcServices(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient=discountProtoServiceClient;
        }
        public async Task<CouponModel> GetDiscount(string ProductName)
        { 
        var DiscounRequest = new GetDiscountRequest { ProductName = ProductName };
            return await _discountProtoServiceClient.GetDiscountAsync(DiscounRequest);


        }
    }
}
