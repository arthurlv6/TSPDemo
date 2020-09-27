using System;

namespace TSP.App.Components
{
    public enum MessageLevel 
    {
        Normal,
        Error,
    }
    public class GlobalMessage
    {
        public string Message { get; private set; }
        public string Color { get; private set; } = "blue";
        public event Action OnChange;

        public void SetMessage(string message="", MessageLevel messageLevel= MessageLevel.Normal)
        {
            Message = message;
            //MessageLevel = messageLevel;
            if (messageLevel == MessageLevel.Error)
            {
                Color = "red";
                // log the error in database;
            }
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
