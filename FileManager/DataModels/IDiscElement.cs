using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TotalCommanderProjectV2.DataModels
{
    public abstract class DiscElement
    {
        public abstract string GetName();

        public abstract DateTime GetCreationDate();

        public abstract string GetDescription();

        public abstract string GetPath();
    }
}
