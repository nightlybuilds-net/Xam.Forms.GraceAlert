namespace Xam.Forms.GraceAlert
{
    class GraceRequest
    {
        public GraceRequest(NotificationType type, string title, string message)
        {
            this.Type = type;
            this.Title = title;
            this.Message = message;
        }

        public NotificationType Type { get; }
        public string Title { get; }
        public string Message { get; }
    }
}