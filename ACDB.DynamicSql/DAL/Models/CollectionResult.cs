using System.Collections.Generic;

namespace ACDB.DynamicSql.DAL.Models
{
    public class CollectionResult<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Collection { get; set; }

        public int TotalCount { get; set; }
    }
}