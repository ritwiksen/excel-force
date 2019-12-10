namespace ExcelForce.Models
{
    public class Reusables
    {
        private static Reusables instance;

        public string ConnectionProfile { get; set; }

        public string AccessToken { get; set; }

        private Reusables() { }

        public static Reusables Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Reusables();
                }
                return instance;
            }
        }
    }
}
