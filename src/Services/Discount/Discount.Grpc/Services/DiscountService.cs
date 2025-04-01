using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repostories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DiscountService> _logger;
        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _discountRepository =discountRepository;
            _logger=logger;
            _mapper=mapper;
        }
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
            }
            _logger.LogInformation("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);
            var CouponModel = _mapper.Map<CouponModel>(coupon);
            return CouponModel;
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var Coupon = _mapper.Map<Coupon>(request.Coupon);
            await _discountRepository.CreateDiscount(Coupon);
            _logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", Coupon.ProductName);
            var CouponModel = _mapper.Map<CouponModel>(Coupon);
            return CouponModel;
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _discountRepository.UpdateDiscount(coupon);
            _logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);
            var CouponModel = _mapper.Map<CouponModel>(coupon);
            return CouponModel;
        }



        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var Coupon = await _discountRepository.DeleteDiscount(request.ProductName);
            var Response = new DeleteDiscountResponse
            {
                Success=Coupon

            };
            return Response;

        }
    }
}
