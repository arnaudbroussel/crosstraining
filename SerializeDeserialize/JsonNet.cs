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

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var newtonsoft = Newtonsoft.Json.JsonConvert.SerializeObject(teams);
            Console.WriteLine($"Serialize Newtonsoft {watch.ElapsedMilliseconds} ms");

            watch.Restart();
            Newtonsoft.Json.JsonConvert.SerializeObject(teams, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine($"Serialize Newtonsoft {watch.ElapsedMilliseconds} ms (pretty json)");

            watch.Restart();
            var netCore = System.Text.Json.JsonSerializer.Serialize(teams);
            Console.WriteLine($"Serialize System.Text.Json {watch.ElapsedMilliseconds} ms");

            watch.Restart();
            System.Text.Json.JsonSerializer.Serialize(teams, options);
            Console.WriteLine($"Serialize System.Text.Json {watch.ElapsedMilliseconds} ms (pretty json)");

            watch.Restart();
            List<Team> teamsNetCore = System.Text.Json.JsonSerializer.Deserialize<List<Team>>(netCore);
            Console.WriteLine($"Deserialize System.Text.Json {watch.ElapsedMilliseconds} ms");

            watch.Restart();
            List<Team> teamsNewtonsoft = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Team>>(newtonsoft);
            Console.WriteLine($"Deserialize Newtonsoft {watch.ElapsedMilliseconds} ms");

            watch.Stop();
        }
    }
}