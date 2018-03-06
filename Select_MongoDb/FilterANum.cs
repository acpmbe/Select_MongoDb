using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Select_MongoDb
{

    /// <summary>
    /// 字段代表的数字。
    /// </summary>
    public class FilterANum
    {



        //        类型 对应数字    别名 说明
        //Double1	1	double
        //String	2	string
        //Object	3	object
        //Array	4	array
        //Binary data	5	binData
        //Undefined	6	undefined 弃用
        //ObjectId	7	objectId
        //Boolean	8	“bool”	 
        //Date	9	“date”	 
        //Null	10	“null”	 
        //Regular Expression	11	“regex”	 
        //DBPointer	12	“dbPointer”	 
        //JavaScript	13	“javascript”	 
        //Symbol	14	“symbol”	 
        //JavaScript(with scope)  15	“javascriptWithScope”	 
        //32-bit integer	16	“int”	 
        //Timestamp	17	“timestamp”	 
        //64-bit integer	18	“long”	 
        //Min key	-1	“minKey”	 
        //Max key	127	“maxKey”


        public const int Double1 = 1;
        public const int String = 2;
        public const int Object = 3;
        public const int Array = 4;
        public const int binData = 5;
        public const int Undefined = 6;
        public const int ObjectId = 7;
        public const int Boolean = 8;
        public const int Date = 9;
        public const int Null = 10;
        public const int RegularExpression = 11;
        public const int DBPointer = 12;
        public const int JavaScript = 13;
        public const int Symbol = 14;
        public const int javascriptWithScope = 15;
        public const int Int_32 = 16;
        public const int Timestamp = 17;
        public const int Int_64 = 18;
        public const int MinKey = -1;
        public const int MaxKey = 127;



    }
}
