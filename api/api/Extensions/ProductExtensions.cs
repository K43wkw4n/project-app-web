using Microsoft.EntityFrameworkCore; 
namespace api.Extensions
{
    public static class ProductExtenstions
    {
        public static IQueryable<Product> Filter(this IQueryable<Product> query, string brand)
        {
            var brandList = new List<string>();

            if (!string.IsNullOrEmpty(brand)) brandList.AddRange(brand.ToLower().Split(",").ToList());
            //กระบวนการวนลูปอยู่ภายใน (ทำอยู่ข้างใน)
            //รูปแบบมันจะดูยาก
            query = query.Where(p => brandList.Count == 0 || brandList.Contains(p.Brand.ToLower()));
            return query;
        }

        public static IQueryable<Product> RangePrice(this IQueryable<Product> query, int RangeStart, int RangeEnd)
        {
            if (RangeStart == 0 && RangeEnd == 0) return query;
            query = query.Where(p => p.Price >= RangeStart && p.Price <= RangeEnd);
            return query;
        }

        public static IQueryable<Product> Search(this IQueryable<Product> query, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return query;
            // Trim เป็นการตัดช่องว่างทิ้ง
            // ToLower เป็นตัวเล็ก
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return query.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm));
        }

        //public static IQueryable<Product> ByAccountID(this IQueryable<Product> query, string accountId)
        //{
        //    if (string.IsNullOrEmpty(accountId)) return query;

        //    return query.Where(p => p.AccountID.Equals(accountId));
        //}
         
        //private static double GetAverageScore(string? productId, ApplicationDbContext db)
        //{
        //    var reviews = db.Reviews.ByProductID(productId).ToList();
        //    double averageScore = 0;
        //    if (reviews?.Count > 0)
        //    {
        //        var sumScore = reviews.Sum(e => e.Score);
        //        averageScore = sumScore / reviews.Count();
        //    }
        //    return averageScore;
        //}
    }
}
