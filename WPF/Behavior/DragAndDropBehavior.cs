using Microsoft.Xaml.Behaviors;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.TreeGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragandDropDemo
{
    public class DragAndDropBehavior:Behavior<MainWindow>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            //Uncomment these events for Drag Drop customization
            //AssociatedObject.firstGrid.RowDragDropController.DragStart += RowDragDropController_DragStart;
            //AssociatedObject.firstGrid.RowDragDropController.DragOver += RowDragDropController_DragOver;
            //AssociatedObject.firstGrid.RowDragDropController.Drop += RowDragDropController_Drop;
            //AssociatedObject.firstGrid.RowDragDropController.Dropped += RowDragDropController_Dropped;
        }

        private void RowDragDropController_Dropped(object sender, GridRowDroppedEventArgs e)
        {
            ObservableCollection<object> draggingRecords = new ObservableCollection<object>();

            draggingRecords = e.Data.GetData("Records") as ObservableCollection<object>;

            var items = draggingRecords[0] as UserInfo;

            var records = AssociatedObject.firstGrid.View.Records.ToList();

            IList collection = AssociatedObject.firstGrid.ItemsSource as IList;

            for (int i = 0; i < records.Count; i++)
            {

                var orderData = records[i].Data as UserInfo;
                if (orderData.UserId == items.UserId)
                {
                    collection.Remove(items);
                    collection.Insert(i, orderData);
                }

            }
            AssociatedObject.firstGrid.ItemsSource = collection;
        }

        private void RowDragDropController_Drop(object sender, GridRowDropEventArgs e)
        {
            var record = e.DraggingRecords[0] as UserInfo;
            var dropPosition = e.DropPosition.ToString();

            if (dropPosition == "DropAbove")
                e.Handled = true;

            if (record != null && record.UserId == 1010)
                e.Handled = true;
        }


        private void RowDragDropController_DragOver(object sender, GridRowDragOverEventArgs e)
        {
            e.ShowDragUI = false;   
        }

        private void RowDragDropController_DragStart(object sender, GridRowDragStartEventArgs e)
        {
            var record = e.DraggingRecords[0] as UserInfo;
            if(record != null && record.UserId == 1005)
            {
                e.Handled = true;
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.firstGrid.RowDragDropController.DragStart -= RowDragDropController_DragStart;
            AssociatedObject.firstGrid.RowDragDropController.DragOver -= RowDragDropController_DragOver;
            AssociatedObject.firstGrid.RowDragDropController.Drop -= RowDragDropController_Drop;
            AssociatedObject.firstGrid.RowDragDropController.Dropped -= RowDragDropController_Dropped;
            base.OnDetaching();
        }
    }
}
