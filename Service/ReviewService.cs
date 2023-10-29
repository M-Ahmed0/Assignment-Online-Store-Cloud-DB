using AutoMapper;
using DAL.Repositories;
using Domain.Entities;
using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewsByProductId(string productId)
        {
            var reviews = await _reviewRepository.GetReviewsByProductId(productId);
            return _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
        }

        public async Task<ReviewDTO> AddReview(ReviewDTO reviewDto)
        {
            if (reviewDto == null)
            {
                throw new ArgumentNullException(nameof(reviewDto));
            }

            // Automatically assign id to the review
            reviewDto.Id = Guid.NewGuid().ToString();

            var review = _mapper.Map<Review>(reviewDto);
            await _reviewRepository.Add(review);

            
            return reviewDto;
        }


        public async Task<IEnumerable<ReviewDTO>> GetAllReviews()
        {
            var reviews = await _reviewRepository.GetAll();
            return _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
        }
    }
}
