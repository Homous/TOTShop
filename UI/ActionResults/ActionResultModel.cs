namespace UI.ActionResults
{
    public class ActionResultModel
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public Object Data { get; set; }

        public ActionResultModel()
        {

        }
        public ActionResultModel(string message, bool status, object data)
        {
            this.Message = message;
            this.Status = status;
            this.Data = data;
        }
    }
}
