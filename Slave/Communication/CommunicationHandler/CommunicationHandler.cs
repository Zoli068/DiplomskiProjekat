using Common;
using Common.Communication;
using Common.CommunicationExceptions;
using Common.SecureExceptions;
using Common.StateHandler;
using Common.TaskHandler;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Slave.Communication
{
    /// <summary>
    /// The <see cref="CommunicationHandler"/> class handling the communication (<see cref="ICommunicationStream"/>), creates the specified object from 
    /// the recived bytes, also converting our specified object to a byte array to be able to send it
    /// </summary>
    public class CommunicationHandler
    {
        private TaskHandler reciverTask;
        private TaskHandler sendingTask;
        private TaskHandler acceptingConnectionTask;

        private Action<byte[]> raiseRecivedBytes;
        private byte[] currentlySendingBytes;
        private CancellationTokenSource cts = new CancellationTokenSource();
        public BlockingCollection<byte[]> dataToSend = new BlockingCollection<byte[]>();

        private ICommunicationHandlerOptions options;
        private ICommunicationStream communicationStream;
        private IAsyncSecureCommunication secureCommunication;
        private IStateHandler<CommunicationState> stateHandler = new StateHandler<CommunicationState>();

        public CommunicationHandler(ICommunicationHandlerOptions communicationHandlerOptions, ICommunicationOptions communicationOptions, Action<byte[]> RaiseRecivedBytes)
        {
            this.raiseRecivedBytes = RaiseRecivedBytes;
            options = communicationHandlerOptions;

            stateHandler.StateChanged += connectionStateChanged;

            if (options.SecurityMode == SecurityMode.SECURE) { secureCommunication = new SecureCommunication(); }

            if (communicationOptions.CommunicationType == CommunicationType.TCP)
                communicationStream = new TcpCommunicationStream(communicationOptions as ITcpCommunicationOptions);

            acceptingConnectionTask = new TaskHandler(startAcceptingConnections, false, 0, cts);
            reciverTask = new TaskHandler(recivingData, false, 0, cts);
            sendingTask = new TaskHandler(sendingData, false, 0, cts);

            acceptingConnectionTask.TaskShouldContinue();
        }

        private async Task startAcceptingConnections()
        {
            try
            {
                await communicationStream.Accept();

                if (options.SecurityMode == SecurityMode.SECURE)
                    communicationStream.Stream = await secureCommunication.SecureStream(communicationStream.Stream);

                stateHandler.ChangeState(CommunicationState.CONNECTED);
            }
            catch (Exception ex) when (ex is UnsuccessfullConnectionException || ex is AuthenticationFailedException)
            {
                Console.WriteLine(ex.Message);
                communicationStream.Close();
            }
        }

        private async Task sendingData()
        {
            try
            {
                if (currentlySendingBytes == null)
                    currentlySendingBytes = dataToSend.Take();

                if (stateHandler.State != CommunicationState.CONNECTED)
                    return;

                await communicationStream.Send(currentlySendingBytes);

                currentlySendingBytes = null;
            }
            catch (Exception ex) when (ex is ConnectionErrorException || ex is ConnectionNotExisting)
            {
                Console.WriteLine(ex.Message);
                stateHandler.ChangeState(CommunicationState.DISCONNECTED);
            }
            catch (Exception ex) when (ex is ObjectDisposedException) { }; ;
        }

        private async Task recivingData()
        {
            byte[] recivedData;

            try
            {
                recivedData = await communicationStream.Receive();
                raiseRecivedBytes(recivedData);
            }
            catch (Exception ex) when (ex is ConnectionErrorException || ex is ConnectionNotExisting)
            {
                Console.WriteLine(ex.Message);
                stateHandler.ChangeState(CommunicationState.DISCONNECTED);
            }
            catch (Exception ex) when (ex is ObjectDisposedException) { };
        }

        public void connectionStateChanged()
        {

            Console.WriteLine("Connection changed to: " + stateHandler.State);

            if (stateHandler.State == CommunicationState.CONNECTED)
            {
                acceptingConnectionTask.TaskShouldWait();
                sendingTask.TaskShouldContinue();
                reciverTask.TaskShouldContinue();
            }
            else
            {
                sendingTask.TaskShouldWait();
                reciverTask.TaskShouldWait();

                communicationStream.Close();
                acceptingConnectionTask.TaskShouldContinue();
            }
        }

        public void Dispose()
        {
            if (cts != null) { cts.Cancel(); }

            if (dataToSend != null) { dataToSend.Dispose(); }

            if (communicationStream != null) { communicationStream.Dispose(); }

            if (sendingTask != null) { sendingTask.DeleteTask(); }

            if (reciverTask != null) { reciverTask.DeleteTask(); }

            if (acceptingConnectionTask != null) { acceptingConnectionTask.DeleteTask(); }
        }
    }
}
