using AE.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortMessage
{
    class Program
    {
        const string folderTest = "Lesson/Test";
        static void Main(string[] args)
        {
            var emails = new[] { "WHICH EMAILS" };
            var client = new ImapClient("imap.gmail.com", "YOUR EMAIL","YOUR PASSWORD",AuthMethods.Login, 993,true);
            client.SelectMailbox("INBOX");
            var countofmessages = client.GetMessageCount();
            var mailBox = client.Examine(folderTest);
            if (mailBox == null)
            {
                client.CreateMailbox(folderTest);
            }
            client.SelectMailbox("INBOX");
            for (int i = 0; i <emails.Length; i++)
            {
                SearchCondition searchPatter = SearchCondition.From(emails[i]);
                var result = client.SearchMessages(searchPatter, false, false);
                foreach (var item in result)
                {
                    var msg = item.Value;
                    client.MoveMessage(msg.Uid, folderTest);
                    Console.WriteLine($"{msg.Uid}/ {msg.From}");
                }
            }
           
            Console.ReadKey();


            //using (var imap = new AE.Net.Mail.ImapClient(host, username, password, AE.Net.Mail.ImapClient.AuthMethods.Login, port, isSSL))
            //{
            //    var msgs = imap.SearchMessages(
            //      SearchCondition.Undeleted().And(
            //        SearchCondition.From("david"),
            //        SearchCondition.SentSince(new DateTime(2000, 1, 1))
            //      ).Or(SearchCondition.To("andy"))
            //    );

            //    Assert.AreEqual(msgs[0].Value.Subject, "This is cool!");

            //    imap.NewMessage += (sender, e) => {
            //        var msg = imap.GetMessage(e.MessageCount - 1);
            //        Assert.AreEqual(msg.Subject, "IDLE support?  Yes, please!");
            //    };
            //}
        }
    }
}
