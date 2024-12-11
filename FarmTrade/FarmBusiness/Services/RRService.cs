using FarmTradeDataLayer.Repository;
using FarmTradeEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmBusiness.Services
{
    public class RRService
    {
        IRatingAndReviewRepository _RatingAndReviewRepository;
        public RRService(IRatingAndReviewRepository RatingAndReviewRepository)
        {
            _RatingAndReviewRepository = RatingAndReviewRepository;
        }

        // CRUD Service Operations for RR:
        public void DeleteRR(int rRId)
        {
            _RatingAndReviewRepository.DeleteRR(rRId);
        }
        public void AddOrUpdateReview(int productId, Guid userId, string comments, int rating)
        {
            _RatingAndReviewRepository.AddOrUpdateReview(productId, userId, comments,rating);
        }
    }
}
