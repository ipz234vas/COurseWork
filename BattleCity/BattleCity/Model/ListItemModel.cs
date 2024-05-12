using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleCity.Model
{
    public class ListItemModel
    {
        public string Text { get; set; }
        public ICommand Command { get; set; }

        public ListItemModel (string text, ICommand command)
        {
            Text = text;
            Command = command;
        }

        public ListItemModel()
        {

        }
    }
}
