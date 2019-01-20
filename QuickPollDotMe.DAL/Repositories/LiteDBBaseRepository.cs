using System;
using LiteDB;

namespace QuickPollDotMe.DAL.Repositories
{
    public class LiteDBBaseRepository
    {
        public LiteDatabase DB {get;set;}
        public LiteDBBaseRepository()
        {
            DB = new LiteDatabase(@"MyData.db");
        }
    }
}
