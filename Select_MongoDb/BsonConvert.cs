using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Select_MongoDb
{
    /// <summary>
    /// Bson转换类。
    /// </summary>
    public class BsonConvert
    {


        /// <summary>
        /// bson列表转换成实体列表。
        /// </summary>
        /// <param name="list">bson列表</param>
        /// <returns></returns>
        public static List<Http_Mod> ConvertListMod(List<BsonDocument> list)
        {

            List<Http_Mod> listH = new List<Http_Mod>();
            foreach (var v in list)
            {
                listH.Add(ConvertMod(v));
            }
            return listH;
        }





        /// <summary>
        /// bson转换成实体。
        /// </summary>
        /// <param name="bson">bson文档</param>
        /// <returns></returns>
        public static Http_Mod ConvertMod(BsonDocument bson)
        {

            Dictionary<string, object> dic = bson.ToDictionary();
            Http_Mod info = new Http_Mod();
            foreach (var item in dic)
            {

                switch (item.Key)
                {
                    case "LID":
                        info.LID = Convert.ToInt64(item.Value);
                        break;
                    case "CREATED":
                        info.CREATED = Convert.ToDateTime(item.Value);
                        break;
                    case "RN":
                        info.RN = item.Value.ToString();
                        break;
                    case "DEVID":
                        info.DEVID = Convert.ToInt32(item.Value);
                        break;
                    case "RESERVE":
                        info.RESERVE = item.Value.ToString();
                        break;
                    case "SUBDEVTYPE":
                        info.SUBDEVTYPE = item.Value.ToString();
                        break;
                    case "SPEID":
                        info.SPEID = item.Value.ToString();
                        break;
                    case "VERSION":
                        info.VERSION = item.Value.ToString();
                        break;
                    case "CMDID":
                        info.CMDID = item.Value.ToString();
                        break;
                    case "COMTENT":
                        info.COMTENT = item.Value.ToString();
                        break;
                }

            }

            return info;
        }
    }
}
