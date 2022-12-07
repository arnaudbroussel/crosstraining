namespace crosstraining.Network {
    using crosstraining.SerializeDeserialize;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Security.Cryptography;
    using System.Text;

    public class IPTools {

        public void GetIP() {
            var myPI = LocalIPAddress();
            System.Console.WriteLine(myPI.ToString());

            var team = WorkWithSerialize.MakeOneTeam();
            System.Console.WriteLine(this.ComputeSha256Hash(team.ToString()));
        
        }

        private static IPAddress LocalIPAddress() {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()) {
                return null;
            }

            var host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        private string ComputeSha256Hash(string rawData) {
            // Create a SHA256   
            using SHA256 sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            var builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++) {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}