using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Select_MongoDb
{
    public class Config
    {

        public readonly static string MongoDbConnStr = ConfigurationManager.AppSettings["MongoDbConnStr"];

        public readonly static string MongoDbDatabase = ConfigurationManager.AppSettings["MongoDbDatabase"];




    }
}
