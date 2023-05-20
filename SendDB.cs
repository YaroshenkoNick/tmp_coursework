using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace goldbach
{
    internal class SendDB
    {
        public void SendException(Exception ex)
        {
            using (GoldbachContext db = new GoldbachContext())
            {
                UserException exception = new UserException
                {
                    Message = ex.Message,
                    TargetSite = ex.TargetSite.ToString(),
                    DateTimeExc = DateTime.Now
                };
                db.UserExceptions.AddRange(exception);
                db.SaveChanges();
            }
        }
        public void SendRunning(string mode, string input, string output)
        {
            using (GoldbachContext db = new GoldbachContext())
            {
                Running running = new Running
                {
                    Mode = mode,
                    Input = input,
                    Output = output,
                    DateTime = DateTime.Now
                };
                db.Runnings.AddRange(running);
                db.SaveChanges();
            }
        }

        public string RTBToString(RichTextBox textBox)
        {
            string text = textBox.Text;
            string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string result = string.Join(",", lines);
            return result;
        }
    }
}
