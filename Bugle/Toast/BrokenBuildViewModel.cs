using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace SevanConsulting.Bugle.Toast
{
    public class BrokenBuildViewModel:PropertyChangedBase
    {
        private string _buildMessage;
        private string _heading;

        public string BuildMessage
        {
            get { return _buildMessage; }
            set
            {
                if (value == _buildMessage) return;
                _buildMessage = value;
                NotifyOfPropertyChange(() => BuildMessage);
            }
        }

        public string Heading
        {
            get { return _heading; }
            set
            {
                if (value == _heading) return;
                _heading = value;
                NotifyOfPropertyChange(() => Heading);
            }
        }
     
    }
}
