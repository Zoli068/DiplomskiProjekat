using Common.Communication;
using Common.CommunicationExceptions;
using Common.FIFOQueue;
using Common.FileRecord;
using Common.IPointsDataBase;
using Common.Message;
using Common.PointsDataBase;
using Slave.Communication;
using Slave.Message;
using SlaveGUI.GUIElements;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SlaveGUI
{
    public partial class MainWindow : Window
    {
        private ICommunication communication;
        private PointsDataBase db = new PointsDataBase();
        private IFileRecord fileRecord = new FileRecord();
        private IFIFOQueue fIFOQueue = new FIFOQueue();
        private IMessageProcesser<FunctionCode> messageProcesser;

        private PointsType typeToUpdate;
        private ushort addrressToUpdate;

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

        private void UpdateValue_Click(object sender, RoutedEventArgs e)
        {
                InputDialog inputDialog;
            
                if(typeToUpdate==PointsType.INPUT_REGISTERS || typeToUpdate == PointsType.HOLDING_REGISTERS)
                {
                    inputDialog = new InputDialog("Enter the register value", typeof(short));
                }
                else
                {
                    (PointsType, byte) tmp;
                    
                    if(db.DiscreteDictionary.TryGetValue(addrressToUpdate, out tmp))
                    {
                        if(tmp.Item2 == 0)
                        {
                        
                            db.DiscreteDictionary[addrressToUpdate] = (tmp.Item1,1);
                            db.OnDataChanged();
                        }
                        else
                        {
                            db.DiscreteDictionary[addrressToUpdate] = (tmp.Item1,0);
                            db.OnDataChanged();
                        }   
                    }

                    addrressToUpdate = 0;

                    return;
                }

                inputDialog.Owner = this;
                
                if (inputDialog.ShowDialog() == false)
                {
                    return;
                }

                if (short.TryParse(inputDialog.ResponseText, out short value))
                {
                    (PointsType, short) tmp;

                    if (db.RegistersDictionary.TryGetValue(addrressToUpdate, out tmp))
                    {
                        db.RegistersDictionary[addrressToUpdate] = (tmp.Item1, value);
                        db.OnDataChanged();
                    }
                }
                else
                {
                    MessageBox.Show("Error", "You entered invalid value for the register", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                addrressToUpdate = 0;
        }

        private void ContextMenu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (sender is DataGridRow row)
            {
                dynamic item = row.Item;
                addrressToUpdate = item.Address;
                typeToUpdate = item.Type;
            }
        }
    }
}