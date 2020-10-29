using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Cells;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.Windows.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Selection_Copy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SampleDataGrid.AutoGeneratingColumn += SampleDataGrid_AutoGeneratingColumn1;
            SampleDataGrid.CellRenderers.Remove("Template");
            SampleDataGrid.CellRenderers.Add("Template", new GridCellTemplateRendererExt());
            SampleDataGrid.CurrentCellEndEdit += SampleDataGrid_CurrentCellEndEdit1;
            this.SampleDataGrid.Loaded += sfdatagrid_Loaded;
            this.SampleDataGrid.SortClickAction = SortClickAction.DoubleClick;
            this.SampleDataGrid.GridCopyPaste = new CustomCopyPaste(this.SampleDataGrid);

        }

        private void SampleDataGrid_CurrentCellEndEdit1(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellEndEditEventArgs e)
        {
            SampleDataGrid.UpdateDataRow(e.RowColumnIndex.RowIndex);
        }

        private void SampleDataGrid_AutoGeneratingColumn1(object sender, Syncfusion.UI.Xaml.Grid.AutoGeneratingColumnArgs e)
        {
            if (e.Column.MappingName == "SelectedFruitsIndex" || e.Column.MappingName == "DateTimeValue")
            {
                e.Cancel = true;
                return;
            }
            else if (e.Column.MappingName == "Value")
            {
                e.Column = new GridTemplateColumn() { MappingName = "Value" };
                e.Column.CellTemplateSelector = new DataTemplateSelectorExt(sender as SfDataGrid);
                e.Column.UseBindingValue = true;
            }
        }

        private void sfdatagrid_Loaded(object sender, RoutedEventArgs e)
        {
            var visualContainer = this.SampleDataGrid.GetVisualContainer();
            visualContainer.MouseLeftButtonDown += visualContainer_MouseLeftButtonDown;

        }

        void visualContainer_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (e.ClickCount == 1)
                {
                    //GetVisualContainer 
                    var visualContainer = this.SampleDataGrid.GetVisualContainer();
                    var rowcolumnindex = visualContainer.PointToCellRowColumnIndex(e.GetPosition(visualContainer));
                    var columnindex = this.SampleDataGrid.ResolveToGridVisibleColumnIndex(rowcolumnindex.ColumnIndex);
                    if (columnindex < 0)
                        return;

                    //Return if it is not HeaderRow5
                    if (this.SampleDataGrid.GetHeaderIndex() != rowcolumnindex.RowIndex)
                        return;

                    var firstrowdata = this.SampleDataGrid.GetRecordAtRowIndex(SampleDataGrid.GetFirstRowIndex());
                    //Get the record of LastRowIndex 
                    var lastrowdata = this.SampleDataGrid.GetRecordAtRowIndex(SampleDataGrid.GetLastRowIndex());
                    //Get the column of particular index
                    var column = this.SampleDataGrid.Columns[columnindex];

                    if (firstrowdata == null || lastrowdata == null)
                        return;

                    this.SampleDataGrid.SelectCells(firstrowdata, this.SampleDataGrid.Columns[columnindex], lastrowdata, column);
                }
            }
            catch
            {

            }
        }
        public class DataTemplateSelectorExt : DataTemplateSelector
        {
            SampleViewModel viewModel = null;
            ArrayList al;
            public DataTemplateSelectorExt(SfDataGrid dataGrid)
            {
                viewModel = dataGrid.DataContext as SampleViewModel;

            }

            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                if (item == null || container == null)
                    return base.SelectTemplate(item, container);

                if ((item as DataItem).ItemType == 1)
                {
                    if ((item as DataItem).ItemShortName <= 200)
                    {
                        var factory1 = new FrameworkElementFactory(typeof(ComboBox));
                        factory1.SetValue(ComboBox.BackgroundProperty, Brushes.Transparent);
                        factory1.SetValue(ComboBox.IsEnabledProperty, true);
                        factory1.SetBinding(ComboBox.SelectedValueProperty, new Binding("StringValue"));
                        factory1.SetValue(ComboBox.DisplayMemberPathProperty, "Name");
                        factory1.SetValue(ComboBox.SelectedValuePathProperty, "StringValue");
                        factory1.SetValue(ComboBox.ItemsSourceProperty, viewModel.NameValuePair);
                        factory1.SetValue(ComboBox.IsEditableProperty, true);
                        factory1.SetValue(ComboBox.BorderBrushProperty, Brushes.Transparent);
                        return new DataTemplate { VisualTree = factory1 };
                    }
                    else if ((item as DataItem).ItemShortName <= 400)
                    {
                        var factory1 = new FrameworkElementFactory(typeof(ComboBox));
                        factory1.SetValue(ComboBox.BackgroundProperty, Brushes.Transparent);
                        factory1.SetValue(ComboBox.IsEnabledProperty, true);
                        factory1.SetValue(ComboBox.DisplayMemberPathProperty, "Name");
                        factory1.SetValue(ComboBox.SelectedValuePathProperty, "StringValue");
                        factory1.SetValue(ComboBox.SelectedValueProperty, new Binding("StringValue"));
                        factory1.SetValue(ComboBox.ItemsSourceProperty, viewModel.NameValuePair1);
                        factory1.SetValue(ComboBox.IsEditableProperty, true);
                        factory1.SetValue(ComboBox.BorderBrushProperty, Brushes.Transparent);

                        return new DataTemplate { VisualTree = factory1 };
                    }
                    else
                    {
                        al = new ArrayList();
                        al.Add((item as DataItem).ItemShortName.ToString() + "" + "th" + " Mango");
                        var factory1 = new FrameworkElementFactory(typeof(ComboBox));
                        factory1.SetValue(ComboBox.BackgroundProperty, Brushes.Transparent);
                        factory1.SetValue(ComboBox.IsEnabledProperty, true);
                        factory1.SetValue(ComboBox.SelectedIndexProperty, 0);
                        factory1.SetValue(ComboBox.SelectedValueProperty, 0);
                        factory1.SetValue(ComboBox.TextProperty, al[0].ToString());
                        factory1.SetValue(ComboBox.IsEditableProperty, true);
                        factory1.SetValue(ComboBox.BorderBrushProperty, Brushes.Transparent);
                        return new DataTemplate { VisualTree = factory1 };
                    }
                }
                if ((item as DataItem).ItemType == 2)
                {
                    var factory2 = new FrameworkElementFactory(typeof(DateTimeEdit));
                    factory2.SetValue(DateTimeEdit.IsReadOnlyProperty, true);
                    factory2.SetValue(DateTimeEdit.BackgroundProperty, Brushes.Transparent);
                    factory2.SetValue(DateTimeEdit.IsEnabledProperty, true);
                    factory2.SetValue(DateTimeEdit.IsEditableProperty, true);
                    factory2.SetBinding(DateTimeEdit.DateTimeProperty, new Binding("DateTimeValue"));
                    return new DataTemplate { VisualTree = factory2 };
                }
                else
                {
                    var factory3 = new FrameworkElementFactory(typeof(TextBox));
                    factory3.SetBinding(TextBox.TextProperty, new Binding("ItemShortName"));
                    factory3.SetValue(TextBox.BackgroundProperty, Brushes.Transparent);
                    factory3.SetValue(TextBox.IsEnabledProperty, true);
                    return new DataTemplate { VisualTree = factory3 };
                }
            }
        }

        public class GridCellTemplateRendererExt : GridCellTemplateRenderer
        {
            public override bool CanUpdateBinding(GridColumn column)
            {
                return true;
            }

            public override void OnUpdateTemplateBinding(DataColumnBase dataColumn, ContentControl uiElement, object dataContext)
            {
                GridColumn column = dataColumn.GridColumn;
                uiElement.ContentTemplateSelector = null;
                uiElement.ContentTemplateSelector = column.CellTemplateSelector;
                base.OnUpdateTemplateBinding(dataColumn, uiElement, dataContext);
            }


        }



        public class CustomCopyPaste : GridCutCopyPaste
        {
            public CustomCopyPaste(SfDataGrid DataGrid)
                : base(DataGrid)
            {

            }
            protected override void CopyCell(object record, GridColumn column, ref System.Text.StringBuilder text)
            {

                if (this.dataGrid.View == null)
                    return;

                object copyText = null;

                if (column is GridUnBoundColumn)
                {
                    var unboundValue = this.dataGrid.GetUnBoundCellValue(column, record);
                    copyText = unboundValue != null ? unboundValue.ToString() : string.Empty;
                }
                else
                {
                    if (this.dataGrid.GridCopyOption.HasFlag(GridCopyOption.IncludeFormat))
                        copyText = this.dataGrid.View.GetPropertyAccessProvider().GetFormattedValue(record, column.MappingName);
                    else if (column is GridTemplateColumn && column.MappingName == "Value")
                    {
                        var dataItem = record as DataItem;
                        if (dataItem.ItemType == 1)
                        {
                            if (dataItem.ItemShortName <= 200)
                            {
                                var nameValuePair = (dataGrid.DataContext as SampleViewModel).NameValuePair;
                                copyText = nameValuePair[dataItem.StringValue].Name;

                            }
                            else if (dataItem.ItemShortName <= 400)
                            {
                                var nameValuePair = (dataGrid.DataContext as SampleViewModel).NameValuePair1;
                                copyText = nameValuePair[dataItem.StringValue].Name;
                            }
                            else
                            {
                                copyText = dataItem.ItemShortName.ToString() + "" + "th" + " Mango";
                            }
                        }
                        else if (dataItem.ItemType == 2)
                        {
                            copyText = dataItem.DateTimeValue;
                        }
                        else
                        {
                            copyText = dataItem.ItemShortName;
                        }
                    }
                    else
                        copyText = this.dataGrid.View.GetPropertyAccessProvider().GetValue(record, column.MappingName);
                }
                var copyargs = this.RaiseCopyGridCellContentEvent(column, record, copyText);
                if (!copyargs.Handled)
                {
                    if (this.dataGrid.Columns[leftMostColumnIndex] != column || text.Length != 0)
                        text.Append('\t');

                    text.Append(copyargs.ClipBoardValue);
                }
            }
        }
    }

    public static class ChildElementWrapper
    {
        public static T GetChildOfType<T>(this DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }
    }
}
