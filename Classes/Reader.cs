using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Biluthyrning.Classes
{
    public class Reader
    {
        private static Thread inputThread;
        private static AutoResetEvent getInput, gotInput;
        private static int Input;
        private static string InputLine;
        private static ConsoleKeyInfo InputKey;
        private static ReadTypeAsEnum ReadType;
        private static bool IsReadKeyInputHidden;

        private enum ReadTypeAsEnum { Read, ReadKey, ReadLine };

        static Reader()
        {
            ReadType = new ReadTypeAsEnum();
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);
            inputThread = new Thread(reader);
            inputThread.IsBackground = true;
            inputThread.Start();
        }

        private static void reader()
        {
            while (true)
            {
                getInput.WaitOne();
                switch (ReadType)
                {
                    case ReadTypeAsEnum.Read:
                        Input = Console.Read();
                        break;

                    case ReadTypeAsEnum.ReadKey:
                        InputKey = Console.ReadKey(IsReadKeyInputHidden);
                        break;

                    case ReadTypeAsEnum.ReadLine:
                        InputLine = Console.ReadLine();
                        break;
                }
                gotInput.Set();
            }
        }

        public static bool TryReadLine(out string inputLine, int timeOutMillisecs = Timeout.Infinite)
        {
            ReadType = ReadTypeAsEnum.ReadLine;
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success)
                inputLine = InputLine;
            else
                inputLine = null;
            return success;
        }

        public static bool TryReadKey(out ConsoleKey inputKey, int timeOutMillisecs = Timeout.Infinite, bool isReadKeyInputHidden = false)
        {
            IsReadKeyInputHidden = isReadKeyInputHidden;
            ReadType = ReadTypeAsEnum.ReadKey;
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success)
                inputKey = InputKey.Key;
            else
                /*Returning a key that has a low possibility of being used
                  as ConsoleKey is non nullable value type*/
                inputKey = ConsoleKey.Help;
            return success;
        }

        public static bool TryRead(out int inputChar, int timeOutMillisecs = Timeout.Infinite)
        {
            ReadType = ReadTypeAsEnum.Read;
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success)
                inputChar = Input;
            else
                inputChar = -1;
            return success;
        }
    }
}