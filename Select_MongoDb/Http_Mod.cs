using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Select_MongoDb
{

    /// <summary>
    /// Http实体。
    /// </summary>
    public class Http_Mod
    {

        public Int64 LID { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CREATED { get; set; }
        public string RN { get; set; }
        public Int32 DEVID { get; set; }
        public string RESERVE { get; set; }
        public string SUBDEVTYPE { get; set; }
        public string SPEID { get; set; }
        public string VERSION { get; set; }
        public string CMDID { get; set; }
        public string COMTENT { get; set; }
    }
}
