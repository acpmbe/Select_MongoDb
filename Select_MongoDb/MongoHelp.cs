using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.IO;
using System.Threading;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization.Attributes;
namespace Select_MongoDb
{

    #region 参考
    //查询：$gt:大于，$lt:小于，$gte:大于或等于，$lte:小于或等于,
    //举例：new BsonDocument().Add("LID", new BsonDocument("$gt", 3));
    //更新：new BsonDocument().Add("$set", new BsonDocument("SPEID", "51"));
 
    #endregion




    /// <summary>
    /// MongoDb帮助类
    /// </summary>
    public class MongoHelp
    {

        private static MongoClient Client;
        private static IMongoDatabase Database;

        private static string MongoDbConnStr;
        private static string DatabaseName;

     

        /// <summary>
        /// 初始化（返回0:成功，1:已经初始化过，其它：出错信息）
        /// </summary>
        /// <param name="mongoDbConnStr">连接字符串</param>
        /// <param name="databaseName">数据库名称</param>
        /// <returns></returns>
        public static string Init(string mongoDbConnStr, string databaseName)
        {
            try
            {
                if (Client == null || Database == null)
                {
                    MongoDbConnStr = mongoDbConnStr;
                    DatabaseName = databaseName;
                    Client = new MongoClient(mongoDbConnStr);
                    Database = Client.GetDatabase(databaseName);
                    return "0";
               
                }
                else
                {
                    return "1";
                }
               
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
     

        }

        private static void Init()
        {
            if (Client == null || Database == null)
            {
                Client = new MongoClient(MongoDbConnStr);
                Database = Client.GetDatabase(DatabaseName);
            }

           
        }

        private static IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            Init();
            return Database.GetCollection<BsonDocument>(collectionName);
        }


        /// <summary>
        /// 创建集合。
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        private static void CreateCollection(string collectionName)
        {

            Init();
            Database.CreateCollectionAsync(collectionName).Wait();
        }


        /// <summary>
        /// 删除集合。
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        private static void DeleteCollection(string collectionName)
        {
            Init();
            Database.DropCollectionAsync(collectionName).Wait();

        }



        /// <summary>
        /// 重命名集合。
        /// </summary>
        /// <param name="oldname">旧的集合名称</param>
        /// <param name="newName">新的集合名称</param>
        private static void RenameColl(string oldname, string newName)
        {

            Init();
            Database.RenameCollectionAsync(oldname, newName).Wait();

        }



        /// <summary>
        /// 得到集合数据量。
        /// </summary>
        /// <param name="collectionName">集合名称。</param>
        /// <returns></returns>
        public static long Count(string collectionName)
        {

            var collection = GetCollection(collectionName);
            var Count = collection.CountAsync(new BsonDocument());
            Count.Wait();
            return Count.Result;

        }


        /// <summary>
        /// 查询集合当中第一条数据。
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public static BsonDocument SelectFirst(string collectionName)
        {

            var collection = GetCollection(collectionName);
            return collection.Find(new BsonDocument()).FirstOrDefaultAsync().Result;

        }


        /// <summary>
        /// 查询集合当中所有数据。
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public static List<BsonDocument> Select(string collectionName)
        {

            List<BsonDocument> list = new List<BsonDocument>();
            var collection = GetCollection(collectionName);
            collection.Find(new BsonDocument()).ForEachAsync(x => list.Add(x)).Wait();
            return list;

        }



        /// <summary>
        /// 通过条件查询单条数据。
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="bson">BsonDocument对象</param>
        /// <returns></returns>
        public static List<BsonDocument> Select(string collectionName,BsonDocument bson)
        {

            List<BsonDocument> list = new List<BsonDocument>();
            var collection = GetCollection(collectionName);
            collection.Find(bson).ForEachAsync(x => list.Add(x)).Wait();
            return list;

        }


        /// <summary>
        /// 插入数据。
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="list">BsonDocument列表</param>
        public static void Insert(string collectionName, List<BsonDocument> list)
        {

            var collection = GetCollection(collectionName);
            collection.InsertManyAsync(list).Wait();

        }


   



