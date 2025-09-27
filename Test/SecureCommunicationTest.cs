using Common.Exceptioons.SecureExceptions;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Configuration;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class SecureCommunicationTest
    {
        private Master.Communication.SecureCommunication secureMasterCommunication;
        private Slave.Communication.SecureCommunication secureSlaveCommunication;

        [SetUp]
        public void Setup()
        {
            secureMasterCommunication = new Master.Communication.SecureCommunication();
            secureSlaveCommunication = new Slave.Communication.SecureCommunication();
        }

        [Test, Timeout(10000)]
        public async Task SecureStream_BothSideDoingAuthentication_SuccessfullAuth()
        {
            //Arrange
            ConfigurationManager.AppSettings.Set("CA_ThumbPrintSlave", "0a76b16c5eff2e78155776f439b603ce73617f0b");
            ConfigurationManager.AppSettings.Set("CA_ThumbPrintMaster", "8565dae6c27502af7ec18e856c41b63c5a026306");

            TcpClient client = new TcpClient();
            TcpListener tcpListener = new TcpListener(System.Net.IPAddress.Loopback, 8002);

            tcpListener.Start(1);
            client.Connect("127.0.0.1", 8002);

            TcpClient acceptedClient = await tcpListener.AcceptTcpClientAsync();

            Stream clientStream = client.GetStream();
            Stream serverStream = acceptedClient.GetStream();

            //Act
            SslStream sslServer = null;
            Task secureSlave = secureSlaveCommunication.SecureStream(serverStream).ContinueWith(t => { sslServer = (SslStream)t.Result; });
            SslStream sslClient = (await secureMasterCommunication.SecureStream(clientStream)) as SslStream;

            //Assert
            ClassicAssert.NotNull(sslClient);
            ClassicAssert.NotNull(sslServer);
            ClassicAssert.IsTrue(sslClient.IsMutuallyAuthenticated);
            ClassicAssert.IsTrue(sslServer.IsMutuallyAuthenticated);

            client.Close();
            tcpListener.Stop();
        }

        [Test, Timeout(10000)]
        public async Task SecureStream_NoCertificates_ShouldThrowAuthFailed()
        {
            //Arrange
            ConfigurationManager.AppSettings.Set("CA_ThumbPrintSlave", "fakeThumb");
            ConfigurationManager.AppSettings.Set("CA_ThumbPrintMaster", "fakeThumb");

            TcpClient client = new TcpClient();
            TcpListener tcpListener = new TcpListener(System.Net.IPAddress.Loopback, 8002);

            tcpListener.Start(1);
            client.Connect("127.0.0.1", 8002);

            TcpClient acceptedClient = await tcpListener.AcceptTcpClientAsync();
            Stream clientStream = client.GetStream();
            Stream serverStream = acceptedClient.GetStream();

            //Act & Assert
            Assert.ThrowsAsync<AuthenticationFailedException>(async () => await secureSlaveCommunication.SecureStream(clientStream));
            Assert.ThrowsAsync<AuthenticationFailedException>(async () => await secureSlaveCommunication.SecureStream(serverStream));

            client.Close();
            tcpListener.Stop();
        }

        [Test, Timeout(10000)]
        public async Task SecureStream_ServerIsNotSecured_ShouldThrowAuthFailed()
        {
            //Arrange
            ConfigurationManager.AppSettings.Set("CA_ThumbPrintSlave", "0a76b16c5eff2e78155776f439b603ce73617f0b");
            ConfigurationManager.AppSettings.Set("CA_ThumbPrintMaster", "8565dae6c27502af7ec18e856c41b63c5a026306");

            TcpClient client = new TcpClient();
            TcpListener tcpListener = new TcpListener(System.Net.IPAddress.Loopback, 8002);

            tcpListener.Start(1);
            client.Connect("127.0.0.1", 8002);

            TcpClient acceptedClient = await tcpListener.AcceptTcpClientAsync();

            Stream clientStream = client.GetStream();
            Stream serverStream = acceptedClient.GetStream();

            Assert.ThrowsAsync<AuthenticationFailedException>(async () => await secureSlaveCommunication.SecureStream(clientStream));

            client.Close();
            tcpListener.Stop();
        }

        [Test, Timeout(10000)]
        public async Task SecureStream_ClientIsNotSecured_ShouldThrowAuthFailed()
        {
            //Arrange
            ConfigurationManager.AppSettings.Set("CA_ThumbPrintSlave", "0a76b16c5eff2e78155776f439b603ce73617f0b");
            ConfigurationManager.AppSettings.Set("CA_ThumbPrintMaster", "8565dae6c27502af7ec18e856c41b63c5a026306");

            TcpClient client = new TcpClient();
            TcpListener tcpListener = new TcpListener(System.Net.IPAddress.Loopback, 8002);

            tcpListener.Start(1);
            client.Connect("127.0.0.1", 8002);

            TcpClient acceptedClient = await tcpListener.AcceptTcpClientAsync();

            Stream clientStream = client.GetStream();
            Stream serverStream = acceptedClient.GetStream();

            //Act & Asser
            Assert.ThrowsAsync<AuthenticationFailedException>(async () => await secureSlaveCommunication.SecureStream(serverStream));

            client.Close();
            tcpListener.Stop();
        }
    }
}
