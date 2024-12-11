using FarmBusiness.Services;
using FarmTradeEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RatingReviewController : ControllerBase
    {
        private RRService _rRService;
        public RatingReviewController(RRService rrService)
        {
            _rRService = rrService;
        }
        
        [HttpDelete("DeleteRR")]
        public IActionResult DeleteRR(int rRId)
        {
            #region Delete RR
            _rRService.DeleteRR(rRId);
            return Ok("RR deleted successfully!!!");
            #endregion
        }
        [HttpPost("AddOrUpdateReview")]
        public IActionResult AddOrUpdateReview(int productId, Guid userId, string comments, int rating)
        {
            _rRService.AddOrUpdateReview(productId, userId, comments,rating);
            return Ok("RR updated successfully!!!");
        }
    }
}
