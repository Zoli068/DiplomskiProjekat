using Common.IPointsDataBase;
using Common.Message;
using Common.Utilities;
using Master;
using Master.CommandHandler.MessageInitiateHandler;
using Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse;
using MasterGUI.GUIElements;
using MasterGUI.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MasterGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Scada scada;
        private Point<Byte> coilsPoint=null;
        private Point<short> registersPoint=null;
        public PointsDatabase pointsDatabase = new PointsDatabase();

        public MainWindow()
        {
            scada = new Scada();

            InitializeComponent();

            DiscretesGrid.ItemsSource = pointsDatabase.Coils;
            RegistersGrid.ItemsSource = pointsDatabase.Registers;
            pointsDatabase.DataChanged += RefreshValues;
            scada.ResponseRecived += SubscriptionResponse;

            InitRead();
        }

        #region Handle Response

        public void SubscriptionResponse(FunctionCode functionCode, IMessageDTO responseDTO)
        {
            var fc = functionCode;
            var dto = responseDTO;

            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                HandleResponse(fc, responseDTO);
            }));
        }

        private void HandleResponse(FunctionCode functionCode, IMessageDTO responseDTO)
        {
            if (responseDTO == null)
                return;

            switch(functionCode)
            {
                case FunctionCode.ReadCoils:
                case FunctionCode.ReadDiscreteInputs:
                    ReadCoilsResponseDTO tempReadCoilsDTO= responseDTO as ReadCoilsResponseDTO;
                    for (int i = 0; i < tempReadCoilsDTO.Values.Length; i++)
                    {
                        pointsDatabase.UpdateCoil((ushort)(tempReadCoilsDTO.Address + i), tempReadCoilsDTO.Values[i]);
                    }
                    break;

                case FunctionCode.WriteSingleCoil:
                case FunctionCode.WriteMultipleCoils:
                    WriteCoilsResponseDTO writeCoilsResponseDTO= responseDTO as WriteCoilsResponseDTO;
                    for (int i = 0; i < writeCoilsResponseDTO.Values.Length; i++)
                    {
                        pointsDatabase.UpdateCoil((ushort)(writeCoilsResponseDTO.Address + i), writeCoilsResponseDTO.Values[i]);
                    }
                    break;

                case FunctionCode.ReadInputRegisters:
                case FunctionCode.ReadHoldingRegisters:
                    ReadRegistersResponseDTO tempReadRegDTO = responseDTO as ReadRegistersResponseDTO;
                    for (int i = 0; i < tempReadRegDTO.Values.Length; i++)
                    {
                           pointsDatabase.UpdateRegister((ushort)(tempReadRegDTO.Address+i), tempReadRegDTO.Values[i]);
                    }
                    break;

                case FunctionCode.WriteSingleRegister:
                case FunctionCode.WriteMultipleRegisters:
                    WriteRegistersResponseDTO writeRegistersResponseDTO= responseDTO as WriteRegistersResponseDTO;
                    for (int i = 0; i < writeRegistersResponseDTO.Values.Length; i++)
                    {
                        pointsDatabase.UpdateRegister((ushort)(writeRegistersResponseDTO.Address + i), writeRegistersResponseDTO.Values[i]);
                    }
                    break;

                case FunctionCode.ReadWriteMultipleRegisters:
                    ReadWriteRegistersResponseDTO readWriteRegistersResponseDTO= responseDTO as ReadWriteRegistersResponseDTO;

                    for (int i = 0; i < readWriteRegistersResponseDTO.ReadedValues.Length; i++)
                    {
                        pointsDatabase.UpdateRegister((ushort)(readWriteRegistersResponseDTO.ReadAddress + i), readWriteRegistersResponseDTO.ReadedValues[i]);
                    }

                    for (int i = 0; i < readWriteRegistersResponseDTO.WriteValues.Length; i++)
                    {
                        pointsDatabase.UpdateRegister((ushort)(readWriteRegistersResponseDTO.WriteAddress + i), readWriteRegistersResponseDTO.WriteValues[i]);
                    }

                    break;
            }

            if (((byte)functionCode & 0x80) != 0){
                ErrorResponseDTO errorResponseDTO = responseDTO as ErrorResponseDTO;
                MessageBox.Show( "Error while processing the last request, Error Code:"+errorResponseDTO.ErrorCode+" , Exception Code:"+errorResponseDTO.ExceptionCode, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RefreshValues()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                DiscretesGrid.ItemsSource = null;
                DiscretesGrid.ItemsSource = pointsDatabase.Coils;

                RegistersGrid.ItemsSource = null;
                RegistersGrid.ItemsSource = pointsDatabase.Registers;
            }));
        }

        #endregion

        #region Read/Write Coils

        private void ContextMenu_DiscreteValuesContextMenuOpening(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {
            if (sender is DataGridRow row && row.Item is KeyValuePair<ushort, Point<byte>> kvp)
            {
                var menu = row.ContextMenu;

                if (menu != null)
                {
                    var writeItem = menu.Items.OfType<MenuItem>()
                                                .FirstOrDefault(m => m.Header.ToString() == "Change Value");
                    if (writeItem != null)
                    {
                        writeItem.Visibility = kvp.Value.Type == PointsType.DISCRETE_INPUTS
                            ? Visibility.Collapsed
                            : Visibility.Visible;
                    }
                }

                coilsPoint = kvp.Value;
            }
        }

        private void ReadDiscreteValue_Click(object sender, RoutedEventArgs e)
        {
            InitiateReadModbusDTO initiateReadModbusDTO = new InitiateReadModbusDTO();
            initiateReadModbusDTO.Address = coilsPoint.Address;
            initiateReadModbusDTO.Quantity = 1;

            if (coilsPoint.Type == PointsType.COILS)
                scada.initateMessage(Common.Message.FunctionCode.ReadCoils, initiateReadModbusDTO);
            else
                scada.initateMessage(Common.Message.FunctionCode.ReadDiscreteInputs, initiateReadModbusDTO);

            coilsPoint = null;
        }

        private void WriteDiscreteValue_Click(object sender, RoutedEventArgs e)
        {
            InitiateWriteSingleModbusDTO initiateWriteSingleModbusDTO = new InitiateWriteSingleModbusDTO();
            initiateWriteSingleModbusDTO.Address=coilsPoint.Address;

            if (coilsPoint.Value == 0)
            {
                initiateWriteSingleModbusDTO.Value = 1;
            }
            else
            {
                initiateWriteSingleModbusDTO.Value = 0;
            }

            scada.initateMessage(Common.Message.FunctionCode.WriteSingleCoil, initiateWriteSingleModbusDTO);

            coilsPoint = null;
        }

        #endregion

        #region Read/Write Registers

        private void ContextMenu_RegistersContextMenuOpening(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {
            if (sender is DataGridRow row && row.Item is KeyValuePair<ushort, Point<short>> kvp)
            {
                var menu = row.ContextMenu;
                if (menu != null)
                {
                    var writeItem = menu.Items.OfType<MenuItem>()
                                                .FirstOrDefault(m => m.Header.ToString() == "Write");
                    if (writeItem != null)
                    {
                        writeItem.Visibility = kvp.Value.Type == PointsType.INPUT_REGISTERS
                            ? Visibility.Collapsed
                            : Visibility.Visible;
                    }
                }

                registersPoint = kvp.Value;
            }          
        }

        private void ReadRegister_Click(object sender, RoutedEventArgs e)
        {
            InitiateReadModbusDTO initiateReadModbusDTO = new InitiateReadModbusDTO();
            initiateReadModbusDTO.Address = registersPoint.Address;
            initiateReadModbusDTO.Quantity = 1;

            if(registersPoint.Type==PointsType.INPUT_REGISTERS)
                scada.initateMessage(Common.Message.FunctionCode.ReadInputRegisters, initiateReadModbusDTO);
            else
                scada.initateMessage(Common.Message.FunctionCode.ReadHoldingRegisters, initiateReadModbusDTO);

            registersPoint = null;

        }

        private void WriteRegister_Click(object sender, RoutedEventArgs e)
        {
            InputDialog inputDialog = new InputDialog("Enter the register value",typeof(short));

            inputDialog.Owner = this; 

            if (inputDialog.ShowDialog() == false)
            {
                return;
            }

            if(short.TryParse(inputDialog.ResponseText, out short value))
            {
                InitiateWriteSingleModbusDTO requestDTO = new InitiateWriteSingleModbusDTO();

                requestDTO.Address = registersPoint.Address;
                requestDTO.Value = value;

                scada.initateMessage(FunctionCode.WriteSingleRegister, requestDTO);
            }
            else
            {
                MessageBox.Show("Error","You entered invalid value for the register",MessageBoxButton.OK,MessageBoxImage.Warning);
            }

            registersPoint = null;
        }
        #endregion

        #region Read Multiple Discrete Values

        private void CoilsDiscreteValuesRead_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new InputTwoValuesDialog("Enter Address and Quantity to be readed");
            ushort number;
            ushort address;

            if (dialog.ShowDialog() == true)
            {
                 address = dialog.Address;
                 number = dialog.Number;
            }
            else
            {
                return;
            }

            Point<byte> point;
            pointsDatabase.Coils.TryGetValue(address,out point);

            if (point == null)
            {
                MessageBox.Show("Entered invalid address","Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            PointsType pointsType = point.Type;

            for(int i=1;i<number; i++)
            {
                pointsDatabase.Coils.TryGetValue((ushort)(address+i), out point);

                if (point == null)
                {
                    MessageBox.Show("Entered invalid numbers to be readed","Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            InitiateReadModbusDTO initiateReadModbusDTO = new InitiateReadModbusDTO();
            initiateReadModbusDTO.Address = address;
            initiateReadModbusDTO.Quantity = number;

            if(pointsType==PointsType.COILS)
                scada.initateMessage(FunctionCode.ReadCoils,initiateReadModbusDTO);
            else
                scada.initateMessage(FunctionCode.ReadDiscreteInputs,initiateReadModbusDTO);
        }

        #endregion

        #region Read Multiple Registers 

        private void RegistersRead_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new InputTwoValuesDialog("Enter Address and Quantity to be readed");
            ushort number;
            ushort address;

            if (dialog.ShowDialog() == true)
            {
                address = dialog.Address;
                number = dialog.Number;
            }
            else
            {
                return;
            }

            Point<short> point;
            pointsDatabase.Registers.TryGetValue(address, out point);

            if (point == null)
            {
                MessageBox.Show("Entered invalid address", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            PointsType pointsType = point.Type;

            for (int i = 1; i < number; i++)
            {
                pointsDatabase.Registers.TryGetValue((ushort)(address + i), out point);

                if (point == null)
                {
                    MessageBox.Show("Entered invalid numbers to be readed", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            InitiateReadModbusDTO initiateReadModbusDTO = new InitiateReadModbusDTO();
            initiateReadModbusDTO.Address = address;
            initiateReadModbusDTO.Quantity = number;

            if (pointsType == PointsType.HOLDING_REGISTERS)
                scada.initateMessage(FunctionCode.ReadHoldingRegisters, initiateReadModbusDTO);
            else
                scada.initateMessage(FunctionCode.ReadInputRegisters, initiateReadModbusDTO);
        }

        #endregion

        #region Write Multiple Registers

        private void WriteMultipleHoldingRegisters_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MultipleValueInputDialog();

            if (dialog.ShowDialog() == true)
            {
                ushort address = dialog.Address;
                ushort quantity = dialog.Quantity;
                List<short> values = dialog.Values;

                Point<short> pointTemp;

                if (quantity > 123)
                {
                    MessageBox.Show("Max quantity is 123", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                for(int i=0;i<quantity;i++)
                {
                    if(!pointsDatabase.Registers.TryGetValue((ushort)(address + i), out pointTemp))
                    {
                        MessageBox.Show("Address doesnt exists: "+ (address + i), "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else
                    {
                        if(pointTemp.Type==PointsType.INPUT_REGISTERS)
                        {
                            MessageBox.Show("Type of the address: "+ (address + i)+ " isn't HOLDING_REGISTERS", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                }

                InitiateWriteMultipleModbusDTO requestDTO= new InitiateWriteMultipleModbusDTO();
                requestDTO.Address = address;
                requestDTO.Quantity = quantity;
                
                requestDTO.Values=values.ToArray();

                scada.initateMessage(FunctionCode.WriteMultipleRegisters,requestDTO);
            }
        }
        #endregion

        #region  Write Multiple Coils 

        private void WriteMultipleCoils_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MultipleValueInputDialog();

            if (dialog.ShowDialog() == true)
            {
                ushort address = dialog.Address;
                ushort quantity = dialog.Quantity;
                List<short> values = dialog.Values;

                Point<byte> pointTemp;

                if (quantity > 1968)
                {
                    MessageBox.Show("Max quantity is 1968", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                for (int i = 0; i < quantity; i++)
                {
                    if (!pointsDatabase.Coils.TryGetValue((ushort)(address + i), out pointTemp))
                    {
                        MessageBox.Show("Address doesnt exists: " + (address + i), "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else
                    {
                        if (pointTemp.Type == PointsType.DISCRETE_INPUTS)
                        {
                            MessageBox.Show("Type of the address: " + (address + i) + " isn't COILS", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                }

                InitiateWriteMultipleModbusDTO requestDTO = new InitiateWriteMultipleModbusDTO();
                requestDTO.Address = address;
                requestDTO.Quantity = quantity;
                requestDTO.Values = values.ToArray();

                scada.initateMessage(FunctionCode.WriteMultipleCoils, requestDTO);
            }
        }
        #endregion

        #region InitRead

        private void InitRead()
        {
            InitiateReadModbusDTO initiateReadModbusDTO;
            foreach (Point<byte> p in pointsDatabase.Coils.Values.ToList())
            {
                initiateReadModbusDTO = new InitiateReadModbusDTO();
                initiateReadModbusDTO.Address = p.Address;
                initiateReadModbusDTO.Quantity = 1;

                if (p.Type == PointsType.COILS)
                {
                    scada.initateMessage(FunctionCode.ReadCoils, initiateReadModbusDTO);
                }
                else
                {
                    scada.initateMessage(FunctionCode.ReadDiscreteInputs, initiateReadModbusDTO);
                }
            }

            foreach (Point<short> p in pointsDatabase.Registers.Values.ToList())
            {
                initiateReadModbusDTO = new InitiateReadModbusDTO();
                initiateReadModbusDTO.Address = p.Address;
                initiateReadModbusDTO.Quantity = 1;

                if (p.Type == PointsType.HOLDING_REGISTERS)
                {
                    scada.initateMessage(FunctionCode.ReadHoldingRegisters, initiateReadModbusDTO);
                }
                else
                {
                    scada.initateMessage(FunctionCode.ReadInputRegisters, initiateReadModbusDTO);
                }
            }
        }

        #endregion
    }
}