using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace crosstraining.SerializeDeserialize {
    public class WorkWithSerialize {

        public static void SerializeObjectAndRestoreFromXML() {
            ////SerializeOnePerson();
            SerializeOneTeam();
            ////SerializeBinary();
        }

        private static void SerializeOnePerson() {
            XmlSerializer serializer = new XmlSerializer(typeof(Person));
            string xml;
            using (StringWriter stringWriter = new StringWriter()) {
                Person p = new Person {
                    FirstName = "John",
                    LastName = "Doe",
                    Age = 42
                };
                serializer.Serialize(stringWriter, p);
                xml = stringWriter.ToString();
            }
            Console.WriteLine(xml);
            Console.WriteLine();
            using (StringReader stringReader = new StringReader(xml)) {
                Person p = (Person) serializer.Deserialize(stringReader);
                Console.WriteLine("{0} {1} is {2} years old", p.FirstName, p.LastName, p.Age);
            }
        }

        private static void SerializeOneTeam() {
            XmlSerializer serializer = new XmlSerializer(typeof(Team));            
            string xml;
            using (StringWriter stringWriter = new StringWriter()) {
                Team team = MakeOneTeam();

                serializer.Serialize(stringWriter, team);
                xml = stringWriter.ToString();
            }
            Console.WriteLine(xml);
            Console.WriteLine();
            using StringReader stringReader = new StringReader(xml);
            Team t = (Team) serializer.Deserialize(stringReader);
            foreach (var p in t.Members) {
                Console.WriteLine("{0} {1} is {2} years old", p.FirstName, p.LastName, p.Age);
            }
        }

        public static Team MakeOneTeam() {
            Team team = new Team {
                Name = "PlayTheBlues",
                TlName = new Person { FirstName = "Bob", LastName = "Dylan", Age = 30 },
                SuperTlName = new Person { FirstName = "Robert", LastName = "Johnson", Age = 40 },
            };

            team.Members.Add(new Person { FirstName = "Lou", LastName = "Barlow", Age = 45 });
            team.Members.Add(new Person { FirstName = "Alex", LastName = "Chilton", Age = 42 });
            team.Members.Add(new Person { FirstName = "Damien", LastName = "Jurado", Age = 41 });

            return team;
        }

        private static void SerializeBinary() {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream("data.bin", FileMode.Create)) {
                formatter.Serialize(stream, MakeOneTeam());
            }
            using (Stream stream = new FileStream("data.bin", FileMode.Open)) {
                Team team = (Team) formatter.Deserialize(stream);
                foreach (var p in team.Members) {
                    Console.WriteLine("{0} {1} is {2} years old", p.FirstName, p.LastName, p.Age);
                }
            }
        }
    }
}