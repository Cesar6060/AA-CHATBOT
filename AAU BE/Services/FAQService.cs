using AAU_BE.Database;
using DefaultNamespace;

namespace AAU_BE.Models;
using System.Collections.Generic;
using System.Data;

public class FaqService
{
    private readonly string _connectionString;
    
    
    public bool DeleteFaq(int id)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "id", id },
        };
        if(Database.SQLDB.DoSqlCommand(Queries.DELETEFAQ, bindparams) > 0)
        {
            return true;
        }
        return false;
    }

    public bool InsertFaq(FAQ faq)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "question", faq.question },
            { "answer", faq.answer },
        };
        if(Database.SQLDB.DoSqlCommand(Queries.INSERTFAQ, bindparams) > 0)
        {
            return true;
        }
        return false;
    }
    public bool UpdateFaQ(FAQ faq)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "id", faq.id },
            { "question", faq.question },
            { "answer", faq.answer },
        };
        if(Database.SQLDB.DoSqlCommand(Queries.UPDATEFAQ, bindparams) > 0)
        {
            return true;
        }
        return false;
    }

    public int GetFAQID(FAQ faq)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "question", faq.question }
        };
        DataTable dT = SQLDB.FetchDataTable(Queries.ALLFAQ);
        return Convert.ToInt32(dT.Rows[0]["id"]);
    }
 
        public List<FAQ> GetAllFAQS()
        {
            DataTable dT = SQLDB.FetchDataTable(Queries.ALLFAQ);
            List<FAQ> faqs = new List<FAQ>();
            foreach (DataRow row in dT.Rows)
            {
                FAQ faq = new FAQ()
                {
                    id = Convert.ToInt32(row["id"]),
                    question = row["Question"].ToString(),
                    answer = row["Answer"].ToString(),
                };

                faqs.Add(faq);
            }
            return faqs;
    }
}