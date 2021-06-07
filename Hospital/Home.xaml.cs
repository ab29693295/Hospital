using Ry4SCom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital
{
    enum Ry4Cmd : ushort
    {
        RY_FIND = 1,            //Find Ry4S
        RY_FIND_NEXT,       //Find next
        RY_OPEN,            //Open Ry4S
        RY_CLOSE,           //Close Ry4S
        RY_READ,            //Read Ry4S
        RY_WRITE,           //Write Ry4S
        RY_RANDOM,          //Generate random
        RY_SEED,			//Generate seed
        RY_SET_MODULE = 11,
        RY_READ_USERID = 10,    //Read UID
        RY_CHECK_MODULE = 12,	//Check Module
        RY_WRITE_ARITHMETIC,//Write
        RY_CALCULATE1 = 14, //Calculate1
        RY_CALCULATE2,      //Calculate1
        RY_CALCULATE3,      //Calculate1
    };

    enum Ry4ErrCode : uint
    {
        ERR_SUCCESS = 0,                            //No error
        ERR_NO_PARALLEL_PORT = 0x80300001,      //(0x80300001)No parallel port
        ERR_NO_DRIVER,                          //(0x80300002)No drive
        ERR_NO_ROCKEY,                          //(0x80300003)No Ry4S
        ERR_INVALID_PASSWORD,                   //(0x80300004)Invalid password
        ERR_INVALID_PASSWORD_OR_ID,             //(0x80300005)Invalid password or ID
        ERR_SETID,                              //(0x80300006)Set id error
        ERR_INVALID_ADDR_OR_SIZE,               //(0x80300007)Invalid address or size
        ERR_UNKNOWN_COMMAND,                    //(0x80300008)Unkown command
        ERR_NOTBELEVEL3,                        //(0x80300009)Inner error
        ERR_READ,                               //(0x8030000A)Read error
        ERR_WRITE,                              //(0x8030000B)Write error
        ERR_RANDOM,                             //(0x8030000C)Generate random error
        ERR_SEED,                               //(0x8030000D)Generate seed error
        ERR_CALCULATE,                          //(0x8030000E)Calculate error
        ERR_NO_OPEN,                            //(0x8030000F)The Ry4S is not opened
        ERR_OPEN_OVERFLOW,                      //(0x80300010)Open Ry4S too more(>16)
        ERR_NOMORE,                             //(0x80300011)No more Ry4S
        ERR_NEED_FIND,                          //(0x80300012)Need Find before FindNext
        ERR_DECREASE,                           //(0x80300013)Dcrease error
        ERR_AR_BADCOMMAND,                      //(0x80300014)Band command
        ERR_AR_UNKNOWN_OPCODE,                  //(0x80300015)Unkown op code
        ERR_AR_WRONGBEGIN,                      //(0x80300016)There could not be constant in first instruction in arithmetic 
        ERR_AR_WRONG_END,                       //(0x80300017)There could not be constant in last instruction in arithmetic 
        ERR_AR_VALUEOVERFLOW,                   //(0x80300018)The constant in arithmetic overflow
        ERR_UNKNOWN = 0x8030ffff,                   //(0x8030FFFF)Unkown error

        ERR_RECEIVE_NULL = 0x80300100,          //(0x80300100)Receive null
        ERR_PRNPORT_BUSY = 0x80300101               //(0x80300101)Parallel port busy

    };

    /// <summary>
    /// Home.xaml 的交互逻辑
    /// </summary>
    public partial class Home : Window
    {

        private ushort pass1;
        private ushort pass2;
        private ushort pass3;
        private ushort pass4;
        public Home()
        {
            InitializeComponent();
            //TestR4();


        }
        public void TestR4()
        {
            byte[] buffer = new byte[1024];
            byte[] buf = new byte[4096];
            object obbuffer = new object();
            ushort handle = 0;
            ushort function = 0;
            ushort p1 = 0;
            ushort p2 = 0;
            ushort p3 = 0;
            ushort p4 = 0;
            uint lp1 = 0;
            uint lp2 = 0;

            string strCmd = "H=H^H, A=A*23, F=B*17, A=A+F, A=A+G, A=A<C, A=A^D, B=B^B, C=C^C, D=D^D";
            string strCmd1 = "A=A+B, A=A+C, A=A+D, A=A+E, A=A+F, A=A+G, A=A+H";
            string strCmd2 = "A=E|E, B=F|F, C=G|G, D=H|H";

            int iMaxRockey = 0;
            uint[] uiarrRy4ID = new uint[32];
            uint iCurrID;

            char[] cmd = strCmd.ToCharArray();
            char[] cmd1 = strCmd1.ToCharArray();
            char[] cmd2 = strCmd2.ToCharArray();

            pass1 = 0xc44c;
            pass2 = 0xc8f8;
            pass3 = 0x0799;
            pass4 = 0xc43b;


            p1 = pass1; p2 = pass2; p3 = pass3; p4 = pass4;
            CRy4S Ry4S = new CRy4S();
            obbuffer = (object)buffer;
            ulong ret = Ry4S.Rockey((ushort)Ry4Cmd.RY_FIND, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);//Find Ry4S

            string strRet;
            string strTmp;
          
            if (0 != ret)
            {
                strRet = string.Format("No Rockey Found! Error code:{0}", ret);
                MessageTip msg1 = new MessageTip(strRet);
                msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg1.Show();
                Application.Current.Shutdown();
                return;
            }
            uiarrRy4ID[iMaxRockey] = lp1;
            iMaxRockey++;

            ulong flag = 0;
            while (flag == 0)
            {//Find next Ry4S
                flag = Ry4S.Rockey((ushort)Ry4Cmd.RY_FIND_NEXT, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);

                if (flag == 0)
                {
                    uiarrRy4ID[iMaxRockey] = lp1;
                    iMaxRockey++;
                }

            }

            strRet = "Found " + iMaxRockey + " Ry4S";
          

            for (int i = 0; i < iMaxRockey; i++)
            {

                strRet = string.Format("({0}): {1:X8}", i + 1, uiarrRy4ID[i]);
             

            }

            iCurrID = uiarrRy4ID[0];
            for (int n = 0; n < iMaxRockey; n++)
            {
                lp1 = uiarrRy4ID[n];
                p1 = pass1; p2 = pass2; p3 = pass3; p4 = pass4;

                //Open Ry4S
                ret = Ry4S.Rockey((ushort)Ry4Cmd.RY_OPEN, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);

                if (0 != ret)
                {
                    strRet = string.Format("Open Rockey failed! Error code:{0}", ret);
                    MessageTip msg1 = new MessageTip(strRet);
                    msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    msg1.Show();
                    Application.Current.Shutdown();
                    return;
                }


                //write Ry4S
                strRet = "Writting 20 bytes to user data zone:";
                for (int i = 0; i < 20; i++)
                {
                    buffer[i] = (byte)i;
                    strRet = strRet + string.Format("{0} ", buffer[i]);
                }
                obbuffer = (object)buffer;

              

                p1 = 0; p2 = 20;
                ret = Ry4S.Rockey((ushort)Ry4Cmd.RY_WRITE, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);
                if (0 != ret)
                {
                    strRet = string.Format("Write Rockey failed! Error code:{0}", ret);
                    MessageTip msg1 = new MessageTip(strRet);
                    msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    msg1.Show();
                    Application.Current.Shutdown();
                    return;
                }

                //Read Ry4S
                strRet = string.Format("Reading 20 bytes from user data zone...");
             

                p1 = 0; p2 = 20;
                ret = Ry4S.Rockey((ushort)Ry4Cmd.RY_READ, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);
                if (0 != ret)
                {
                    strRet = string.Format("Read Rockey failed! Error code:{0}", ret);
                    MessageTip msg1 = new MessageTip(strRet);
                    msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    msg1.Show();
                    Application.Current.Shutdown();
                    return;
                }
                strRet = "Reading 20 bytes from user data zone:";
                buffer = (Byte[])obbuffer;
                for (int i = 0; i < 20; i++) strRet = strRet + string.Format("{0} ", buffer[i]);
            

                //Generate random
                ushort[] usRandom = new ushort[4];
                strRet = string.Format("Generating 8 bytes random...");

                for (int i = 0; i < 4; i++)
                {
                    ret = Ry4S.Rockey((ushort)Ry4Cmd.RY_RANDOM, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);
                    if (0 != ret)
                    {
                        strRet = string.Format("Build Random failed! Error code:{0}", ret);
                        MessageTip msg1 = new MessageTip(strRet);
                        msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        msg1.Show();
                        Application.Current.Shutdown();
                        return;
                    }
                    usRandom[i] = p1;
                }

                foreach (ushort i in usRandom)
                {
                    strRet += string.Format("{0:X4} ", i);
                }

             


                //Generate seed code. lp2 is seed.

                strRet = string.Format("Generating seed code, seed=0x12345678...");
            
                lp2 = 0x12345678;
                ret = Ry4S.Rockey((ushort)Ry4Cmd.RY_SEED, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);
                if (0 != ret)
                {
                    strRet = string.Format("Generate seed failed! Error code:{0}", ret);
                    MessageTip msg1 = new MessageTip(strRet);
                    msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    msg1.Show();
                    Application.Current.Shutdown();
                    return;
                }

                strRet = string.Format("seed code is {0:X4} {1:X2} {2:X4} {3:X4}", p1, p2, p3, p4);
            

                // Read UID.
                strRet = string.Format("Reading user ID    ");
                ret = Ry4S.Rockey((ushort)Ry4Cmd.RY_READ_USERID, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);
                if (0 != ret)
                {
                    strRet = string.Format("Read UID failed! Error code:{0}", ret);
                    MessageTip msg1 = new MessageTip(strRet);
                    msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    msg1.Show();
                    Application.Current.Shutdown();
                    return;
                }
                strRet += string.Format("User ID is {0:X8}", lp1);
           

                //Check module. p1=module index
                strRet = string.Format("Checking module 8...");
              
                p1 = 8;
                ret = Ry4S.Rockey((ushort)Ry4Cmd.RY_CHECK_MODULE, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);
                if (0 != ret)
                {
                    strRet = string.Format("Check module failed! Error code:{0}", ret);
                    MessageTip msg1 = new MessageTip(strRet);
                    msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    msg1.Show();
                    Application.Current.Shutdown();
                    return;
                }

                strRet = string.Format("bValidate={0}, bDecreasable={1}", p2, p3);
            

                p1 = 0;
                for (int i = 0; i < buf.Length; i++) buf[i] = 0;
                for (int i = 0; i < cmd.Length; i++) buf[i] = (Byte)cmd[i];
                obbuffer = (object)buf;
                ret = Ry4S.Rockey((ushort)Ry4Cmd.RY_WRITE_ARITHMETIC, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);
                if (0 != ret)
                {
                    strRet = string.Format("RY_WRITE_ARITHMETIC failed! Error code:{0}", ret);
                    MessageTip msg1 = new MessageTip(strRet);
                    msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    msg1.Show();
                    Application.Current.Shutdown();
                    return;
                }
           

                //Calculate 1
                strRet = "Calculate 1,A=1,B=2,C=3,D=4...";
            
                //lp1=0; lp2=8; p1=1; p2=2; p3=3; p4=4;
                lp1 = 0;
                lp2 = 7;
                p1 = 5;
                p2 = 3;
                p3 = 1;
                p4 = 0xffff;
                ret = Ry4S.Rockey((ushort)Ry4Cmd.RY_CALCULATE1, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);
                if (0 != ret)
                {
                    strRet = string.Format("Calcullate1 failed! Error code:{0}", ret);
                    MessageTip msg1 = new MessageTip(strRet);
                    msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    msg1.Show();
                    Application.Current.Shutdown();
                    return;
                }
                strRet = string.Format("result: A={0:X4},B={1:X4},C={2:X4},D={3:X4}", p1, p2, p3, p4);
             


                strRet = "Calculate 2,Seed=0x12345678,A=1,B=2,C=3,D=4...";
            

                p1 = 10;
                for (int i = 0; i < buf.Length; i++) buf[i] = 0;
                for (int i = 0; i < cmd1.Length; i++) buf[i] = (Byte)cmd1[i];
                obbuffer = (object)buf;
                ret = Ry4S.Rockey((ushort)Ry4Cmd.RY_WRITE_ARITHMETIC, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);
                if (0 != ret)
                {
                    strRet = string.Format("RY_WRITE_ARITHMETIC failed! Error code:{0}", ret);
                    MessageTip msg1 = new MessageTip(strRet);
                    msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    msg1.Show();
                    Application.Current.Shutdown();
                    return;
                }
           

                //Calculate 2
                lp1 = 10;
                lp2 = 0x12345678;
                p1 = 1;
                p2 = 2;
                p3 = 3;
                p4 = 4;
                ret = Ry4S.Rockey((ushort)Ry4Cmd.RY_CALCULATE2, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);
                if (0 != ret)
                {
                    strRet = string.Format("Calcullate2 failed! Error code:{0}", ret);
                    MessageTip msg1 = new MessageTip(strRet);
                    msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    msg1.Show();
                    Application.Current.Shutdown();
                    return;
                }

                strRet = string.Format("result: A={0:X4},B={1:X4},C={2:X4},D={3:X4}", p1, p2, p3, p4);
             

                //Calculate 3
                strRet = "Calculate 3,A=1,B=2,C=3,D=4...";
            

                p1 = 17;
                for (int i = 0; i < buf.Length; i++) buf[i] = 0;
                for (int i = 0; i < cmd2.Length; i++) buf[i] = (Byte)cmd2[i];
                obbuffer = (object)buf;
                ret = Ry4S.Rockey((ushort)Ry4Cmd.RY_WRITE_ARITHMETIC, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);
                if (0 != ret)
                {
                    strRet = string.Format("RY_WRITE_ARITHMETIC failed! Error code:{0}", ret);
                    MessageTip msg1 = new MessageTip(strRet);
                    msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    msg1.Show();
                    Application.Current.Shutdown();
                    return;
                }
            

                lp1 = 17;
                lp2 = 6;
                p1 = 1;
                p2 = 2;
                p3 = 3;
                p4 = 4;
                ret = Ry4S.Rockey((ushort)Ry4Cmd.RY_CALCULATE3, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);
                if (0 != ret)
                {
                    strRet = string.Format("Calcullate3 failed! Error code:{0}", ret);
                    MessageTip msg1 = new MessageTip(strRet);
                    msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    msg1.Show();
                    Application.Current.Shutdown();
                    return;
                }

                strRet = string.Format("result: A={0:X4},B={1:X4},C={2:X4},D={3:X4}", p1, p2, p3, p4);
              

                //Close Ry4S
                strRet = string.Format("Closing handle {0:X4}...", handle);
            
                ret = Ry4S.Rockey((ushort)Ry4Cmd.RY_CLOSE, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);

                strRet = string.Format("[OK]");

                //MessageTip msg = new MessageTip(strRet);
                //msg.Show();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }
    }
}
