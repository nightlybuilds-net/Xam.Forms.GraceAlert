namespace Xam.Forms.GraceAlert
{
    class GraceRequest
    {
        public GraceRequest(NotificationType type, string title, string message, bool block)
        {
            this.Type = type;
            this.Title = title;
            this.Message = message;
            this.Block = block;
        }

        public NotificationType Type { get; }
        public string Title { get; }
        public string Message { get; }
        public bool Block { get; }
    }
}