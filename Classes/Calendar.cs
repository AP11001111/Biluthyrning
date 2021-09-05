using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Biluthyrning.Classes
{
    internal class Calendar
    {
        public bool StopCalender { get; set; }
        private int CurrentDay;
        private readonly object CurrentDayLock = new object();

        public Calendar()
        {
            CurrentDay = 0;
            StopCalender = false;
        }

        public void StartCalender(UI ui)
        {
            while (!StopCalender)
            {
                //lock (CurrentDayLock)
                //{
                Thread.Sleep(2000);
                //Parallel.Invoke(() =>
                //{
                IncreaseCurrentDay();
                ui.UpdateCurrentDay(CurrentDay);
                Task.Run(() => ui.UpdateCarDaysToAvailability());
                Console.WriteLine("Outside");
                Console.WriteLine(ui.RefreshLiveTicker);
                Thread.Sleep(500);
                //t.Start();
                //ui.UpdateCarDaysToAvailability();
                //},
                //() =>
                //{
                if (ui.RefreshLiveTicker)
                {
                    Console.WriteLine("inside");
                    ui.ALiveTicker.StartUI();
                    if (ui.StopUI)
                    {
                        StopCalender = true;
                    }
                }
                //});

                //Console.WriteLine("Calender Current Day: " + GetCurrentDay());
                //Console.WriteLine("UI Current Day: " + ui.CurrentDay);
                //}
            }
        }

        public void PauseTime()
        {
            StopCalender = true;
        }

        public void ResumeTime(UI ui)
        {
            StopCalender = false;
            StartCalender(ui);
        }

        public int GetCurrentDay()
        {
            return CurrentDay;
        }

        private void IncreaseCurrentDay()
        {
            CurrentDay++;
        }

        public void SetUiCurrentDay(UI ui)
        {
            ui.CurrentDay = CurrentDay;
        }
    }
}