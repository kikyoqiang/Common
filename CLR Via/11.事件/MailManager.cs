using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLR_Via
{
    public class MailManager
    {
        public event EventHandler<NewMailEventArgs> NewMail;
        protected virtual void OnNewMail(NewMailEventArgs e)
        {
            e.Raise(this, ref NewMail);
            //EventHandler<NewMailEventArgs> temp = Volatile.Read(ref NewMail);
            //temp?.Invoke(this, e);
        }
        public void SimulateNewMail(string form, string to, string subject)
        {
            NewMailEventArgs e = new NewMailEventArgs(form, to, subject);
            OnNewMail(e);
        }
    }
    public class NewMailEventArgs : EventArgs
    {
        private readonly string m_from, m_to, m_subject;
        public NewMailEventArgs(string from, string to, string subject)
        {
            m_from = from; m_to = to; m_subject = subject;
        }
        public string From { get { return m_from; } }
        public string To { get { return m_to; } }
        public string Subject { get { return m_subject; } }
    }
    public sealed class Fax
    {
        public Fax(MailManager mm)
        {

        }
        private void FaxMsg(object sender, NewMailEventArgs e)
        {
            Console.WriteLine("FaxMsg mail message");
            Console.WriteLine("From={0},To={1},Subject={2}", e.From, e.To, e.Subject);
        }
        public void UnRegister(MailManager mm)
        {
            mm.NewMail -= FaxMsg;
        }
    }
}
