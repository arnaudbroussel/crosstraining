using System;
using System.Collections.Generic;

namespace crosstraining.SerializeDeserialize {
    public class JsonNet {

        public static void Serialize() {
            Team team = WorkWithSerialize.MakeOneTeam();

            var options = new System.Text.Json.JsonSerializerOptions {
                WriteIndented = true,
            };


            string json1 = Newtonsoft.Json.JsonConvert.SerializeObject(team, Newtonsoft.Json.Formatting.Indented);
            string json2 = System.Text.Json.JsonSerializer.Serialize(team, options);

            Console.WriteLine(json1);
            Console.WriteLine(json2);
        }

        public static void SerializePerformanceTesting() {
            var options = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };

            List<Team> teams = new List<Team>();
            for (int i = 0; i < 1000000; i++) {
                teams.Add(WorkWithSerialize.MakeOneTeam());
            }

            var startTime = DateTime.Now;
            var newtonsoft = Newtonsoft.Json.JsonConvert.SerializeObject(teams);
            Console.WriteLine(String.Format("Serialize Newtonsoft {0} ms", (int) (DateTime.Now - startTime).TotalMilliseconds));

            startTime = DateTime.Now;
            Newtonsoft.Json.JsonConvert.SerializeObject(teams, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(String.Format("Serialize Newtonsoft {0} ms (pretty json)", (int) (DateTime.Now - startTime).TotalMilliseconds));

            startTime = DateTime.Now;
            var netCore = System.Text.Json.JsonSerializer.Serialize(teams);
            Console.WriteLine(String.Format("Serialize System.Text.Json {0} ms", (int) (DateTime.Now - startTime).TotalMilliseconds));

            startTime = DateTime.Now;
            System.Text.Json.JsonSerializer.Serialize(teams, options);
            Console.WriteLine(String.Format("Serialize System.Text.Json {0} ms (pretty json)", (int) (DateTime.Now - startTime).TotalMilliseconds));

            startTime = DateTime.Now;
            List<Team> teamsNetCore = System.Text.Json.JsonSerializer.Deserialize<List<Team>>(netCore);
            Console.WriteLine(String.Format("Deserialize System.Text.Json {0} ms", (int) (DateTime.Now - startTime).TotalMilliseconds));
            
            startTime = DateTime.Now;
            List<Team> teamsNewtonsoft = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Team>>(newtonsoft);
            Console.WriteLine(String.Format("Deserialize Newtonsoft {0} ms", (int) (DateTime.Now - startTime).TotalMilliseconds));
        }
    }
}