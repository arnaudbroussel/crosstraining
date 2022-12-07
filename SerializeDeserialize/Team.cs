using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace crosstraining.SerializeDeserialize {

    public class Team {
        public Team() {
            this.Members = new List<Person>();
            this.AutomaticCode = ValuesHelper.RandomString(10);
        }

        public string Name { get; set; }
        public List<Person> Members { get; set; }

        [JsonProperty(PropertyName = "TeamLeader")]
        public Person TlName { get; set; }

        [JsonPropertyNameAttribute("LeaderOfTheTeamLeader")]
        public Person SuperTlName { get; set; }

        [JsonProperty]
        private string AutomaticCode { get; set; }

        [NonSerialized]
        public string Nickname = "Hey Ho Let's Go !!";
    }
}