using System;
using System.ComponentModel;

namespace StudentPlanner.Models {

    public enum Priority {
         Высокий,
         Средний,
         Низкий 
    }

    public enum Status {
        NotStarted,
        Started,
        Paused,
        Complete,
        Cancelled
    }

    public abstract class Task: INotifyPropertyChanged {

        private string _title;
        private string _description;
        private Priority _priority;
        private Status _status;
        private DateTime _dueDatetime;
        private DateTime _createdDatetime;
        private DateTime _completeDatetime;
        private bool _isComplete;
        private bool _isCancelled;

        public string Title {
            get { return _title; }
            set {
                _title = value;
                RaisePropertyChanged("Название");
                Toastr.Success("Обновлено", "Название задачи было обновлено");
            }
        }
        public string Description {
            get { return _description; }
            set {
                _description = value;
                RaisePropertyChanged("Описание");
                Toastr.Success("Обновлено", "Описание задачи было обновлено");
            }
        }
        public Priority Priority {
            get { return _priority; }
            set {
                _priority = value;
                RaisePropertyChanged("Приоритет");
                Toastr.Success("Обновлено", "Приоритет задачи был обновлен");
            }
        }
        public Status Status {
            get { return _status; }
            set {
                _status = value;
                RaisePropertyChanged("Status");
            }
        }
        public DateTime DueDatetime {
            get { return _dueDatetime; }
            set {
                _dueDatetime = value;
                RaisePropertyChanged("Назначенное время");
                RaisePropertyChanged("Срок оплаты");
                Toastr.Success("Обновлено", "Дата выполнения задачи была обновлена");
            }
        }
        public DateTime CreatedDatetime {
            get { return _createdDatetime; }
            set { _createdDatetime = value; }
        }
        public DateTime CompleteDatetime {
            get { return _completeDatetime; }
            set {
                _completeDatetime = value;
                RaisePropertyChanged("Время завершения");
            }
        }
        public bool IsComplete {
            get { return _isComplete; }
            set {
                _isComplete = value;
                RaisePropertyChanged("Завершено");
            }
        }
        public bool IsCancelled {
            get { return _isCancelled; }
            set {
                _isCancelled = value;
                RaisePropertyChanged("Отменено");
            }
        }

        public string DueDateReadable { get {
                int daysDiff = ((TimeSpan)(DueDatetime - DateTime.Now)).Days;
                if (daysDiff > 0) return "Due in " + daysDiff + " day" + (daysDiff > 1 ? "s" : "");
                else return "Эта задача  " + (IsComplete ? "завершена": "просрочена");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName) {
            Database.SaveTasks();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Task() { }

        public Task(
            string title, string description, Priority priority,
            Status status, DateTime dueDatetime, DateTime createdDatetime
        ) : this(title, description, priority, dueDatetime, createdDatetime) {
            Status = status;
        }
        
        public Task(string title, string description, Priority priority, DateTime dueDatetime, DateTime createdDatetime) {
            Title = title;
            Description = description;
            Priority = priority;
            DueDatetime = dueDatetime;
            CreatedDatetime = createdDatetime;

            Status = Status.NotStarted;
        }

        public void CompleteTask() {
            Status = Status.Complete;
            CompleteDatetime = DateTime.Now;
            IsComplete = true;
            Toastr.Success("Выполнено", "Задача была помечена как выполненная");
        }

        public void CancelTask() {
            Status = Status.Cancelled;
            IsCancelled = true;
            Toastr.Info("Отменено", "Задание было отменено");
        }

        public override string ToString() {
            return Title;
        }
    }
}
