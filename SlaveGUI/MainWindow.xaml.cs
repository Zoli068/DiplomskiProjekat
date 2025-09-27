using Common.Communication;
using Common.CommunicationExceptions;
using Common.FIFOQueue;
using Common.FileRecord;
using Common.Message;
using Common.PointsDataBase;
using Slave.Communication;
using Slave.Message;
using System;
using System.Linq;
using System.Windows;

namespace SlaveGUI
{
    public partial class MainWindow : Window
    {
        ICommunication communication;
        IMessageProcesser<FunctionCode> messageProcesser;
        PointsDataBase db = new PointsDataBase();
        IFileRecord fileRecord = new FileRecord();
        IFIFOQueue fIFOQueue = new FIFOQueue();

        public MainWindow()
        {
            InitializeComponent();
            Start();
            db.DataChanged +=RefreshTables;
            RefreshTables(); 
        }

        private void RefreshTables() 
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                RegistersDataGrid.ItemsSource = null;
                RegistersDataGrid.ItemsSource = db.RegistersDictionary
                    .Select(kvp => new
                    {
                        Address = kvp.Key,
                        Type = kvp.Value.Item1,
                        Value = kvp.Value.Item2
                    })
                    .ToList();

                DiscretesDataGrid.ItemsSource = null;
                DiscretesDataGrid.ItemsSource = db.DiscreteDictionary
                    .Select(kvp => new
                    {
                        Address = kvp.Key,
                        Type = kvp.Value.Item1,
                        Value = kvp.Value.Item2
                    })
                    .ToList();
            }));
        }

        private void Start()
        {
            try
            {
                communication = new Communication();
                messageProcesser = new ModbusMessageProcesser(db, fileRecord, fIFOQueue, communication);
            }
            catch (Exception ex) when (ex is ListeningNotSuccessedException)
            {
                Console.WriteLine("The server was not able to start");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unknown error happened");
                Console.WriteLine(ex.Message);
            }
        }
    }
}