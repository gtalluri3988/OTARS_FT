using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Notifications;

namespace MTAoarsFamily.Builders.Interfaces {
    public interface IMailBuilder {

        NewMailViewModel GetNewMail();

    }
}
