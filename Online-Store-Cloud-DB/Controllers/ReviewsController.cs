using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service;

namespace Online_Store_Cloud_DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
        }

        // POST api/reviews
        [HttpPost]
        public async Task<IActionResult> PostReview([FromBody] ReviewDTO reviewDto)
        {
            if (reviewDto == null)
            {
                return BadRequest("Review data is required.");
            }

            // Assuming you have a method in the service to add a review
            await _reviewService.AddReview(reviewDto);

            return CreatedAtAction(nameof(GetReviewsByProductId), new { id = reviewDto.Id }, reviewDto);
        }

        // GET api/reviews/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewsByProductId(string id)
        {
            var review = await _reviewService.GetReviewsByProductId(id);

            if (review == null)
            {
                return NotFound($"Review with ID {id} not found.");
            }

            return Ok(review);
        }

        // GET api/reviews
        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviews();
            return Ok(reviews);
        }
    }

}
