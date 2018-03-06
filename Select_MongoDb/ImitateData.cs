using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Select_MongoDb
{
 public   class ImitateData
    {

    public    static List<BsonDocument> GetList(string CollectionName)
        {


            List<BsonDocument> list = new List<BsonDocument>();
            Http_Mod doc;
            for (int i = 200000; i < 500000; i++)
            {
                doc = new Http_Mod();
                doc.LID = i;
                doc.CMDID = GetCmdId();
                doc.COMTENT = GetContent();
                doc.CREATED = DateTime.Now;
                doc.DEVID = GetDevice();
                doc.RESERVE = "00000000";
                doc.RN = "0000";
                doc.SPEID = "136";
                doc.SUBDEVTYPE = GetSubDevType();
                doc.VERSION = "10";
                list.Add(doc.ToBsonDocument());

            }

            return list;


         


        }

        /// <summary>
        /// 随机6位整数。
        /// </summary>
        /// <returns></returns>
        public static int GetDevice()
        {
            Random rd = new Random();
            return rd.Next(313131, 313162);


        

        }

        /// <summary>
        /// 随机4位整数转字符串
        /// </summary>
        /// <returns></returns>
        public static string GetSubDevType()
        {
            Random rd = new Random();
            return  rd.Next(8021, 8031).ToString();
        }

        /// <summary>
        /// 随机3位整数转字符串。
        /// </summary>
        /// <returns></returns>
        public static string GetSpeId()
        {
            Random rd = new Random();
            return rd.Next(136, 141).ToString();
        }

        /// <summary>
        /// 随机5位整数转字符串。
        /// </summary>
        /// <returns></returns>
        public static string GetCmdId()
        {
            Random rd = new Random();
            return rd.Next(61443, 61456).ToString();
        }

        public static string GetContent()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        }
        //110C190C2C0F01002F00010002

  
         
    }
}
