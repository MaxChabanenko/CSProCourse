
namespace Logistic.Models
{
    public interface IEntity<Tid>
    {
        public Tid Id { get; set; }
    }
}