        /// <summary> 
        /// 更新符合条件的第一条数据，返回0:成功，其它：出错信息。
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="find">查询条件</param>
        /// <param name="update">更新值</param>
        /// <returns></returns>
        public static string Updatefirst(string collectionName, BsonDocument find, BsonDocument update)
        {

            try
            {
                var collection = GetCollection(collectionName);
                var result = collection.UpdateOneAsync(find, update);
                result.Wait();
                if (result.Result.IsAcknowledged)
                {
                    return "0";
                }
                else
                {
                    return "1";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }



        /// <summary>
        /// 更新符合条件的所有数据,返回0:成功，其它：出错信息。
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="find">查询条件</param>
        /// <param name="update">更新值</param>
        /// <returns></returns>
        public static string Update(string collectionName,BsonDocument find,BsonDocument update)
        {

            try
            {
                var collection = GetCollection(collectionName);
                var result = collection.UpdateManyAsync(find, update);
                result.Wait();
                if (result.Result.IsAcknowledged)
                {
                    return "0";
                }
                else
                {
                    return "1";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
    
        }




        /// <summary>
        /// 删除集合下所有数据。
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public static string Delete(string collectionName)
        {
            try
            {
                
                var collection = GetCollection(collectionName);
                var resut = collection.DeleteManyAsync(new BsonDocument());            
                resut.Wait();
                if (resut.Result.IsAcknowledged)
                {
                    return "0";
                }
                else
                {
                    return "1";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }





        }


        /// <summary>
        /// 删除所有符合条件的数据,返回0:成功，其它：出错信息
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="find">查询条件</param>
        /// <returns>返回0:成功，其它：出错信息</returns>
        public static string Delete(string collectionName,BsonDocument find)
        {
            try
            {
                var collection = GetCollection(collectionName);
                var resut = collection.DeleteManyAsync(find);
                resut.Wait();
                if (resut.Result.IsAcknowledged)
                {
                    return "0";
                }
                else
                {
                    return "1";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

        



        }




        /// <summary>
        /// 状态。
        /// </summary>
       public static void Status()
        {
            //config = MongoServerSettings.FromUrl(MongoUrl.Create(conStr));
            ////最大连接池
            //config.MaxConnectionPoolSize = 500;
            ////最大闲置时间
            //config.MaxConnectionIdleTime = TimeSpan.FromSeconds(30);
            ////最大存活时间
            //config.MaxConnectionLifeTime = TimeSpan.FromSeconds(60);
            ////链接时间
            //config.ConnectTimeout = TimeSpan.FromSeconds(10);
            ////等待队列大小
            //config.WaitQueueSize = 50;
            ////socket超时时间
            //config.SocketTimeout = TimeSpan.FromSeconds(10);
            ////队列等待时间
            //config.WaitQueueTimeout = TimeSpan.FromSeconds(60);
            ////操作时间
            //config.OperationTimeout = TimeSpan.FromSeconds(60);

            Console.WriteLine("最大连接池：" + Client.Settings.MaxConnectionPoolSize);
            Console.WriteLine("最小连接池：" + Client.Settings.MinConnectionPoolSize);

            Console.WriteLine("最大闲置时间：" + Client.Settings.MaxConnectionIdleTime);
            Console.WriteLine("最大存活时间：" + Client.Settings.MaxConnectionLifeTime);

            Console.WriteLine("链接时间：" + Client.Settings.ConnectTimeout);

            Console.WriteLine("等待队列大小：" + Client.Settings.WaitQueueSize);


            Console.WriteLine("socket超时时间：" + Client.Settings.SocketTimeout);


            Console.WriteLine("队列等待时间：" + Client.Settings.WaitQueueTimeout);

        



        }


        /// <summary>
        /// 通过条件查询并排序。
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="bson">BsonDocument查询对象</param>
        /// <param name="sort">BsonDocument排序对象</param>
        /// <returns></returns>
        public static List<BsonDocument> SelectSort(string collectionName, BsonDocument bson, BsonDocument sort)
        {


            List<BsonDocument> list = new List<BsonDocument>();
            var collection = GetCollection(collectionName);
            //   collection.Find(bson).Sort(sort).ForEachAsync(x => list.Add(x)).Wait();

            //   collection.Find(bson).ForEachAsync(x => list.Add(x)).Wait();


            //  db.test.find({ xxx...xxx}).sort({ "amount":1}).skip(10).limit(10)//这里忽略掉查询语句

              list = collection.Find(bson).Sort(sort).Skip(100000).Limit(500).ToList();

         
    





            //var query = Query<Http_Mod>.GT(item => item., 2399927);
            //var result = collection.Find(query).SetLimit(100)
            //                       .SetSortOrder(SortBy.Ascending("amount")).ToList();
            //Console.WriteLine(result.First().ToJson());//BSON自带的ToJson



          //  list = collection.Find(bson).Sort(sort).Limit(500).ToList();


          //  db.test.find({ amount: {$gt: 2399927} }).sort({ "amount":1}).limit(10)  //53ms










            return list;





        }

        public static void RR(string collectionName)
        {
            IMongoCollection<BsonDocument> collection= Database.GetCollection<BsonDocument>(collectionName);


        

       

            //  BsonDocument sddd;

          


            //
            // 摘要:
            //     Begins a fluent find interface.
            //
            // 参数:
            //   collection:
            //     The collection.
            //
            //   filter:
            //     The filter.
            //
            //   options:
            //     The options.
            //
            // 类型参数:
            //   TDocument:
            //     The type of the document.
            //
            // 返回结果:
            //     A fluent find interface.
            //public static IFindFluent<TDocument, TDocument> Find<TDocument>(this IMongoCollection<TDocument> collection, FilterDefinition<TDocument> filter, FindOptions options = null);



            // collection.Find

            //  var vdd = collection.Find<BsonDocument>(bson).Sort(sort);

        }














       

    }

    //  TDocument

    public class Test
    {
        /// <summary>
        /// 主键 ObjectId 是MongoDB自带的主键类型
        /// </summary>
        public ObjectId Id { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        [MongoDB.Bson.Serialization.Attributes.BsonElement("cust_id")]
        public string CustomerId { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        [BsonElement("amount")]
        public int Amount { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [BsonElement("status")]
        public string Status { get; set; }
    }

    public class TDocument : FilterDefinition<SDocument>
    {
        public override BsonDocument Render(IBsonSerializer<SDocument> documentSerializer, IBsonSerializerRegistry serializerRegistry)
        {
         
           
            throw new NotImplementedException();
        }
    }
    public class SDocument
    {

        public Int64 LID { get; set; }

     
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
