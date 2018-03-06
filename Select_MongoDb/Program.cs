using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;

using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Select_MongoDb
{
    class Program
    {


        private static string  CollectionName = "tendencydata";
        static void Main(string[] args)
        {
            try
            {
             
                string Result = MongoHelp.Init(Config.MongoDbConnStr, Config.MongoDbDatabase);
                if (Result != "0" && Result != "1")
                {
                    Console.WriteLine("连接数据库错误：" + Result);
                    return;
                }

                //Insert();
                //MongoHelp.Client.Settings.MaxConnectionPoolSize = 1000;
                //MongoHelp.Client.Settings.MinConnectionPoolSize = 100;
                //MongoHelp.Client.Settings.WaitQueueSize = 1000;
                //MongoHelp.Client.Settings.WaitQueueTimeout = TimeSpan.FromSeconds(1000);

                Console.WriteLine("连接数据库成功！");
                while (true)
                {
                    string str = Console.ReadLine();
                    string[] array = str.ToUpper().Split(',');
                    switch (array[0])
                    {
                        case "HELP":
                            Console.WriteLine("SPEID,TIME,CMDID,SUBDEVTYPE,COMTENT,COUNT,EXIT");
                            break;
                        case "SPEID":
                            FindSpeId(array[1]);
                            break;
                        case "TIME":
                            FindTime(array[1], array[2]);
                            break;
                        case "CMDID":
                            FindCmdId(array[1]);
                            break;
                        case "SUBDEVTYPE":
                            FindSubDevTypr(array[1]);
                            break;
                        case "COMTENT":
                            FindContent(array[1]);
                            break;
                        case "COUNT":
                            Count();
                            break;
                        case "STATUS":
                            Status();
                            break;
                        case "EXIT":
                            Console.WriteLine("已退出程序。");
                            return;

                    }

                    
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            Console.Read();


      
        }

        static void Count()
        {
            long count = MongoHelp.Count(CollectionName);
            Console.WriteLine(count);
        }

        static void Status()
        {
            MongoHelp.Status();
        }

        static void FindSpeId(string speid)
        {
            BsonDocument find = new BsonDocument();
            find.Add("SPEID", speid);

            BsonDocument sort = BsonDocumentHelp.SortDesc("LID");
         


            DateTime beforDT = System.DateTime.Now;


            List<BsonDocument> list = MongoHelp.SelectSort(CollectionName, find, sort);

            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            Console.WriteLine("DateTime总共花费{0}ms.", ts.TotalMilliseconds);


            Console.WriteLine("数据量：" + list.Count + "条");

       
        }

        static void FindTime(string startTime,string endTime )
        {
         
            BsonDocument find = new BsonDocument();
       
            Dictionary<string, object> dic = new Dictionary<string, object>();    
            DateTime time_D = Convert.ToDateTime(startTime);
            DateTime time_X = Convert.ToDateTime(endTime);
            dic.Add(BsonDocumentHelp.大于等于, time_D);
            dic.Add(BsonDocumentHelp.小于等于, time_X);
            find.Add("CREATED", new BsonDocument(dic));

            BsonDocument sort = BsonDocumentHelp.SortDesc("LID");


            DateTime beforDT = System.DateTime.Now;


            List<BsonDocument> list = MongoHelp.SelectSort(CollectionName, find, sort);


            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            Console.WriteLine("DateTime总共花费{0}ms.", ts.TotalMilliseconds);


            Console.WriteLine("数据量：" + list.Count + "条");


    

      
        }



        static void FindCmdId(string cmdId)
        {
            BsonDocument find = new BsonDocument();
            find.Add("CMDID", cmdId);

            BsonDocument sort = BsonDocumentHelp.SortDesc("LID");



            DateTime beforDT = System.DateTime.Now;


            List<BsonDocument> list = MongoHelp.SelectSort(CollectionName, find, sort);

            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            Console.WriteLine("DateTime总共花费{0}ms.", ts.TotalMilliseconds);


            Console.WriteLine("数据量：" + list.Count + "条");


        }

        static void FindSubDevTypr(string subDevType)
        {
            BsonDocument find = new BsonDocument();
            find.Add("SUBDEVTYPE", subDevType);

            BsonDocument sort = BsonDocumentHelp.SortDesc("LID");



            DateTime beforDT = System.DateTime.Now;


            List<BsonDocument> list = MongoHelp.SelectSort(CollectionName, find, sort);

            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            Console.WriteLine("DateTime总共花费{0}ms.", ts.TotalMilliseconds);


            Console.WriteLine("数据量：" + list.Count + "条");


        }



        static void FindContent(string content)
        {


            BsonDocument find = new BsonDocument();
            find.Add("COMTENT", BsonRegularExpression.Create(content));

            BsonDocument sort = BsonDocumentHelp.SortDesc("LID");

            DateTime beforDT = System.DateTime.Now;


            List<BsonDocument> list = MongoHelp.SelectSort(CollectionName, find, sort);

            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            Console.WriteLine("DateTime总共花费{0}ms.", ts.TotalMilliseconds);


            Console.WriteLine("数据量：" + list.Count + "条");
        }

        static void FindSort(string CollectionName)
        {
            BsonDocument find = new BsonDocument();
            find.Add("RN", "A");


            BsonDocument sort = new BsonDocument();
            find.Add("LID", -1);
            List<BsonDocument> list = MongoHelp.SelectSort(CollectionName, find, sort);

            List<Http_Mod> listH = BsonConvert.ConvertListMod(list);
        }

        static void Find3(string CollectionName)
        {

            BsonDocument find = new BsonDocument();
            find.Add("RN", "A");

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("$gt", 3);
            dic.Add("$lt", 13);
            find.Add("LID", new BsonDocument(dic));

            List<BsonDocument> list = MongoHelp.Select(CollectionName, find);

            List<Http_Mod> listH = BsonConvert.ConvertListMod(list);
        }

        static void Find(string CollectionName)
        {
            BsonDocument find = new BsonDocument();
            find.Add("RN", "A");

            List<BsonDocument> list = MongoHelp.Select(CollectionName, find);

            List<Http_Mod> listH = BsonConvert.ConvertListMod(list);

        }
        static void Update(string CollectionName)
        {

            BsonDocument find = new BsonDocument();
            find.Add("SPEID", "CC");

            Dictionary<string, Object> dic = new Dictionary<string, object>();
            dic.Add("SPEID", "CC");
            dic.Add("CMDID", "VV");

            BsonDocument update = BsonDocumentHelp.Update(dic);

            string R = MongoHelp.Update(CollectionName, find, update);



        }

        static void Insert()
        {


            List<BsonDocument> list = ImitateData.GetList(CollectionName);

            MongoHelp.Insert(CollectionName, list);


        }


    }
}
