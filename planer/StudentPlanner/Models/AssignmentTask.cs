﻿using StudentPlanner.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.Models {
    public class AssignmentTask : Task, IStartableTask, IPausableTask {

        private string _subject;
        private int _percentageWorth;
        private DateTime _pausedDatetime;
        private DateTime _startedDatetime;

        public string Subject {
            get { return _subject; }
            set {
                _subject = value;
                RaisePropertyChanged("Предмет");
                Toastr.Success("Обновлено", "Тема задания была обновлена");
            }
        }
        public int PercentageWorth {
            get { return _percentageWorth; }
            set {
                _percentageWorth = value;
                RaisePropertyChanged("Процент");
                Toastr.Success("Обновлено", "Значение процента было обновлено");
            }
        }
        public DateTime PausedDatetime {
            get { return _pausedDatetime; }
            set {
                _pausedDatetime = value;
                RaisePropertyChanged("PausedDatetime");
            }
        }
        public DateTime StartedDatetime {
            get { return _startedDatetime; }
            set {
                _startedDatetime = value;
                RaisePropertyChanged("StartedDatetime");
            }
        }

        public AssignmentTask() { }

        public AssignmentTask(
            string title, string description, Priority priority, Status status,
            DateTime dueDatetime, DateTime createdDatetime, string subject,
            int percentageWorth, DateTime startedDatetime
        ) : base(title, description, priority, status, dueDatetime, createdDatetime) {
            Subject = subject;
            PercentageWorth = percentageWorth;
            StartedDatetime = startedDatetime;
        }

        public AssignmentTask(
            string title, string description, Priority priority,
            DateTime dueDatetime, DateTime createdDatetime, string subject,
            int percentageWorth
        ) : base(title, description, priority, dueDatetime, createdDatetime) {
            Subject = subject;
            PercentageWorth = percentageWorth;
        }

        public void PauseTask() {
            Status = Status.Paused;
            PausedDatetime = DateTime.Now;
            Toastr.Info("Пауза", "'" + Title + "'Задача была приостановлена");
        }

        public void StartTask() {
            Status = Status.Started;
            StartedDatetime = DateTime.Now;
            Toastr.Info("Старт", "'" + Title + "' Задача была запущена");
        }

    }
}
