namespace TesteWebmotors.Web.Models
{
    public abstract class EntidadeBaseViewModel
    {
        protected EntidadeBaseViewModel()
        {
        }
        protected EntidadeBaseViewModel(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
