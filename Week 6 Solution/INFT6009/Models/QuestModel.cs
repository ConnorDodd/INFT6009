using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This line may be different depending on your project name
//Namespaces take the format <ProjectName>.<NamespaceName>
namespace INFT6009.Models
{
    public class QuestModel
    {
        public string Description { get; set; }
        public string Address { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
        public string ImageSource { get; set; }
    }
}
