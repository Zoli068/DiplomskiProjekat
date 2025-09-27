using Common;
using Common.CommunicationExceptions;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class MasterCommunicationTest
    {
        private Master.Communication.TcpCommunicationOptions options;
        private Master.Communication.TcpCommunicationStream tcpCommunicationStream;
        private Slave.Communication.TcpCommunicationStream tcpServerCommunicationStream;
        private CancellationTokenSource cancellationTokenSource;

        [SetUp]
        public async void Setup()
        {
            cancellationTokenSource = new CancellationTokenSource();

            tcpServerCommunicationStream = new Slave.Communication.TcpCommunicationStream(new Slave.Communication.TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP, 8192));
            Task serverTask = Task.Run(async () =>
            {
                while (true)
                {
                    await tcpServerCommunicationStream.Accept();
                }
            });
        }

        [Test, Timeout(10000)]
        public async Task Connect_ValidParam_SuccessfullConnection()
        {
            //Arrange
            options = new Master.Communication.TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP, 5000, 8192);
            tcpCommunicationStream = new Master.Communication.TcpCommunicationStream(options);

            // Act
            await tcpCommunicationStream.Connect();

            // Assert
            ClassicAssert.IsNotNull(tcpCommunicationStream.Stream);
            ClassicAssert.IsTrue(tcpCommunicationStream.Stream.CanRead);
        }

        [Test, Timeout(10000)]
        public void Connect_BadParam_ThrowsUnsuccessfullConnectionException()
        {
            //Arrange
            options = new Master.Communication.TcpCommunicationOptions(IPAddress.Loopback, 8001, CommunicationType.TCP, 5000, 8192);
            tcpCommunicationStream = new Master.Communication.TcpCommunicationStream(options);

            // Act & Assert

            Assert.ThrowsAsync<UnsuccessfullConnectionException>(async () => await tcpCommunicationStream.Connect());
        }

        [Test, Timeout(10000)]
        public async Task Connect_AlreadyConnected_ThrowsConnectionAlreadyExisting()
        {
            // Arrange
            options = new Master.Communication.TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP, 5000, 8192);
            tcpCommunicationStream = new Master.Communication.TcpCommunicationStream(options);
            await tcpCommunicationStream.Connect();

            // Act & Assert
            Assert.ThrowsAsync<ConnectionAlreadyExisting>(async () => await tcpCommunicationStream.Connect());
        }

        [Test, Timeout(10000)]
        public async void Receive_WithoutConnection_ThrowsConnectionNotExisting()
        {
            //Arrange
            options = new Master.Communication.TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP, 2000, 8192);
            tcpCommunicationStream = new Master.Communication.TcpCommunicationStream(options);

            //Act
            await tcpServerCommunicationStream.Send(new byte[] { 1, 2, 3 });
            Assert.ThrowsAsync<ConnectionNotExisting>(async () => await tcpCommunicationStream.Receive());
        }


        [Test, Timeout(10000)]
        public async void Receive_WithConnection_RecivesData()
        {
            //Arrange
            byte[] sendingdata = new byte[] { 1, 2, 3 };
            byte[] receiveingdata = null;
            options = new Master.Communication.TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP, 2000, 8192);
            tcpCommunicationStream = new Master.Communication.TcpCommunicationStream(options);
            await tcpCommunicationStream.Connect();
            //Act

            await tcpServerCommunicationStream.Send(sendingdata);
            Assert.DoesNotThrow(async () => receiveingdata = await tcpCommunicationStream.Receive());
            CollectionAssert.AreEqual(sendingdata, receiveingdata);
        }

        [Test, Timeout(10000)]
        public void Send_WithoutConnection_ThrowsConnectionNotExisting()
        {
            //Arrange
            options = new Master.Communication.TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP, 2000, 8192);
            tcpCommunicationStream = new Master.Communication.TcpCommunicationStream(options);

            //Act
            Assert.ThrowsAsync<ConnectionNotExisting>(async () => await tcpCommunicationStream.Send(new byte[10]));
        }

        [Test, Timeout(10000)]
        public async Task Close_ClosesClientAndStream()
        {
            //Arrange
            options = new Master.Communication.TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP, 5000, 8192);
            tcpCommunicationStream = new Master.Communication.TcpCommunicationStream(options);

            // Act
            await tcpCommunicationStream.Connect();
            tcpCommunicationStream.Close();

            // Assert
            ClassicAssert.IsNull(tcpCommunicationStream.Stream);
            Assert.ThrowsAsync<ConnectionNotExisting>(async () => await tcpCommunicationStream.Send(new byte[] { 1, 2, 3 }));
            Assert.ThrowsAsync<ConnectionNotExisting>(async () => await tcpCommunicationStream.Receive());
        }

        [Test, Timeout(10000)]
        public async Task Send_WithConnectedClient_SendsData()
        {
            // Arrange
            options = new Master.Communication.TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP, 5000, 8192);
            tcpCommunicationStream = new Master.Communication.TcpCommunicationStream(options);
            await tcpCommunicationStream.Connect();
            var testData = new byte[] { 1, 2, 3, 4 };

            // Act & Assert (Ensures no exception is thrown)
            Assert.DoesNotThrowAsync(async () => await tcpCommunicationStream.Send(testData));
        }

        [TearDown]
        public void Cleanup()
        {
            tcpCommunicationStream.Close();
            tcpCommunicationStream = null;
            options = null;
            cancellationTokenSource.Cancel();
            tcpServerCommunicationStream.Close();
        }

    }
}

