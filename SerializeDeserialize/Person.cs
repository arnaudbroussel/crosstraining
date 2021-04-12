using Newtonsoft.Json;
using System;

namespace crosstraining.SerializeDeserialize {
    [Serializable]
    public class Person {
        public Person() {
            this.AutomaticCode = ValuesHelper.RandomString(10);
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        
        [JsonProperty(PropertyName = "$autocode")]
        protected string AutomaticCode { get; set; }
    }
}