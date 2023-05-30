using StudentPlanner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.Models {
    public class ExamTask : Task, IStartableTask {

        private string _subject;
        private int _percentageWorth;
        private List<string> _materialsNeeded;
        private DateTime _startedDatetime;

        public List<string> MaterialsNeeded {
            get { return _materialsNeeded; }
            set {
                _materialsNeeded = value;
                RaisePropertyChanged("Необходимые материалы");
                Toastr.Success("Обновлено", "Экзаменационные материалы были обновлены");
            }
        }
        public string Materials { get { return string.Join(",", MaterialsNeeded); }
            set {
                this.MaterialsNeeded = value.Split(',').Select(s => s.Trim()).ToList();
                RaisePropertyChanged("Materials");
            }
        }

        public string Subject {
            get { return _subject; }
            set {
                _subject = value;
                RaisePropertyChanged("Предмет");
                Toastr.Success("Обновлено", "Предмет экзамена был обновлен");
            }
        }
        public int PercentageWorth {
            get { return _percentageWorth; }
            set {
                _percentageWorth = value;
                RaisePropertyChanged("Процент");
                Toastr.Success("Обновлено", "Процент экзамена был обновлен");
            }
        }
        public DateTime StartedDatetime {
            get { return _startedDatetime; }
            set {
                _startedDatetime = value;
                RaisePropertyChanged("StartedDatetime");
            }
        }

        public ExamTask() { }

        public ExamTask(
            string title, string description, Priority priority, Status status,
            DateTime dueDatetime, DateTime createdDatetime, string subject,
            int percentageWorth, List<string> materialsNeeded
        ) : base(title, description, priority, status, dueDatetime, createdDatetime) {
            Subject = subject;
            PercentageWorth = percentageWorth;
            MaterialsNeeded = materialsNeeded;
        }

        public ExamTask(
            string title, string description, Priority priority,
            DateTime dueDatetime, DateTime createdDatetime, string subject,
            int percentageWorth, List<string> materialsNeeded
        ) : base(title, description, priority, dueDatetime, createdDatetime) {
            Subject = subject;
            PercentageWorth = percentageWorth;
            MaterialsNeeded = materialsNeeded;
        }

        public void StartTask() {
            Status = Status.Started;
            StartedDatetime = DateTime.Now;
            Toastr.Info("Старт", " '" + Title + "'Задача была запущена");
        }

    }
}
