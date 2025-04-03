using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace crosstraining.xml
{
    public class HandleXml
    {
        public static void TestXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("./xml/newbooks.xml");

            // Create an XmlNamespaceManager to resolve the default namespace.
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", "urn:newbooks-schema");

            // Select the first book written by an author whose last name is Atwood.
            XmlNode book;
            XmlElement root = doc.DocumentElement;
            book = root.SelectSingleNode("descendant::bk:book[bk:author/bk:last-name='Atwood']", nsmgr);

            Console.WriteLine(book.OuterXml);
        }

        public static void TestXmlHrmc_v0()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("./xml/hrmc.xml");

            // Create an XmlNamespaceManager to resolve the default namespace.
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", "http://www.govtalk.gov.uk/taxation/CTF");

            // Select the first book written by an author whose last name is Atwood.
            XmlNode book;
            XmlElement root = doc.DocumentElement;
            book = root.SelectSingleNode("descendant::bk:DefaultCurrency", nsmgr);

            var book2 = root.SelectNodes("descendant::bk:Uncompressed", nsmgr);

            ////book = root.SelectSingleNode("/Body", nsmgr);
            ////book = book.SelectSingleNode("/IRenvelope", nsmgr);
            ////book = book.SelectSingleNode("/TestMessage", nsmgr);
            ////book = book.SelectSingleNode("/Keys", nsmgr);
            ////book = book.SelectSingleNode("/PeriodEnd", nsmgr);
            ////book = book.SelectSingleNode("/DefaultCurrency", nsmgr);

            Console.WriteLine(book.OuterXml);
        }

        public static void TestXmlHrmc_v1()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"./xml/contacts.xml");

            XmlNodeList listOfContacts = doc.SelectNodes("/Contacts/Contact");

            foreach (XmlNode singleContact in listOfContacts) 
            {
                Console.WriteLine("aqui");
            }
        }

        public static void TestXmlHrmc()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"./xml/hrmc.xml");

            var x = doc.InnerXml;
            x= x.Replace("{{partnershiptaxutpr}}", "123456789");
            x= x.Replace("{{constpartnershipaccountingperiodend_datedash}}", "2024-02-03");
        }
    }
}