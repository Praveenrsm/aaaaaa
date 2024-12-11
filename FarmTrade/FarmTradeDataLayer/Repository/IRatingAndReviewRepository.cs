using FarmTradeEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmTradeDataLayer.Repository
{
    public interface IRatingAndReviewRepository
    {
        //void UpdateRR(ReviewsAndRatings rr);
        //string AddRR(ReviewsAndRatings rr);
        //ReviewsAndRatings GetRRById(int rRId);
        //IEnumerable<ReviewsAndRatings> GetRRs();
        void DeleteRR(int rRId);
        void AddOrUpdateReview(int productId, Guid userId, string comments, int rating);
    }
}
