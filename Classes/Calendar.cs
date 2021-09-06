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
        public int CurrentDay;
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
                Thread.Sleep(2000);
                IncreaseCurrentDay();
                ui.UpdateCurrentDay(CurrentDay);
                ui.UpdateCarDaysToAvailability();
                if (ui.RefreshLiveTicker)
                {
                    ui.ALiveTicker.StartUI(this, ui);
                    if (ui.StopUI)
                    {
                        StopCalender = true;
                    }
                }
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

        public void IncreaseCurrentDay()
        {
            CurrentDay++;
        }

        public void SetUiCurrentDay(UI ui)
        {
            ui.CurrentDay = CurrentDay;
        }
    }
}