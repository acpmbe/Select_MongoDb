using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Select_MongoDb
{

    /// <summary>
    ///  Bson文档帮助类。
    /// </summary>
    public class BsonDocumentHelp
    {

        #region 参考

        //查询：$gt:大于，$lt:小于，$gte:大于或等于，$lte:小于或等于,
        //举例：new BsonDocument().Add("LID", new BsonDocument("$gt", 3));
        //更新：new BsonDocument().Add("$set", new BsonDocument("SPEID", "51"));

        #endregion

        public const string 大于 = "$gt";
        public const string 大于等于 = "$gte";

        public const string 小于 = "$lt";
        public const string 小于等于 = "$lte";

        public const string 设置 = "$set";


        private const int 正序 = 1;
        private const int 倒序 = -1;









        /// <summary>
        /// 大于和小于。
        /// </summary>
        private static void GreaterAndLess()
        {
            
            BsonDocument bson = new BsonDocument();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("$gt", 1);
            dic.Add("$lt", 5);
            bson.Add("LID", new BsonDocument(dic));
        }


        /// <summary>
        /// 大于等于和小于等于。
        /// </summary>
        public static void GreaterEqualAndLessEqual()
        {
            BsonDocument bson = new BsonDocument();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("$gte", 1);
            dic.Add("$lte", 5);
            bson.Add("LID", new BsonDocument(dic));
        }

        /// <summary>
        /// 排序，正序。
        /// </summary>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static BsonDocument Sort(string field)
        {
            return new BsonDocument().Add(field, 正序);
        }


        /// <summary>
        /// 排序，倒序。
        /// </summary>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public static BsonDocument SortDesc(string field)
        {
            return new BsonDocument().Add(field, 倒序);
        }










        /// <summary>
        /// 返回更新BsonDocument对象。
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static BsonDocument Update(Dictionary<string,object> dic)
        {


            return new BsonDocument().Add(设置, new BsonDocument(dic));

        }

    }
}
