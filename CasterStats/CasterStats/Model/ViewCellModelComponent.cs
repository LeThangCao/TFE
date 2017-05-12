using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CasterStats.Views;
using Xamarin.Forms;

namespace CasterStats.Model
{
    public class ViewCellModelComponent : ViewCell
    {
        public ViewCellModelComponent()
        {
            StackLayout layout = new StackLayout();
            Label label = new Label();

            label.SetBinding(Label.TextProperty, ".");
            layout.Children.Add(label);



            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; 
            deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            deleteAction.Clicked += OnDelete;

   
            this.ContextActions.Add(deleteAction);
            View = layout;
        }

     

        public void OnDelete(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            HomePage.ComponentList.Remove(item.CommandParameter.ToString());
        }
    }
    
}
