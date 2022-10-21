using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxPhonebook.Domain.AggregatesModel.ContactAggregateModel
{
    public class Tag : Entity<Guid>
    {
        public Tag(string title)
        {
            Id = Guid.NewGuid();
            Title = Guard.Against.InvalidNameInput(title, nameof(title));
        }

        public string Title { get; private set; }
    }
}
