namespace GUI.Pages.Shared
{
    public class User_Id
    {
        public bool username { get; set; }
        User_Id() { 
            username = false;
        }
        public bool getUser_Id(bool username)
        {
            this.username = username;
            return username;
        }
    }
}
