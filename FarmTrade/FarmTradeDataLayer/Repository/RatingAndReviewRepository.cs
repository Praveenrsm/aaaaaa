using FarmTradeEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmTradeDataLayer.Repository
{
    public class RatingAndReviewRepository : IRatingAndReviewRepository
    {
        FarmContext _farmcontext;
        public RatingAndReviewRepository(FarmContext context)
        {
            _farmcontext = context;
        }

        //public ReviewsAndRatings GetRRById(int rRId)
        //{
        //    #region GET Single RR 
        //    var result = _farmcontext.ratingsreview.ToList();
        //    var Rr = result.Where(obj => obj.Id == rRId).FirstOrDefault();
        //    return Rr;
        //    #endregion
        //}

        public void DeleteRR(int rRId)
        {
            #region DELETE RR
            var rR = _farmcontext.ratingsreview.Find(rRId);
            _farmcontext.ratingsreview.Remove(rR);
            _farmcontext.SaveChanges();
            #endregion
        }

        public void AddOrUpdateReview(int productId, Guid userId, string comments, int rating)
        {
            var review = _farmcontext.ratingsreview
                .FirstOrDefault(r => r.ProductId == productId && r.UserId == userId);

            if (review != null)
            {
                // Update the review
                review.Comments = comments;
                review.Rating = (Rating)rating;
            }
            else
            {
                // Add a new review
                var newReview = new ReviewsAndRatings
                {
                    ProductId = productId,
                    UserId = userId,
                    Comments = comments,
                    Rating = (Rating)rating
                };
                _farmcontext.ratingsreview.Add(newReview);
            }

            _farmcontext.SaveChanges();
        }

    }
}
