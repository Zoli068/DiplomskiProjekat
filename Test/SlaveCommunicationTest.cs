using Common;
using Common.CommunicationExceptions;
using Common.Exceptioons.CommunicationExceptions;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class SlaveCommunicationTest
    {
        private Slave.Communication.TcpCommunicationOptions options;
        private Slave.Communication.TcpCommunicationStream tcpSlaveCommunication;

        [SetUp]
        public async void Setup()
        {
            options = new Slave.Communication.TcpCommunicationOptions(IPAddress.Loopback, 8001, CommunicationType.TCP, 8192);
            tcpSlaveCommunication = new Slave.Communication.TcpCommunicationStream(options);
        }


        [Test, Timeout(10000)]
        public void Constructor_ShouldThrowException_WhenListeningNotSuccessed()
        {

            Assert.ThrowsAsync<ListeningNotSuccessedException>(async () => { var tcpSlaveCommunication2 = new Slave.Communication.TcpCommunicationStream(options); });
        }

        [Test, Timeout(10000)]
        public async Task Accept_ShouldEstablishConnection_WhenClientConnects()
        {
            //Arrange
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", 8001);
            await tcpSlaveCommunication.Accept();

            //Assert
            ClassicAssert.IsTrue(client.Connected);
            ClassicAssert.NotNull(tcpSlaveCommunication.Stream);
        }

        [Test, Timeout(10000)]
        public void Send_ShouldThrowException_WhenNoConnectionExists()
        {
            //Arange
            tcpSlaveCommunication.Close();
            byte[] data = new byte[] { 1, 2, 3 };

            //Act & Assert
            Assert.ThrowsAsync<ConnectionNotExisting>(async () => await tcpSlaveCommunication.Send(data));
        }

        [Test, Timeout(10000)]
        public async Task Send_ShouldSendData_WhenConnectionIsEstablished()
        {
            //Arrange
            TcpClient client = new TcpClient();
            byte[] dataToSend = new byte[] { 1, 2, 3 };
            byte[] receiveBuffer = new byte[3];
            tcpSlaveCommunication.Close();

            //Act
            client.Connect("127.0.0.1", 8001);
            await tcpSlaveCommunication.Accept();
            await tcpSlaveCommunication.Send(dataToSend);
            await client.GetStream().ReadAsync(receiveBuffer, 0, 3);

            //Assert
            ClassicAssert.AreEqual(dataToSend, receiveBuffer);
        }

        [Test, Timeout(10000)]
        public async Task Receive_ShouldReceiveData_WhenDataIsAvailable()
        {
            //Arrange
            TcpClient client = new TcpClient();
            var message = "Hello, Client!";
            var dataToSend = Encoding.UTF8.GetBytes(message);
            var receivedData = new byte[dataToSend.Length];

            //Act
            client.Connect("127.0.0.1", 8001);
            await tcpSlaveCommunication.Accept();
            await client.GetStream().WriteAsync(dataToSend, 0, dataToSend.Length);
            var dataReceived = await tcpSlaveCommunication.Receive();

            //Assert
            ClassicAssert.AreEqual(dataToSend, dataReceived);
        }

        [Test, Timeout(10000)]
        public void Receive_ShouldThrowException_WhenNoConnectionExists()
        {
            //Arrange
            tcpSlaveCommunication.Close();

            //Assert
            Assert.ThrowsAsync<ConnectionNotExisting>(async () => await tcpSlaveCommunication.Receive());
        }

        [Test, Timeout(10000)]
        public void Close_ShouldReleaseAllConnection()
        {
            //Act
            tcpSlaveCommunication.Close();

            //Assert
            ClassicAssert.AreEqual(null, tcpSlaveCommunication.Stream);
        }

        [TearDown]
        public void ClosingTests()
        {
            tcpSlaveCommunication.Close();
        }
    }
}
