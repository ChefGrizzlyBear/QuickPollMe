using System;

namespace QuickPollDotMe.DAL.Daos
{
    public class PollDao
    {
        public int PollId {get; set;}
        
        public string Question {get; set;}
        
        public DateTime CreatedDateTime{get;set;}
    }
}
