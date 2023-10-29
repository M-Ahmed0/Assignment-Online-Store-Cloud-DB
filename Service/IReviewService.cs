using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDTO>> GetReviewsByProductId(string productId);
        Task<IEnumerable<ReviewDTO>> GetAllReviews();

        Task<ReviewDTO> AddReview(ReviewDTO reviewDto);
       
    }
}
