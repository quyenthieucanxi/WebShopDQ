using AutoMapper;
using MailKit.Search;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common;
using WebShopDQ.App.Common.Constant;
using WebShopDQ.App.Data;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services
{
    public class OrderReviewService : IOrderReviewService
    {
        private readonly IOrderReviewRepository _orderReviewRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderReviewService(
            IOrderReviewRepository orderReviewRepository,
            IOrderRepository orderRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IPostRepository postRepository)
        {
            _orderReviewRepository = orderReviewRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        public async Task<ValueReviewViewModel> CountOrderReview(string url)
        {
            var user = await _userRepository.FindAsync(u => u.Url == url) 
                    ?? throw new KeyNotFoundException(Messages.UserNotFound);
            var ordersReview = await _orderReviewRepository.CountOrderReview(user.Id);
            var countReview =  ordersReview.Count();
            var totalRating = ordersReview.Sum(o => o.Rating);
            var averageRating = countReview > 0 ? Convert.ToDouble(totalRating)/Convert.ToDouble(countReview) : 0;
            var averageRatingFormat = averageRating == 0 ? 0 : Math.Round(averageRating, 1);
            return await Task.FromResult( 
                new ValueReviewViewModel{ totalReview = countReview,averageRating = averageRatingFormat });
        }

        public async Task<ValueReviewViewModel> CountProductReview(string pathPost)
        {
            var product = await _postRepository.FindAsync(p => p.PostPath == pathPost)
                        ?? throw new KeyNotFoundException(Messages.PostNotFound);
            var ordersReview = await _orderReviewRepository.GetOrderReviews(product.Id,null) ;
            var countReview = ordersReview!.Count() ;
            var totalRating = ordersReview.Sum(o => o.Rating);
            var averageRating = countReview > 0 ? Convert.ToDouble(totalRating) / Convert.ToDouble(countReview) : 0;
            var averageRatingFormat = averageRating == 0 ? 0 : Math.Round(averageRating, 1);
            return await Task.FromResult(
                new ValueReviewViewModel { totalReview = countReview, averageRating = averageRatingFormat });
        }

        public async Task<bool> Create(OrderReviewDTO orderReviewDTO, Guid userId)
        {
            var order = await _orderRepository.GetById(orderReviewDTO.OrderId) 
                        ?? throw new KeyNotFoundException(Messages.OrderNotFound);
            var orderReview = _mapper.Map<OrderReviews>(orderReviewDTO);
            try
            {
                _unitOfWork.BeginTransaction();
                orderReview.UserId = userId;
                await _orderReviewRepository.Add(orderReview);
                order.Status = OrderStatus.Review;
                await _orderRepository.Update(order);
                await _unitOfWork.SaveChanges();
                _unitOfWork.CommitTransaction();
                return await Task.FromResult(true);

            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<OrderReviewsViewModel>> GetOrderReviews(string pathPost,int? rating)
        {
            var product = await _postRepository.FindAsync(p => p.PostPath == pathPost)
                ?? throw new KeyNotFoundException(Messages.PostNotFound);
            var orderReviews = await _orderReviewRepository.GetOrderReviews(product.Id,rating);
            var orderReviewsVM = _mapper.Map<ICollection<OrderReviewsViewModel>>(orderReviews);
            return await Task.FromResult(orderReviewsVM);
        }

        public async Task<ICollection<OrderReviewsViewModel>> GetReviewsUser(string url)
        {
            var user = await _userRepository.FindAsync(u => u.Url == url)
                    ?? throw new KeyNotFoundException(Messages.UserNotFound);
            var orderReviews = await _orderReviewRepository.CountOrderReview(user.Id);
            foreach (var orderRV in orderReviews)
            {
                var product = await _postRepository.GetById(orderRV.Order!.ProductID);
                orderRV.Order.Products?.Add(product!);
            }
            var orderReviewsVM = _mapper.Map<ICollection<OrderReviewsViewModel>>(orderReviews);
            return await Task.FromResult(orderReviewsVM);
        }
    }
}
