using WifiConnect.Wifi.URI.Parser;

namespace WifiConnectTest
{
    [TestClass]
    public class WifiUriParserTest
    {
        [DataRow("WIFI:S:myssid;T:nopass;;")]
        [DataRow("WIFI:S:myssid;T:WPA;P:mypass;;")]
        [DataRow("WIFI:S:myssid;T:WPA;P:false;;")]
        [DataTestMethod]
        public void Test_Parse(string uri)
        {
            _ = new WifiUriParser().Parse(uri);
        }

        [DataRow("WIFI:S:my\\:ssid;T:nopass;;")]
        [DataRow("WIFI:S:my\\;ssid;T:nopass;;")]
        [DataRow("WIFI:S:my\\\"ssid;T:nopass;;")]
        [DataRow("WIFI:S:my\\,ssid;T:nopass;;")]
        [DataRow("WIFI:S:my\\ssid;T:nopass;;")]
        [DataTestMethod]
        public void Test_Parse_withEscapedCharacters(string uri)
        {
            _ = new WifiUriParser().Parse(uri);
        }

        [DataRow("WIFI:S:myssid;T:nopass;")]
        [DataRow("WIFI:P:mypass;;")]
        [DataRow("wifi:S:myssid;P:mypass;;")]
        [DataTestMethod]
        public void Test_Parse_withWrongStructure(string uri)
        {
            WifiUriParser parser = new();
            _ = Assert.ThrowsException<InvalidFormatException>(() => parser.Parse(uri));
        }
    }
}
